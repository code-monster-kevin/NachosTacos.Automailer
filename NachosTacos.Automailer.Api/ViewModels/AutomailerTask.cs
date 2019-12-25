using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NachoTacos.Automailer.Domain;

namespace NachosTacos.Automailer.Api.ViewModels
{
    public class AutomailerTask
    {
        public Guid AutomailerTaskId { get; set; }
        public EmailTemplate EmailTemplate { get; set; }
        public ICollection<EmailModel> EmailModels { get; set; }
    }
}
