using System;

namespace NachoTacos.Automailer.Domain
{
    public class EmailTask : Updateable
    {
        public Guid EmailTaskId { get; set; }
        public Guid EmailTemplateId { get; set; }
        public virtual EmailTemplate EmailTemplate { get; set; }
    }
}
