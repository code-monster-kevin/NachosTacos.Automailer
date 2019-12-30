using System;
using System.ComponentModel.DataAnnotations;

namespace NachoTacos.Automailer.Domain
{
    /// <summary>
    /// All the contacts that we have, includes leads, existing customers, prospects...etc
    /// ContactId is used as the reference to other models
    /// But the main unique identifier is the email address
    /// The other information is optional
    /// </summary>
    public class Contact : Updateable
    {
        public virtual Guid ContactId { get; protected set; }
        [Key]
        public virtual string Email { get; protected set; }
        [Required]
        public virtual bool Unsubscribe { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual string Mobile { get; protected set; }
        public virtual string IdentityNo { get; protected set; }
        public virtual string Nationality { get; protected set; }
        public virtual string Source { get; protected set; }

        public static Contact Create(string source, string email, string name = null, string mobile = null, string identityNo = null, string nationality = null)
        {
            return Create(Guid.NewGuid(), source, email, name, mobile, identityNo, nationality);
        }

        public static Contact Create(Guid id, string source, string email, string name, string mobile, string identityNo, string nationality)
        {
            if (string.IsNullOrEmpty(source)) throw new ArgumentNullException("source");
            if (string.IsNullOrEmpty(email)) throw new ArgumentNullException("email");

            return new Contact()
            {
                ContactId = id,
                Source = source,
                Email = email,
                Unsubscribe = false,
                Name = name,
                Mobile = mobile,
                IdentityNo = identityNo,
                Nationality = nationality,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

        }
    }
}
