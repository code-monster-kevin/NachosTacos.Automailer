using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NachoTacos.Automailer.Domain;

namespace NachosTacos.Automailer.Api.ViewModels
{
    public class AutomailerTask
    {
        public Guid AutomailerTaskId { get; set; }
        public ICollection<AutomailerModel> AutomailerModels { get; set; }
    }
}
