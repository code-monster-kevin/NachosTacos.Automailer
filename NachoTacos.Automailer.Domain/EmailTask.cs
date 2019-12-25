using System;
using System.Collections.Generic;
using System.Text;

namespace NachoTacos.Automailer.Domain
{
    public class EmailTask
    {
        public Guid EmailTaskId { get; set; }
        public Guid EmailTemplateId { get; set; }
        public virtual EmailTemplate EmailTemplate { get; set; }
    }
}
