using System;
using System.Collections.Generic;
using System.Text;

namespace NachoTacos.Automailer.Domain
{
    /// <summary>
    /// For tracking all emails being sent out
    /// A new tracking id is created everytime a new email is sent out
    /// </summary>
    public class CampaignTracking
    {
        public Guid CampaignTrackingId { get; protected set; }
        public Guid CampaignId { get; protected set; }
        public Guid EmailTemplateId { get; protected set; }
        public Guid EmailModelId { get; protected set; }
        public DateTime SentDate { get; protected set; }

        public static CampaignTracking Create(Guid campaignId, Guid emailTemplateId, Guid emailModelId)
        {
            return new CampaignTracking
            {
                CampaignTrackingId = Guid.NewGuid(),
                CampaignId = campaignId,
                EmailTemplateId = emailTemplateId,
                EmailModelId = emailModelId,
                SentDate = DateTime.UtcNow
            };
        }
    }
}
