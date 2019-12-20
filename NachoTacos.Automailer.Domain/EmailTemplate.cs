using System;
using System.Collections.Generic;
using System.Text;

namespace NachoTacos.Automailer.Domain
{
    public class EmailTemplate
    {
        public Guid Id { get; set; }
        public string EmailSubject { get; set; }
        public string EmailContent { get; set; }
    }
}
