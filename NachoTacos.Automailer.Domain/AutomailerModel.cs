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
            string trackingLink = string.Format("<img src='{0}/pix/{1}/pixel.gif' />", serverPath, trackingId);
            string unsubscribeLink = string.Format("<a href='{0}/unsubscribe/{1}'>Unsubscribe</a>", serverPath, contactId);

            return new AutomailerModel
            {
                AutomailerModelId = Guid.NewGuid(),
                Email = email,
                Subject = subject,
                Content = ContentTracking(content, trackingLink, unsubscribeLink),
                TrackingLink = trackingLink,
                UnsubscribeLink = unsubscribeLink,
                Name = name,
                Text1 = text1,
                Text2 = text2,
                Text3 = text3
            };
        }

        /// <summary>
        /// Adds a @Model.TrackingLink and @Model.UnsubscribeLink to the template if it doesn't exist
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private static string ContentTracking(string content, string trackingLink, string unsubscribeLink)
        {
            content += string.Format("<br />{0}{1}", unsubscribeLink, trackingLink);
            return content;
        }
    }
}
