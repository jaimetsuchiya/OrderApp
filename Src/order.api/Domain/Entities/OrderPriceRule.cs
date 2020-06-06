using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace order.api.Domain.Entities
{
    public class OrderPriceRule
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public Guid PriceRuleId { get; set; }

        [Required]
        public decimal Value { get; set; }

        public PriceRule PriceRule { get; set; }
    }
}
