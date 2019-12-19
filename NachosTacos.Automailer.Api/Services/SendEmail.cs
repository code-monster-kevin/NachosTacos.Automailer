using FluentEmail.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NachosTacos.Automailer.Api.Models;

namespace NachosTacos.Automailer.Api.Services
{
    public class SendEmail
    {
        public async Task Send(string sendTo, string subject, string template, TemplateModel model, IFluentEmail email)
        {
            await email
            .To(sendTo)
            .Subject(subject)
            .UsingTemplate(template, model)
            .SendAsync();
        }
    }
}
