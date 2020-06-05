using order.api.Domain.Entities;
using order.api.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace order.api.Repositories
{
    public interface IOrderSandwichRepository: IRepository<OrderSandwich>
    {
    }

    public class OrderSandwichRepository : Repository<OrderSandwich>, IOrderSandwichRepository
    {
        public OrderSandwichRepository(AppDbContext context) : base(context)
        {
        }
    }
}
