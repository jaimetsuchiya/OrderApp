using order.api.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace order.api.Domain.Entities
{
    public class Sandwich: DeletableEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public virtual IList<SandwichIngredient> Ingredients { get; internal set; }

        [Required]
        public decimal Price 
        { 
            get { return this.Ingredients == null ? 0M : Ingredients.Where(i=> i.Ingredient != null && !i.Deleted.HasValue || !i.Deleted.Value).Select(i => i.Ingredient.Price * i.Quantity).Sum(); } 
        }

    }
}
