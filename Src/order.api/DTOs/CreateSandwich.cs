using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace order.api.DTOs
{
    public class CreateSandwich
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public List<CreateSandwichIngredient> Ingredients { get; set; }

        public class CreateSandwichIngredient
        {
            [Required]
            public Guid IngredientId { get; set; }

            [Required]
            public int Quantity { get; set; }
        }
    }
}
