using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace order.api.Domain.Common
{
    public enum OrderStatusEnum
    {
        NewOrder = 1,
        Awaiting = 2,
        InProgress = 3,
        Produced = 4,
        Delivered = 5,
        Finished = 9,
        Canceled = 10
    }
}
