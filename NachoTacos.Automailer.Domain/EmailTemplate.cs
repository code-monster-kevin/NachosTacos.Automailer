using System;
using System.ComponentModel.DataAnnotations;

namespace NachoTacos.Automailer.Domain
{
    public class EmailTemplate : Updateable
    {
        public Guid EmailTemplateId { get; set; }

        [Required]
        public string EmailFrom { get; set; }
        [Required]
        public string EmailSubject { get; set; }
        [Required]
        public string EmailContent { get; set; }
    }
}
