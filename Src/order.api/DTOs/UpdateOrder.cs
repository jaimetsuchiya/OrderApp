using order.api.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace order.api.DTOs
{
    public class UpdateOrder
    {
        public Guid OrderId { get; set; }
        public OrderStatusEnum Status { get; set; }
    }
}
