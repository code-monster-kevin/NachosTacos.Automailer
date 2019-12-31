using System;
using System.ComponentModel.DataAnnotations;

namespace NachoTacos.Automailer.Domain
{
    /// <summary>
    /// For tracking all emails being sent out
    /// A new tracking id is created everytime a new email is sent out
    /// </summary>
    public class CampaignTracking
    {
        [Key]
        public Guid CampaignTrackingId { get; protected set; }
        public Guid CampaignSettingId { get; protected set; }
        public Guid EmailTemplateId { get; protected set; }
        public Guid ContactId { get; protected set; }
        public DateTime SentDate { get; protected set; }

        public static CampaignTracking Create(Guid campaignSettingId, Guid emailTemplateId, Guid contactId)
        {
            return new CampaignTracking
            {
                CampaignTrackingId = Guid.NewGuid(),
                CampaignSettingId = campaignSettingId,
                EmailTemplateId = emailTemplateId,
                ContactId = contactId,
                SentDate = DateTime.UtcNow
            };
        }
    }
}
