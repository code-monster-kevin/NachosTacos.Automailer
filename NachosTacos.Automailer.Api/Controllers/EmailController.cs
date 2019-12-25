using FluentEmail.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NachosTacos.Automailer.Api.Services;
using NachoTacos.Automailer.Data;
using NachoTacos.Automailer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NachosTacos.Automailer.Api.ViewModels;

namespace NachosTacos.Automailer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        #region "Constructors"
        private readonly ILogger<EmailController> _logger;
        private readonly AutomailerContext _automailerContext;

        public EmailController(AutomailerContext automailerContext, ILogger<EmailController> logger)
        {
            _logger = logger;
            _automailerContext = automailerContext;
        }

        #endregion

        #region "Controllers"
        [HttpGet]
        public IActionResult GetMailTasks()
        {
            List<EmailTask> emailTasks = _automailerContext
                                            .EmailTasks
                                            .ToList();

            return Ok(emailTasks);
        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetMailTasks(Guid Id)
        {
            AutomailerTask automailerTask = GetAutomailerTask(Id);
            if (automailerTask == null)
            {
                return NotFound();
            }
            return Ok(automailerTask);
        }

        [HttpPost]
        [Route("/add")]
        public async Task<IActionResult> AddEmailTask(string emails, string subject, string content)
        {
            List<EmailModel> modelList = CreateEmailModelList(emails);
            _automailerContext.EmailModels.AddRange(modelList);

            EmailTemplate emailTemplate = CreateEmailTemplate(subject, content);
            _automailerContext.EmailTemplates.Add(emailTemplate);

            EmailTask emailTask = CreateEmailTask(emailTemplate.EmailTemplateId);
            _automailerContext.Add(emailTask);

            List<EmailTaskModel> modelTaskList = CreateEmailTaskModelList(emailTask.EmailTaskId, modelList);
            _automailerContext.EmailTaskModels.AddRange(modelTaskList);

            await _automailerContext.SaveChangesAsync();

            _logger.LogInformation("New email task created {0}", emailTask.EmailTaskId.ToString());
            return Ok(emailTask.EmailTaskId);
        }

        [HttpPost]
        [Route("/send")]
        public async Task<IActionResult> ActivateEmailTask(Guid Id, [FromServices] IFluentEmailFactory emailFactory)
        {
            AutomailerTask automailerTask = GetAutomailerTask(Id);
            if (automailerTask == null)
            {
                return NotFound();
            }

            EmailService emailService = new EmailService(emailFactory);
            await emailService.Send(automailerTask);

            return Ok();
        }

        #endregion

        #region "Helper functions"
        private List<EmailModel> CreateEmailModelList(string emails)
        {
            string[] emailList = emails.Split(',');
            List<EmailModel> modelList = new List<EmailModel>();
            foreach (string email in emailList)
            {
                modelList.Add(new EmailModel
                {
                    EmailModelId = Guid.NewGuid(),
                    Email = email.Trim(),
                    Name = email.Trim()
                });
            }
            return modelList;
        }
        
        private EmailTemplate CreateEmailTemplate(string subject, string content)
        {
            return new EmailTemplate
            {
                EmailTemplateId = Guid.NewGuid(),
                EmailSubject = subject,
                EmailContent = content
            };
        }

        private EmailTask CreateEmailTask(Guid emailTemplateId)
        {
            return new EmailTask
            {
                EmailTaskId = Guid.NewGuid(),
                EmailTemplateId = emailTemplateId
            };
        }

        private List<EmailTaskModel> CreateEmailTaskModelList(Guid emailTaskId, List<EmailModel> emailModels)
        {
            List<EmailTaskModel> emailTaskModelList = new List<EmailTaskModel>();
            foreach(EmailModel emailModel in emailModels)
            {
                emailTaskModelList.Add(new EmailTaskModel
                {
                    EmailTaskId = emailTaskId,
                    EmailModelId = emailModel.EmailModelId,
                });
            }

            return emailTaskModelList;
        }

        private AutomailerTask GetAutomailerTask(Guid Id)
        {
            EmailTask emailTask = _automailerContext
                                    .EmailTasks
                                    .Include(x => x.EmailTemplate)
                                    .FirstOrDefault(x => x.EmailTaskId == Id);

            List<EmailModel> emailModels = (from et in _automailerContext.EmailTaskModels
                                           join em in _automailerContext.EmailModels on et.EmailModelId equals em.EmailModelId
                                           where et.EmailTaskId == Id
                                           select new EmailModel
                                           {
                                               EmailModelId = em.EmailModelId,
                                               Email = em.Email,
                                               Name = em.Name
                                           }).ToList();

            return new AutomailerTask
            {
                AutomailerTaskId = Id,
                EmailTemplate = emailTask.EmailTemplate,
                EmailModels = emailModels
            };
        }
        #endregion
    }
}