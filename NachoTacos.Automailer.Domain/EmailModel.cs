using System;
using System.ComponentModel.DataAnnotations;

namespace NachoTacos.Automailer.Domain
{
    public class EmailModel
    {
        public Guid EmailModelId { get; set; }

        [Required]
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
