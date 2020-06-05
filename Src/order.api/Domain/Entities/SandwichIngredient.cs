using order.api.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace order.api.Domain.Entities
{
    public class SandwichIngredient : DeletableEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid IngredientId { get; set; }

        public Ingredient Ingredient { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
