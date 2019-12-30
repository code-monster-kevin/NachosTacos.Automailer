using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NachosTacos.Automailer.Api.Services;
using NachosTacos.Automailer.Api.ViewModels;
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
    public class EmailController : ControllerBase
    {
        #region "Constructors"
        private readonly ILogger<EmailController> _logger;
        private readonly IAutomailerContext _automailerContext;
        static readonly byte[] TrackingGif = { 0x47, 0x49, 0x46, 0x38, 0x39, 0x61, 0x1, 0x0, 0x1, 0x0, 0x80, 0x0, 0x0, 0xff, 0xff, 0xff, 0x0, 0x0, 0x0, 0x2c, 0x0, 0x0, 0x0, 0x0, 0x1, 0x0, 0x1, 0x0, 0x0, 0x2, 0x2, 0x44, 0x1, 0x0, 0x3b };


        public EmailController(IAutomailerContext automailerContext, ILogger<EmailController> logger)
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

        [HttpPost]
        [Route("send")]
        public IActionResult ActivateEmailTask(Guid id)
        {
            AutomailerTask automailerTask = GetAutomailerTask(id);
            if (automailerTask == null)
            {
                return NotFound();
            }

            BackgroundJob.Schedule<EmailService>(x => x.SendEmail(automailerTask), TimeSpan.FromSeconds(1));

            return Ok();
        }


        #endregion
        
        private AutomailerTask GetAutomailerTask(Guid Id)
        {
            return new AutomailerTask();
        }

    }
}