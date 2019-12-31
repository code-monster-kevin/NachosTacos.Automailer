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
    public class ContactController : ControllerBase
    {
        #region "Constructors"
        private readonly ILogger<ContactController> _logger;
        private readonly IAutomailerContext _automailerContext;

        public ContactController(IAutomailerContext automailerContext, ILogger<ContactController> logger)
        {
            _logger = logger;
            _automailerContext = automailerContext;
        }
        #endregion

        #region "Controllers"
        [HttpGet]
        public IActionResult GetContacts()
        {
            try
            {
                List<Contact> contacts = _automailerContext.Contacts.OrderBy(x => x.UpdatedDate).ToList();
                if (contacts.Count == 0) return NotFound();

                return Ok(contacts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{email}")]
        public IActionResult GetContactByEmail(string email)
        {
            try
            {
                Contact contact = _automailerContext.Contacts.FirstOrDefault(x => x.Email == email);
                if (contact == null) return NotFound(email);

                return Ok(contact);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Creates a new contact if the email doesn't exist.
        /// Otherwise, it will return the contact with the same email
        /// </summary>
        /// <param name="source">Where did this contact come from</param>
        /// <param name="email">email address</param>
        /// <param name="name">full name</param>
        /// <param name="mobile">mobile number</param>
        /// <param name="identityNo">identity number or passport</param>
        /// <param name="nationality">country of citizenship</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateContact(string source, string email, string name, string mobile, string identityNo, string nationality)
        {
            try
            {
                Contact contact = _automailerContext.Contacts.FirstOrDefault(x => x.Email == email);
                if (contact == null)
                {
                    contact = Contact.Create(source, email, name, mobile, identityNo, nationality);
                    _automailerContext.Contacts.Add(contact);
                    await _automailerContext.SaveChangesAsync();
                }
                return Ok(contact);
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