using System;
using System.ComponentModel.DataAnnotations;

namespace NachoTacos.Automailer.Domain
{
    public class EmailTemplate : Updateable
    {
        [Key]
        public virtual Guid EmailTemplateId { get; protected set; }
        [Required]
        public virtual string EmailFrom { get; protected set; }
        [Required]
        public virtual string EmailSubject { get; protected set; }
        [Required]
        public virtual string EmailContent { get; protected set; }

        public static EmailTemplate Create(string from, string subject, string content)
        {
            return new EmailTemplate
            {
                EmailTemplateId = Guid.NewGuid(),
                EmailFrom = from,
                EmailSubject = subject,
                EmailContent = content
            };
        }
    }
}
