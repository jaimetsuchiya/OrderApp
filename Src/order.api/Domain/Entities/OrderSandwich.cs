using order.api.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("OrderSandwichId")]
        [Required]
        public virtual IList<OrderSandwichIngredient> AdditionalIngredients { get; set; }

        [ForeignKey("SandwichId")]
        public virtual Sandwich Sandwich { get; set; }
    }
}
