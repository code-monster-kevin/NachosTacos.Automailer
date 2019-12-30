using System;
using System.Collections.Generic;
using System.Text;

namespace NachoTacos.Automailer.Domain
{
    /// <summary>
    /// The contacts linked to this campaign
    /// CampaignId = Foreign key to Campaign model
    /// Email = Foreign key to Contact model
    /// CreatedDate = The date the contact was added to the campaign - this date is used to determine when to send the communication
    /// </summary>
    public class CampaignContact : Updateable
    {
        public Guid CampaignId { get; set; }
        public Guid ContactId { get; set; }
    }
}
