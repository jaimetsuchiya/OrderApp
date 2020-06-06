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
        public DbSet<OrderPriceRule> OrderPriceRules { get; set; }
        public DbSet<OrderSandwich> OrderSandwiches { get; set; }
        public DbSet<OrderSandwichIngredient> OrderSandwicheIngredients { get; set; }
        public DbSet<SandwichIngredient> SandwichIngredients { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Sandwich>().HasMany(p => p.Ingredients);
            modelBuilder.Entity<SandwichIngredient>().HasOne(p => p.Ingredient);
            modelBuilder.Entity<Order>().HasMany(p => p.Rules);
            modelBuilder.Entity<Order>().HasMany(p => p.Sandwiches);
            
            modelBuilder.Entity<OrderSandwich>().HasOne(p => p.Sandwich);
            modelBuilder.Entity<OrderSandwich>().HasMany(p => p.AdditionalIngredients);
            modelBuilder.Entity<OrderSandwichIngredient>().HasOne(p => p.Ingredient);
            modelBuilder.Entity<OrderPriceRule>().HasOne(p => p.PriceRule);
        }
    }
}
