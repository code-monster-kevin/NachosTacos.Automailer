using System;

namespace NachoTacos.Automailer.Domain
{
    /// <summary>
    /// A new activitiy is created everytime the email is opened (pixel.gif)
    /// </summary>
    public class CampaignActivity : Updateable
    {
        public Guid CampaignActivityId { get; protected set; }
        public Guid CampaignTrackingId { get; protected set; }

        public static CampaignActivity Create(Guid trackingId)
        {
            return new CampaignActivity
            {
                CampaignActivityId = Guid.NewGuid(),
                CampaignTrackingId = trackingId,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };
        }
    }
}
