using FluentEmail.Core;
using NachoTacos.Automailer.Domain;
using System.Threading.Tasks;
using NachosTacos.Automailer.Api.ViewModels;

namespace NachosTacos.Automailer.Api.Services
{
    public class EmailService
    {
        private readonly IFluentEmailFactory _emailFactory;

        public EmailService(IFluentEmailFactory emailFactory)
        {
            _emailFactory = emailFactory;
        }

        public async Task Send(AutomailerTask emailTask)
        {
            if (emailTask != null)
            {
                foreach (EmailModel item in emailTask.EmailModels)
                {
                    await _emailFactory.Create()
                        .To(item.Email)
                        .Subject(emailTask.EmailTemplate.EmailSubject)
                        .UsingTemplate(emailTask.EmailTemplate.EmailContent, item)
                        .SendAsync();
                }
            }
        }
    }
}
