using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentEmail.Core;
using NachoTacos.Automailer.Domain;

namespace NachosTacos.Automailer.Api.Services
{
    public class EmailService
    {
        private IFluentEmailFactory _emailFactory;

        public EmailService(IFluentEmailFactory emailFactory)
        {
            _emailFactory = emailFactory;
        }

        public async Task Send(Guid Id)
        {
            AutomailerTask automailerTask = GetAutomailerTask(Id);
            if (automailerTask != null)
            {
                foreach (EmailTemplateModel item in automailerTask.ModelList)
                {
                    await _emailFactory.Create()
                        .To(item.Email)
                        .Subject(automailerTask.EmailTemplate.EmailSubject)
                        .UsingTemplate(automailerTask.EmailTemplate.EmailContent, item)
                        .SendAsync();
                }
            }
        }

        public AutomailerTask GetAutomailerTask(Guid Id)
        {
            EmailTemplate emailTemplate1 = new EmailTemplate
            {
                Id = Id,
                EmailSubject = "template email subject",
                EmailContent = @"<h1>hi @Model.Name</h1><br /> this is the first email @(5 + 5)! <br />"
            };

            EmailTemplateModel model1 = new EmailTemplateModel
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                Email = "jd@email.com"
            };

            EmailTemplateModel model2 = new EmailTemplateModel
            {
                Id = Guid.NewGuid(),
                Name = "Jane Doe",
                Email = "jd2@email.com"
            };

            List<EmailTemplateModel> modelList = new List<EmailTemplateModel>();
            modelList.Add(model1);
            modelList.Add(model2);

            AutomailerTask mailTask = new AutomailerTask()
            {
                Id = Guid.NewGuid(),
                EmailTemplate = emailTemplate1,
                ModelList = modelList
            };

            return mailTask;
        }
    }
}
