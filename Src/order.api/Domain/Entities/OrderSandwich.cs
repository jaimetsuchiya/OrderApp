using order.api.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace order.api.Domain.Entities
{
    public class OrderSandwich: DeletableEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public Guid SandwichId { get; set; }

        [Required]
        public int Position { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public IList<OrderSandwichIngredient> AdditionalIngredients { get; set; }

        public Sandwich Sandwich { get; set; }

    }
}
