using FluentEmail.Core;
using NachosTacos.Automailer.Api.ViewModels;
using NachoTacos.Automailer.Domain;
using System.Threading.Tasks;

namespace NachosTacos.Automailer.Api.Services
{
    public class EmailService
    {
        private readonly IFluentEmailFactory _fluentEmailFactory;
        public EmailService(IFluentEmailFactory fluentEmailFactory)
        {
            _fluentEmailFactory = fluentEmailFactory;
        }
        public async Task SendEmail(AutomailerTask emailTask)
        {
            if (emailTask != null)
            {
                foreach(EmailModel item in emailTask.EmailModels)
                {
                    await _fluentEmailFactory.Create()
                            .To(item.Email)
                            .Subject(emailTask.EmailTemplate.EmailSubject)
                            .UsingTemplate(emailTask.EmailTemplate.EmailContent, item)
                            .SendAsync();
                }
            }
        }
    }
}
