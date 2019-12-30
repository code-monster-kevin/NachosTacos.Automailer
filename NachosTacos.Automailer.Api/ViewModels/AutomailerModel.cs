namespace NachosTacos.Automailer.Api.ViewModels
{
    public class AutomailerModel
    {
        public virtual string Email { get; protected set; }
        public virtual string Subject { get; protected set; }
        public virtual string Content { get; protected set; }
        public virtual string TrackingLink { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual string Text1 { get; protected set; }
        public virtual string Text2 { get; protected set; }
        public virtual string Text3 { get; protected set; }

        public static AutomailerModel Create(string trackingPath, string trackingId, string email, string subject, string content, string name = "", string text1 = "", string text2 = "", string text3 = "")
        {
            string trackingLink = string.Format("<img src='{0}/pix/{1}/pixel.gif' />", trackingPath, trackingId);

            return new AutomailerModel
            {
                Email = email,
                Subject = subject,
                Content = content,
                TrackingLink = trackingLink,
                Name = name,
                Text1 = text1,
                Text2 = text2,
                Text3 = text3
            };
        }
    }
}
