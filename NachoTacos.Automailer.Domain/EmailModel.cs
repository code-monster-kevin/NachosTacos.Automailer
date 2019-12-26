using System;
using System.ComponentModel.DataAnnotations;

namespace NachoTacos.Automailer.Domain
{
    public class EmailModel : Updateable
    {
        public Guid EmailModelId { get; set; }

        [Required]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string Text3 { get; set; }
    }
}
