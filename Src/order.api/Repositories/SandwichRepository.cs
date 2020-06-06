using Microsoft.EntityFrameworkCore;
using order.api.Domain.Entities;
using order.api.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace order.api.Repositories
{
    public interface ISandwichRepository : IRepository<Sandwich>
    {
    }
    public class SandwichRepository : Repository<Sandwich>, ISandwichRepository
    {
        public SandwichRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Sandwich>> ListAsync()
        {
            return await _context.Sandwiches.AsNoTracking()
                                            .Include(s => s.Ingredients)
                                            .ThenInclude(i=>i.Ingredient).ToListAsync();
        }

        public override async Task<Sandwich> FindByIdAsync(Guid id)
        {
            return await _context.Sandwiches.Include(s=>s.Ingredients).ThenInclude(i => i.Ingredient).FirstOrDefaultAsync(x=>x.Id == id);
        }
    }
}
