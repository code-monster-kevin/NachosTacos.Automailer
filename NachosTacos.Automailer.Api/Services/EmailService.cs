using FluentEmail.Core;
using Microsoft.EntityFrameworkCore;
using NachoTacos.Automailer.Data;
using NachoTacos.Automailer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NachosTacos.Automailer.Api.Services
{
    public class EmailService
    {
        private readonly IFluentEmailFactory _fluentEmailFactory;
        private readonly IAutomailerContext _automailerContext;
        private readonly string ServerPath = "https://localhost:44382";

        public EmailService(IFluentEmailFactory fluentEmailFactory, IAutomailerContext automailerContext)
        {
            _fluentEmailFactory = fluentEmailFactory;
            _automailerContext = automailerContext;
        }

        #region "Execute Email Task"
        public async Task SendEmail(Guid id)
        {
            AutomailerTask automailerTask = _automailerContext.AutomailerTasks
                                                                .Include(x => x.AutomailerModels)
                                                                .FirstOrDefault(x => x.AutomailerTaskId == id);

            if (automailerTask != null)
            {
                foreach(AutomailerModel item in automailerTask.AutomailerModels)
                {
                    await _fluentEmailFactory.Create()
                            .To(item.Email)
                            .Subject(item.Subject)
                            .UsingTemplate(item.Content, item)
                            .SendAsync();
                }

                automailerTask.IsCompleted = true;
                _automailerContext.AutomailerTasks.Update(automailerTask);
                await _automailerContext.SaveChangesAsync();
            }
        }
        #endregion

        #region "Create Email Tasks"
        /// <summary>
        /// Creates the automailer tasks for sending emails
        /// This should be a daily task
        /// </summary>
        /// <returns></returns>
        public async Task CreateMailTask()
        {
            //gets the list of active campaigns for processing
            List<CampaignSetting> campaignSettings = _automailerContext.CampaignSettings.Where(x => x.Active == true).ToList();

            foreach (CampaignSetting campaignSetting in campaignSettings)
            {
                CampaignTrackingModel trackingModel = CreateAutomailerTrackingModels(campaignSetting);
                AutomailerTask automailerTask = AutomailerTask.Create(trackingModel.AutomailerModels);

                _automailerContext.AutomailerTasks.Add(automailerTask);
                _automailerContext.CampaignTrackings.AddRange(trackingModel.CampaignTrackings);
                await _automailerContext.SaveChangesAsync();
            }
        }

        private List<Contact> GetCampaignContacts(CampaignSetting campaignSetting)
        {
            // create the query of contacts that matches campaign contact and is still subscribed
            IQueryable<Contact> contactsQuery = from c in _automailerContext.Contacts
                                                join e in _automailerContext.CampaignContacts
                                                on c.ContactId equals e.ContactId
                                                where c.Unsubscribe == false
                                                && e.CampaignId == campaignSetting.CampaignId
                                                select c;

            // the final list of contacts that will be receiving emails for this campaign
            return contactsQuery.ToList();
        }

        private CampaignTrackingModel CreateAutomailerTrackingModels(CampaignSetting campaignSetting)
        {
            EmailTemplate emailTemplate = _automailerContext.EmailTemplates.FirstOrDefault(x => x.EmailTemplateId == campaignSetting.EmailTemplateId);
            List<Contact> contacts = GetCampaignContacts(campaignSetting);

            List<AutomailerModel> automailerModels = new List<AutomailerModel>();
            List<CampaignTracking> campaignTrackings = new List<CampaignTracking>();
            foreach (Contact contact in contacts)
            {
                CampaignTracking campaignTracking = CampaignTracking.Create(campaignSetting.CampaignSettingId, emailTemplate.EmailTemplateId, contact.ContactId);
                campaignTrackings.Add(campaignTracking);

                AutomailerModel automailerModel = AutomailerModel.Create(ServerPath, campaignTracking.CampaignTrackingId, contact.ContactId, contact.Email, emailTemplate.EmailSubject, emailTemplate.EmailContent, contact.Name);
                automailerModels.Add(automailerModel);
            }

            return new CampaignTrackingModel
            {
                AutomailerModels = automailerModels,
                CampaignTrackings = campaignTrackings
            };
        }

        private class CampaignTrackingModel
        {
            public List<AutomailerModel> AutomailerModels { get; set; }
            public List<CampaignTracking> CampaignTrackings { get; set; }
        }
        #endregion
    }
}
