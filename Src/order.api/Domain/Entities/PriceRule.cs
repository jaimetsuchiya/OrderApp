using order.api.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace order.api.Domain.Entities
{
    public class PriceRule: DeletableEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public PriceRuleTypeEnum PriceRuleType { get; set; }

        [Required]
        public decimal Value { get; set; }

        [Required]
        public PriceRuleValueTypeEnum PriceRuleValueType { get; set; }

        [Required]
        public RuleTypeEnum RuleType { get; set; }

        public int? RuleValue { get; set; }
    }
}
