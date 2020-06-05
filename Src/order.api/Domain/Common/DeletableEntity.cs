using System;
using System.Collections.Generic;
using System.Text;

namespace order.api.Domain.Common
{
    public class DeletableEntity: AuditableEntity
    {
        public bool? Deleted { get; set; }

        public string DeletedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

    }
}
