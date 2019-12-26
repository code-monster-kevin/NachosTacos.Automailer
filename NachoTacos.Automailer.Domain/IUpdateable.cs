using System;

namespace NachoTacos.Automailer.Domain
{
    public interface IUpdateable
    {
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
    }
}
