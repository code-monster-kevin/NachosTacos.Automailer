using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NachoTacos.Automailer.Domain
{
    public class AutomailerTask : Updateable
    {
        [Key]
        public Guid AutomailerTaskId { get; set; }
        public ICollection<AutomailerModel> AutomailerModels { get; set; }
        public bool IsCompleted { get; set; }

        public static AutomailerTask Create(ICollection<AutomailerModel> models)
        {
            return new AutomailerTask
            {
                AutomailerTaskId = Guid.NewGuid(),
                AutomailerModels = models,
                IsCompleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };
        }
    }
}
