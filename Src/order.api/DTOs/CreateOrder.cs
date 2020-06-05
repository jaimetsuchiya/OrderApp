using order.api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace order.api.DTOs
{
    public class CreateOrder
    {
        [Required]
        public int TableNumber { get; set; }

        [Required]
        public List<CreateOrderSandwich> Sandwiches { get; set; }

        public class CreateOrderSandwich
        {
            [Required]
            public Guid SandwichId { get; set; }

            [Required]
            public int Quantity { get; set; }

            public List<CreateOrderSandwichAdditionalIngredient> AdditionalIngredients { get; set; }
        }

        public class CreateOrderSandwichAdditionalIngredient
        {
            [Required]
            public Guid IngredientId { get; set; }

            [Required]
            public int Quantity { get; set; }
        }
    }
}
