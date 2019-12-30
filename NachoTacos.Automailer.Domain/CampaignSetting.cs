using System;

namespace NachoTacos.Automailer.Domain
{
    /// <summary>
    /// Simple scheduler to determine when the emails are to be sent
    /// CampaignId = Foreign key to the Campaign model
    /// EmailTemplateId = Foreign key to the EmailTemplate model
    /// SendDayAfterAdded = send the email after x days the contact was added to the campaigncontact list.
    /// </summary>
    public class CampaignSetting
    {
        public Guid CampaignSettingId { get; set; }
        public Guid CampaignId { get; set; }
        public Guid EmailTemplateId { get; set; }
        public int SendDayAfterAdded { get; set; }
    }
}
