using Microsoft.EntityFrameworkCore;
using order.api.Domain.Entities;
using order.api.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace order.api.Repositories
{
    public interface IOrderRepository: IRepository<Order>
    {
    }

    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Order>> ListAsync()
        {
            return await _context.Orders.AsNoTracking()
                                            .Include(o => o.Rules).ThenInclude(r => r.PriceRule)
                                            .Include(o => o.Sandwiches).ThenInclude(s => s.Sandwich).ThenInclude(o =>o.Ingredients)
                                            .Include(o => o.Sandwiches).ThenInclude(s=>s.AdditionalIngredients).ThenInclude(i=>i.Ingredient)
                                            .ToListAsync();
        }

        public override async Task<Order> FindByIdAsync(Guid id)
        {
            return await _context.Orders
                                    .Include(o => o.Rules).ThenInclude(r => r.PriceRule)
                                    .Include(o => o.Sandwiches).ThenInclude(s => s.Sandwich).ThenInclude(o => o.Ingredients)
                                    .Include(o => o.Sandwiches).ThenInclude(s => s.AdditionalIngredients).ThenInclude(i => i.Ingredient)
                                    .FirstOrDefaultAsync(x => x.Id == id);
                                            
        }
    }
}
