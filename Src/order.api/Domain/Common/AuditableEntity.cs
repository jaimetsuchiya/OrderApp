using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace order.api.Domain.Common
{
    public class AuditableEntity
    {
        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModified { get; set; }
    }
}
