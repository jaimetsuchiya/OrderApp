using order.api.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace order.api.Domain.Entities
{
    public class OrderSandwichIngredient: DeletableEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid OrderSandwichId { get; set; }

        [Required]
        public Guid IngredientId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public virtual Ingredient Ingredient { get; set; }
    }
}
