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
    }
}
