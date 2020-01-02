using System;
using System.ComponentModel.DataAnnotations;

namespace NachoTacos.Automailer.Domain
{
    /// <summary>
    /// The personalized email data
    /// Tracking link is created automatically in the content template
    /// </summary>
    public class AutomailerModel
    {
        [Key]
        public virtual Guid AutomailerModelId { get; protected set; }
        public virtual string Email { get; protected set; }
        public virtual string Subject { get; protected set; }
        public virtual string Content { get; protected set; }
        public virtual string TrackingLink { get; protected set; }
        public virtual string UnsubscribeLink { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual string Text1 { get; protected set; }
        public virtual string Text2 { get; protected set; }
        public virtual string Text3 { get; protected set; }

        public static AutomailerModel Create(string serverPath, Guid trackingId, Guid contactId, string email, string subject, string content, string name = "", string text1 = "", string text2 = "", string text3 = "")
        {
            string trackingLink = string.Format("{0}/pix/{1}/pixel.gif", serverPath, trackingId);
            string unsubscribeLink = string.Format("{0}/unsubscribe/{1}", serverPath, contactId);

            return new AutomailerModel
            {
                AutomailerModelId = Guid.NewGuid(),
                Email = email,
                Subject = subject,
                Content = content,
                TrackingLink = trackingLink,
                UnsubscribeLink = unsubscribeLink,
                Name = name,
                Text1 = text1,
                Text2 = text2,
                Text3 = text3
            };
        }

        
    }
}
