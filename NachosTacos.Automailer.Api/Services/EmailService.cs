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
        public async Task SendEmail(AutomailerTask automailerTask)
        {
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
            }
        }
    }
}
