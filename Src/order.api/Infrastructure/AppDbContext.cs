using Microsoft.EntityFrameworkCore;
using order.api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace order.api.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Sandwich> Sandwiches { get; set; }
        public DbSet<PriceRule> PriceRules { get; set; }
        public DbSet<Order> Orders { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PriceRule>().HasData
            (
                new PriceRule { Id = Guid.NewGuid(), Description = "Desconto para pedidos com 2 lanches", PriceRuleType = Domain.Common.PriceRuleTypeEnum.Discount, Value = 3, PriceRuleValueType = Domain.Common.PriceRuleValueTypeEnum.Percentage, Created = DateTime.Today, CreatedBy = "System" },
                new PriceRule { Id = Guid.NewGuid(), Description = "Desconto para pedidos com 3 lanches", PriceRuleType = Domain.Common.PriceRuleTypeEnum.Discount, Value = 5, PriceRuleValueType = Domain.Common.PriceRuleValueTypeEnum.Percentage, Created = DateTime.Today, CreatedBy = "System" },
                new PriceRule { Id = Guid.NewGuid(), Description = "Desconto para pedidos acima de 5 lanches", PriceRuleType = Domain.Common.PriceRuleTypeEnum.Discount, Value = 10, PriceRuleValueType = Domain.Common.PriceRuleValueTypeEnum.Percentage, Created = DateTime.Today, CreatedBy = "System" },
                new PriceRule { Id = Guid.NewGuid(), Description = "Taxa de Entrega", PriceRuleType = Domain.Common.PriceRuleTypeEnum.AdditionalCharge, Value = 10, PriceRuleValueType = Domain.Common.PriceRuleValueTypeEnum.Percentage, Created = DateTime.Today, CreatedBy = "System", Deleted = true, DeletedAt = DateTime.Today, DeletedBy = "System" }
            );
        }
    }
}
