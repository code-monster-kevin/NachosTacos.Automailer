using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NachoTacos.Automailer.Data;
using NachoTacos.Automailer.Domain;

namespace NachosTacos.Automailer.Api.Services
{
    public class TrackActivity
    {
        private readonly IAutomailerContext _automailerContext;
        private readonly ILogger<TrackActivity> _logger;

        public TrackActivity(IAutomailerContext automailerContext, ILogger<TrackActivity> logger)
        {
            _automailerContext = automailerContext;
            _logger = logger;
        }

        public async Task SaveTracking(Guid id)
        {
            try
            {
                _automailerContext.CampaignActivities.Add(CampaignActivity.Create(id));
                await _automailerContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
