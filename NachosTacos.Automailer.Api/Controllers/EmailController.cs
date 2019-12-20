using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NachosTacos.Automailer.Api.Services;
using FluentEmail.Core;

namespace NachosTacos.Automailer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;

        public EmailController(ILogger<EmailController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<OkResult> Get([FromServices] IFluentEmailFactory emailFactory)
        {
            EmailService emailService = new EmailService(emailFactory);
            await emailService.Send(Guid.NewGuid());

            return Ok();
        }
    }
}