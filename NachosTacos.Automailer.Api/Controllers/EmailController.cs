using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NachosTacos.Automailer.Api.Services;
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

        public EmailController(IAutomailerContext automailerContext, ILogger<EmailController> logger)
        {
            _logger = logger;
            _automailerContext = automailerContext;
        }

        #endregion



        #region "Controllers"
        /// <summary>
        /// Lists the email tasks by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("task/{id}")]
        public IActionResult GetEmailTaskById(Guid id)
        {
            try
            {
                AutomailerTask automailerTask = _automailerContext.AutomailerTasks
                                                                  .Include(x => x.AutomailerModels)
                                                                  .FirstOrDefault(x => x.AutomailerTaskId == id);
                return Ok(automailerTask);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Executes the email task. This will send out the emails.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("execute/{id}")]
        public IActionResult ActivateEmailTask(Guid id)
        {
            try
            {
                if (GetAutomailerTask(id) == null)
                {
                    return NotFound();
                }

                BackgroundJob.Enqueue<EmailService>(x => x.SendEmail(id));

                return Ok();
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("generate")]
        public IActionResult GenerateEmailTasks()
        {
            try
            {
                BackgroundJob.Enqueue<EmailService>(x => x.CreateMailTask());
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }
        #endregion
        
        private AutomailerTask GetAutomailerTask(Guid id)
        {
            return _automailerContext.AutomailerTasks.FirstOrDefault(x => x.AutomailerTaskId == id);
        }

    }
}