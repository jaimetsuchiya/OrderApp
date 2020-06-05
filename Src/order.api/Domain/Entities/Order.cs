using order.api.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace order.api.Domain.Entities
{
    public class Order: AuditableEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public decimal Total { get; internal set; }

        public decimal TotalItens { get; internal set; }

        [Required]
        public int TableNumber { get; set; }

        [Required]
        public IList<OrderSandwich> Sandwiches { get; set; }

        public IList<OrderPriceRule> Rules { get; set; }

        public OrderStatusEnum Status { get; set; }
    }

}
