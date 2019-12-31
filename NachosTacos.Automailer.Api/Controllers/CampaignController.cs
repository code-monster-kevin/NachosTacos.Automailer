using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NachoTacos.Automailer.Data;
using NachoTacos.Automailer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NachosTacos.Automailer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        #region "Constructors"
        private readonly ILogger<CampaignController> _logger;
        private readonly IAutomailerContext _automailerContext;

        public CampaignController(IAutomailerContext automailerContext, ILogger<CampaignController> logger)
        {
            _logger = logger;
            _automailerContext = automailerContext;
        }
        #endregion

        #region "Controllers"
        [HttpGet]
        public IActionResult GetCampaigns()
        {
            try
            {
                List<Campaign> campaigns = _automailerContext.Campaigns.OrderBy(x => x.UpdatedDate).ToList();
                if (campaigns.Count == 0) return NotFound();

                return Ok(campaigns);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetCampaigns(Guid id)
        {
            try
            {
                Campaign campaign = _automailerContext.Campaigns.FirstOrDefault(x => x.CampaignId == id);
                if (campaign == null) return NotFound(id);

                return Ok(campaign);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCampaign(string code, string description)
        {
            try
            {
                Campaign campaign = Campaign.Create(code, description);
                _automailerContext.Campaigns.Add(campaign);
                await _automailerContext.SaveChangesAsync();

                return Ok(campaign);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Route("contacts")]
        public async Task<IActionResult> AddContactToCampaign(Guid campaignid, Guid contactid)
        {
            try
            {
                if (_automailerContext.Campaigns.FirstOrDefault(x => x.CampaignId == campaignid) == null)
                    return NotFound(campaignid);

                if (_automailerContext.Contacts.FirstOrDefault(x => x.ContactId == contactid) == null)
                    return NotFound(contactid);

                CampaignContact campaignContact = CampaignContact.Create(campaignid, contactid);
                _automailerContext.CampaignContacts.Add(campaignContact);
                await _automailerContext.SaveChangesAsync();
                return Ok(campaignContact);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Route("settings")]
        public async Task<IActionResult> CreateCampaignSettings(Guid campaignId, Guid templateId, int emailDay)
        {
            try
            {
                CampaignSetting campaignSetting = CampaignSetting.Create(campaignId, templateId, emailDay);

                _automailerContext.CampaignSettings.Add(campaignSetting);
                await _automailerContext.SaveChangesAsync();

                return Ok(campaignSetting);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }

        }

        /// <summary>
        /// Activate or De-activate the campaign
        /// </summary>
        /// <param name="id">Campaign Setting Id</param>
        /// <param name="active">true/false</param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/activate/{id}")]
        public async Task<IActionResult> ActivateCampaign(Guid id, bool active)
        {
            try
            {
                CampaignSetting campaignSetting = _automailerContext.CampaignSettings.FirstOrDefault(x => x.CampaignSettingId == id);
                if (campaignSetting == null) return NotFound(id);
                campaignSetting.Active = active;
                _automailerContext.CampaignSettings.Update(campaignSetting);
                await _automailerContext.SaveChangesAsync();

                return Ok(campaignSetting);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }
        #endregion
    }
}