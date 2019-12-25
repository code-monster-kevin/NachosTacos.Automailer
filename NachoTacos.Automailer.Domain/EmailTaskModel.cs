using System;
using System.Collections.Generic;
using System.Text;

namespace NachoTacos.Automailer.Domain
{
    public class EmailTaskModel
    {
        public Guid EmailTaskId { get; set; }
        public Guid EmailModelId { get; set; }
        public virtual EmailTask EmailTask { get; set; }
        public virtual EmailModel EmailModel { get; set; }
    }
}
