using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NachosTacos.Automailer.Api.Services;
using NachoTacos.Automailer.Data;
using NachoTacos.Automailer.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NachosTacos.Automailer.Api.Controllers
{
    [ApiController]
    public class TrackingController : ControllerBase
    {
        #region "Constructors"
        static readonly byte[] TrackingGif = { 0x47, 0x49, 0x46, 0x38, 0x39, 0x61, 0x1, 0x0, 0x1, 0x0, 0x80, 0x0, 0x0, 0xff, 0xff, 0xff, 0x0, 0x0, 0x0, 0x2c, 0x0, 0x0, 0x0, 0x0, 0x1, 0x0, 0x1, 0x0, 0x0, 0x2, 0x2, 0x44, 0x1, 0x0, 0x3b };
        private readonly ILogger<TrackingController> _logger;
        private readonly IAutomailerContext _automailerContext;

        public TrackingController(IAutomailerContext automailerContext, ILogger<TrackingController> logger)
        {
            _logger = logger;
            _automailerContext = automailerContext;
        }
        #endregion


        #region "Controllers"
        /// <summary>
        /// Tracking API that returns a 1x1 pixel gif
        /// </summary>
        /// <param name="id">Tracking ID</param>
        /// <returns>byte array image file</returns>
        [HttpGet]
        [Route("pix/{id}/pixel.gif")]
        public IActionResult GetPix(Guid id)
        {
            BackgroundJob.Enqueue<TrackActivity>(x => x.SaveTracking(id));
            return File(TrackingGif, "image/gif");
        }

        /// <summary>
        /// Unsubscribe the contact from further email communication
        /// </summary>
        /// <param name="id">ContactId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("unsubscribe/{id}")]
        public async Task<IActionResult> Unsubscribe(Guid id)
        {
            try
            {
                Contact contact = _automailerContext.Contacts.FirstOrDefault(x => x.ContactId == id);
                if (contact == null)
                    return NotFound(id);

                contact.Unsubscribe = true;
                _automailerContext.Contacts.Update(contact);
                await _automailerContext.SaveChangesAsync();

                return Ok(contact);
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