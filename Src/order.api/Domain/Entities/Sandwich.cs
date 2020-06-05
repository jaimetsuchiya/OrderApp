using order.api.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace order.api.Domain.Entities
{
    public class Sandwich: DeletableEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public IList<SandwichIngredient> Ingredients { get; internal set; }

        [Required]
        public decimal Price 
        { 
            get { return Ingredients.Where(i=>!i.Deleted.HasValue || !i.Deleted.Value).Select(i => i.Ingredient.Price * i.Quantity).Sum(); } 
        }

    }
}
