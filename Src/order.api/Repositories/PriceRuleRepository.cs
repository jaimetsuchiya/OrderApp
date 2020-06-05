using order.api.Domain.Entities;
using order.api.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace order.api.Repositories
{
    public interface IPriceRuleRepository : IRepository<PriceRule>
    {
    }
    public class PriceRuleRepository : Repository<PriceRule>, IPriceRuleRepository
    {
        public PriceRuleRepository(AppDbContext context) : base(context)
        {
        }
    }
}
