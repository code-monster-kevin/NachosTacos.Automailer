using System;
using System.Collections.Generic;
using System.Text;

namespace NachoTacos.Automailer.Domain
{
    public class AutomailerTask
    {
        public Guid Id { get; set; }
        public List<EmailTemplateModel> ModelList { get; set; }
        public EmailTemplate EmailTemplate { get; set; }
    }
}
