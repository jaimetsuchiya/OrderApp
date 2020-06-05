using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace order.api.DTOs
{
    public class UpdateSandwich: CreateSandwich
    {
        [Required]
        public Guid SandwichId { get; set; }

        public bool? Deleted { get; set; }
    }
}
