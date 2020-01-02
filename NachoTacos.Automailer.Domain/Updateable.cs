using System;

namespace NachoTacos.Automailer.Domain
{
    public class Updateable : IUpdateable
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
