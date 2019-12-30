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
    public class EmailTemplateController : ControllerBase
    {
        #region "Constructors"
        private readonly ILogger<EmailTemplateController> _logger;
        private readonly IAutomailerContext _automailerContext;

        public EmailTemplateController(IAutomailerContext automailerContext, ILogger<EmailTemplateController> logger)
        {
            _logger = logger;
            _automailerContext = automailerContext;
        }
        #endregion

        #region "Controllers"
        [HttpGet]
        public IActionResult GetEmailTemplates()
        {
            try
            {
                List<EmailTemplate> emailTemplates = _automailerContext.EmailTemplates.OrderBy(x => x.UpdatedDate).ToList();
                if (emailTemplates.Count == 0) return NotFound();

                return Ok(emailTemplates);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetEmailTemplateById(Guid id)
        {
            try
            {
                EmailTemplate emailTemplate = _automailerContext.EmailTemplates.FirstOrDefault(x => x.EmailTemplateId == id);
                if (emailTemplate == null) return NotFound(id);

                return Ok(emailTemplate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmailTemplate(string from, string subject, string content)
        {
            try
            {
                EmailTemplate emailTemplate = EmailTemplate.Create(from, subject, content);
                _automailerContext.EmailTemplates.Add(emailTemplate);
                await _automailerContext.SaveChangesAsync();

                return Ok(emailTemplate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }
        #endregion
    }
}