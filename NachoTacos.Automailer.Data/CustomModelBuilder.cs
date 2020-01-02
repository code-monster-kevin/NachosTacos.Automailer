using Microsoft.EntityFrameworkCore;
using NachoTacos.Automailer.Domain;
using System.Collections.Generic;

namespace NachoTacos.Automailer.Data
{
    internal class CustomModelBuilder
    {
        private static readonly string testFrom = "tester1@nachotacos.com";
        private static readonly string testSubject = "This is a test email";
        private static readonly string testContent = "<b>Hi @Model.Name,<br />We are having a<font color= 'red' > Great </ font > offer today!<br />Don't miss it!";

        public static void SeedTestData(ModelBuilder modelBuilder)
        {
            Campaign testCampaign = Campaign.Create("TEST", "A test campaign");
            List<Contact> testContacts = CreateContacts();
            List<CampaignContact> testCampaignContacts = CreateCampaignContacts(testCampaign, testContacts);
            EmailTemplate emailTemplate = EmailTemplate.Create(testFrom, testSubject, testContent);
            CampaignSetting campaignSetting = CampaignSetting.Create(testCampaign.CampaignId, emailTemplate.EmailTemplateId, 1);
            campaignSetting.Active = true; // newly created campaign settings are set to inactive by default

            modelBuilder.Entity<Campaign>().HasData(testCampaign);
            modelBuilder.Entity<Contact>().HasData(testContacts);
            modelBuilder.Entity<CampaignContact>().HasData(testCampaignContacts);
            modelBuilder.Entity<EmailTemplate>().HasData(emailTemplate);
            modelBuilder.Entity<CampaignSetting>().HasData(campaignSetting);
        }

        private static List<Contact> CreateContacts()
        {
            List<Contact> contacts = new List<Contact>
            {
                Contact.Create("seed data", "joe.test@mail.com", "Joe Test"),
                Contact.Create("seed data", "jane.test@mail.com", "Jane Test"),
                Contact.Create("seed data", "ken.test@mail.com", "Ken Test"),
                Contact.Create("seed data", "kelly.test@mail.com", "Kelly Test")
            };
            return contacts;
        }

        private static List<CampaignContact> CreateCampaignContacts(Campaign campaign, List<Contact> contacts)
        {
            List<CampaignContact> campaignContacts = new List<CampaignContact>();
            foreach (Contact contact in contacts)
            {
                campaignContacts.Add(CampaignContact.Create(campaign.CampaignId, contact.ContactId));
            }
            return campaignContacts;
        }
    }
}
