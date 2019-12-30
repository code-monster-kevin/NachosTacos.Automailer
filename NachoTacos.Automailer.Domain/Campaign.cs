using System;
using System.ComponentModel.DataAnnotations;

namespace NachoTacos.Automailer.Domain
{
    /// <summary>
    /// The main campaign domain
    /// Code = Campaign identifier key
    /// Description = More details about the campaign
    /// </summary>
    public class Campaign : Updateable
    {
        public virtual Guid CampaignId { get; protected set; }
        [Key]
        public virtual string Code { get; set; }
        public virtual string Description { get; protected set; }

        public static Campaign Create(string code, string description = null)
        {
            return Create(Guid.NewGuid(), code, description);
        }

        public static Campaign Create(Guid id, string code, string description)
        {
            if (string.IsNullOrEmpty(code)) throw new ArgumentNullException("code");

            return new Campaign
            {
                CampaignId = id,
                Code = code,
                Description = description,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };
        }
    }
}
