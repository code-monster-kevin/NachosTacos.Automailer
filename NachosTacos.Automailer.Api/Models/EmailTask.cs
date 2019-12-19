using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NachosTacos.Automailer.Api.Models
{
    public class EmailTask
    {
        public Guid Id { get; set; }
        public string MailFrom { get; set; }
        public string MailTo { get; set; }
        public string Subject { get; set; }
        public Guid EmailTemplateId { get; set; }
        public virtual EmailTemplate EmailTemplate { get; set; }
    }
}
