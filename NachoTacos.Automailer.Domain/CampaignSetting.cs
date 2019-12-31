using System;
using System.ComponentModel.DataAnnotations;

namespace NachoTacos.Automailer.Domain
{
    /// <summary>
    /// Simple scheduler to determine when the emails are to be sent
    /// CampaignId = Foreign key to the Campaign model
    /// EmailTemplateId = Foreign key to the EmailTemplate model
    /// SendDayAfterAdded = send the email after x days the contact was added to the campaigncontact list.
    /// </summary>
    public class CampaignSetting : Updateable
    {
        [Key]
        public Guid CampaignSettingId { get; protected set; }
        public Guid CampaignId { get; protected set; }
        public Guid EmailTemplateId { get; protected set; }
        public int SendAfterJoined { get; set; }
        public bool Active { get; set; }

        public static CampaignSetting Create(Guid campaignId, Guid emailTemplateId, int day)
        {
            return new CampaignSetting
            {
                CampaignSettingId = Guid.NewGuid(),
                CampaignId = campaignId,
                EmailTemplateId = emailTemplateId,
                SendAfterJoined = day,
                Active = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };
        }
    }
}
