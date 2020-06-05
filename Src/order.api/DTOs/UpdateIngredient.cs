using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace order.api.DTOs
{
    public class UpdateIngredient: CreateIngredient
    {
        [Required]
        public Guid IngredientId { get; set; }

        public bool? Deleted { get; set; }
    }
}
