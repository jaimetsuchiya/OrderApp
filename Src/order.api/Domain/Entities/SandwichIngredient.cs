using order.api.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace order.api.Domain.Entities
{
    public class SandwichIngredient : DeletableEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid SandwichId { get; set; }

        [Required]
        public Guid IngredientId { get; set; }

        [ForeignKey("IngredientId")]
        public virtual Ingredient Ingredient { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
