using System;
using System.ComponentModel.DataAnnotations;

namespace NachoTacos.Automailer.Domain
{
    /// <summary>
    /// EmailModel is used together with the email template to create personalized emails
    /// </summary>
    public class EmailModel : Updateable
    {
        public virtual Guid EmailModelId { get; protected set; }

        [Required]
        public virtual string Email { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual string Text1 { get; protected set; }
        public virtual string Text2 { get; protected set; }
        public virtual string Text3 { get; protected set; }

        public static EmailModel Create(string email, string name, string text1 = null, string text2 = null, string text3=null)
        {

            return Create(Guid.NewGuid(), email, name, text1, text2, text3);
        }

        public static EmailModel Create(Guid id, string email, string name, string text1 = null, string text2 = null, string text3 = null)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentNullException("email");

            return new EmailModel
            {
                EmailModelId = id,
                Email = email,
                Name = name,
                Text1 = text1,
                Text2 = text2,
                Text3 = text3,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };
        }
    }
}
