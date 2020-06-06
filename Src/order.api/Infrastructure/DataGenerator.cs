using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using order.api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace order.api.Infrastructure
{
    public class DataGenerator
    {
        public static void Initialize(AppDbContext context)
        {
            var prices = new PriceRule[]
        {
            new PriceRule { Id = Guid.NewGuid(),
                            Description = "Desconto para pedidos com 2 lanches",
                            PriceRuleType = Domain.Common.PriceRuleTypeEnum.Discount,
                            Value = 3,
                            PriceRuleValueType = Domain.Common.PriceRuleValueTypeEnum.Percentage,
                            RuleType = Domain.Common.RuleTypeEnum.SandwichQuantity,
                            RuleValue = 2,
                            Created = DateTime.Today,
                            CreatedBy = "System" },
            new PriceRule { Id = Guid.NewGuid(),
                            Description = "Desconto para pedidos com 3 lanches",
                            PriceRuleType = Domain.Common.PriceRuleTypeEnum.Discount,
                            Value = 5,
                            PriceRuleValueType = Domain.Common.PriceRuleValueTypeEnum.Percentage,
                            RuleType = Domain.Common.RuleTypeEnum.SandwichQuantity,
                            RuleValue = 3,
                            Created = DateTime.Today,
                            CreatedBy = "System" },
            new PriceRule { Id = Guid.NewGuid(),
                            Description = "Desconto para pedidos acima de 5 lanches",
                            PriceRuleType = Domain.Common.PriceRuleTypeEnum.Discount,
                            Value = 10,
                            PriceRuleValueType = Domain.Common.PriceRuleValueTypeEnum.Percentage,
                            RuleType = Domain.Common.RuleTypeEnum.SandwichQuantity,
                            RuleValue = 5,
                            Created = DateTime.Today,
                            CreatedBy = "System" },
            new PriceRule { Id = Guid.NewGuid(),
                            Description = "Taxa de Entrega",
                            PriceRuleType = Domain.Common.PriceRuleTypeEnum.AdditionalCharge,
                            Value = 10,
                            PriceRuleValueType = Domain.Common.PriceRuleValueTypeEnum.Percentage,
                            RuleType = Domain.Common.RuleTypeEnum.None,
                            RuleValue = 10,
                            Created = DateTime.Today,
                            CreatedBy = "System",
                            Deleted = true,
                            DeletedAt = DateTime.Today,
                            DeletedBy = "System" }
        };
            var ingredients = new Ingredient[]
                {
                new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = "Hamburger clássico 160g grelhado",
                    Cost = 3M,
                    Price = 4M,
                    Created = DateTime.Now,
                    CreatedBy = "System"
                },
                new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = "Picles de pimenta",
                    Cost = 0.5M,
                    Price = 1M,
                    Created = DateTime.Now,
                    CreatedBy = "System"
                },
                new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = "Queijo prato",
                    Cost = 0.25M,
                    Price = 0.5M,
                    Created = DateTime.Now,
                    CreatedBy = "System"
                },
                new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = "Maionses especial",
                    Cost = 0.10M,
                    Price = 0.25M,
                    Created = DateTime.Now,
                    CreatedBy = "System"
                },
                new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = "Alface",
                    Cost = 0.05M,
                    Price = 0.15M,
                    Created = DateTime.Now,
                    CreatedBy = "System"
                },
                new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = "Hamburger de salmão grelhado",
                    Cost = 4M,
                    Price = 5M,
                    Created = DateTime.Now,
                    CreatedBy = "System"
                },
                new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = "Pão de Hamburger",
                    Cost = 0.15M,
                    Price = 1.5M,
                    Created = DateTime.Now,
                    CreatedBy = "System"
                },
                    new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = "Cogumelo shitake",
                    Cost = 0.50M,
                    Price = 1.50M,
                    Created = DateTime.Now,
                    CreatedBy = "System"
                },
                new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = "Molho shoyo",
                    Cost = 0.05M,
                    Price = 0.15M,
                    Created = DateTime.Now,
                    CreatedBy = "System"
                },
                new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = "Catupiry",
                    Cost = 0.05M,
                    Price = 0.15M,
                    Created = DateTime.Now,
                    CreatedBy = "System"
                },
                new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = "Cebola",
                    Cost = 0.05M,
                    Price = 0.15M,
                    Created = DateTime.Now,
                    CreatedBy = "System"
                },
                new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = "Queijo Cheddar",
                    Cost = 0.05M,
                    Price = 0.15M,
                    Created = DateTime.Now,
                    CreatedBy = "System"
                },
                new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = "Hamburguer de Calabresa 160g grelhado",
                    Cost = 6M,
                    Price = 8M,
                    Created = DateTime.Now,
                    CreatedBy = "System"
                },
                new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = "Mussarela de búfala",
                    Cost = 1.5M,
                    Price = 3M,
                    Created = DateTime.Now,
                    CreatedBy = "System"
                },
                new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = "Rúcula",
                    Cost = 0.05M,
                    Price = 1M,
                    Created = DateTime.Now,
                    CreatedBy = "System"
                },
                new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = "Tomate seco",
                    Cost = 0.05M,
                    Price = 1M,
                    Created = DateTime.Now,
                    CreatedBy = "System"
                },
                new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = "Hamburguer Angus",
                    Cost = 7M,
                    Price = 9M,
                    Created = DateTime.Now,
                    CreatedBy = "System"
                }
                };
            var sandwiches = new Sandwich[]
                {
                new Sandwich
                {
                    Id = Guid.NewGuid(),
                    Name = "ALAGOAS BURGUER",
                    Created = DateTime.Now,
                    CreatedBy = "System"
                },
                new Sandwich
                {
                    Id = Guid.NewGuid(),
                    Name = "ITACOLOMI BURGER",
                    Created = DateTime.Now,
                    CreatedBy = "System"
                },
                new Sandwich
                {
                    Id = Guid.NewGuid(),
                    Name = "MATHIAS BURGER",
                    Created = DateTime.Now,
                    CreatedBy = "System"
                }
            };

            var sandwichIngredients = new SandwichIngredient[]
                {
                new SandwichIngredient
                {
                    Id = Guid.NewGuid(),
                    Created = DateTime.Now,
                    CreatedBy = "System",
                    IngredientId = ingredients.First(i=>i.Name=="Hamburger clássico 160g grelhado").Id,
                    Ingredient = ingredients.First(i=>i.Name=="Hamburger clássico 160g grelhado"),
                    SandwichId = sandwiches.First(s=>s.Name=="ALAGOAS BURGUER").Id,
                    //Sandwich =  sandwiches.First(s=>s.Name=="ALAGOAS BURGUER"),
                    Quantity = 1
                },
                new SandwichIngredient
                {
                    Id = Guid.NewGuid(),
                    Created = DateTime.Now,
                    CreatedBy = "System",
                    IngredientId = ingredients.First(i=>i.Name=="Picles de pimenta").Id,
                    Ingredient = ingredients.First(i=>i.Name=="Picles de pimenta"),
                    SandwichId = sandwiches.First(s=>s.Name=="ALAGOAS BURGUER").Id,
                    //Sandwich =  sandwiches.First(s=>s.Name=="ALAGOAS BURGUER"),
                    Quantity = 1
                },
                new SandwichIngredient
                {
                    Id = Guid.NewGuid(),
                    Created = DateTime.Now,
                    CreatedBy = "System",
                    IngredientId = ingredients.First(i=>i.Name=="Queijo prato").Id,
                    Ingredient = ingredients.First(i=>i.Name=="Queijo prato"),
                    SandwichId = sandwiches.First(s=>s.Name=="ALAGOAS BURGUER").Id,
                    //Sandwich =  sandwiches.First(s=>s.Name=="ALAGOAS BURGUER"),
                    Quantity = 1
                },
                new SandwichIngredient
                {
                    Id = Guid.NewGuid(),
                    Created = DateTime.Now,
                    CreatedBy = "System",
                    IngredientId = ingredients.First(i=>i.Name=="Maionses especial").Id,
                    Ingredient = ingredients.First(i=>i.Name=="Maionses especial"),
                    SandwichId = sandwiches.First(s=>s.Name=="ALAGOAS BURGUER").Id,
                    //Sandwich =  sandwiches.First(s=>s.Name=="ALAGOAS BURGUER"),
                    Quantity = 1
                },
                new SandwichIngredient
                {
                    Id = Guid.NewGuid(),
                    Created = DateTime.Now,
                    CreatedBy = "System",
                    IngredientId = ingredients.First(i=>i.Name=="Alface").Id,
                    Ingredient = ingredients.First(i=>i.Name=="Alface"),
                    SandwichId = sandwiches.First(s=>s.Name=="ALAGOAS BURGUER").Id,
                    Quantity = 1
                },
                new SandwichIngredient
                {
                    Id = Guid.NewGuid(),
                    Created = DateTime.Now,
                    CreatedBy = "System",
                    IngredientId = ingredients.First(i=>i.Name=="Pão de Hamburger").Id,
                    Ingredient = ingredients.First(i=>i.Name=="Pão de Hamburger"),
                    SandwichId = sandwiches.First(s=>s.Name=="ALAGOAS BURGUER").Id,
                    Quantity = 1
                },
                new SandwichIngredient
                {
                    Id = Guid.NewGuid(),
                    Created = DateTime.Now,
                    CreatedBy = "System",
                    IngredientId = ingredients.First(i=>i.Name=="Hamburger de salmão grelhado").Id,
                    Ingredient = ingredients.First(i=>i.Name=="Hamburger de salmão grelhado"),
                    SandwichId = sandwiches.First(s=>s.Name=="ITACOLOMI BURGER").Id,
                    Quantity = 1
                },
                new SandwichIngredient
                {
                    Id = Guid.NewGuid(),
                    Created = DateTime.Now,
                    CreatedBy = "System",
                    IngredientId = ingredients.First(i=>i.Name=="Cogumelo shitake").Id,
                    Ingredient = ingredients.First(i=>i.Name=="Cogumelo shitake"),
                    SandwichId = sandwiches.First(s=>s.Name=="ITACOLOMI BURGER").Id,
                    Quantity = 1
                },
                new SandwichIngredient
                {
                    Id = Guid.NewGuid(),
                    Created = DateTime.Now,
                    CreatedBy = "System",
                    IngredientId = ingredients.First(i=>i.Name=="Molho shoyo").Id,
                    Ingredient = ingredients.First(i=>i.Name=="Molho shoyo"),
                    SandwichId = sandwiches.First(s=>s.Name=="ITACOLOMI BURGER").Id,
                    Quantity = 1
                },
                new SandwichIngredient
                {
                    Id = Guid.NewGuid(),
                    Created = DateTime.Now,
                    CreatedBy = "System",
                    IngredientId = ingredients.First(i=>i.Name=="Catupiry").Id,
                    Ingredient = ingredients.First(i=>i.Name=="Catupiry"),
                    SandwichId = sandwiches.First(s=>s.Name=="ITACOLOMI BURGER").Id,
                    Quantity = 1
                },
                new SandwichIngredient
                {
                    Id = Guid.NewGuid(),
                    Created = DateTime.Now,
                    CreatedBy = "System",
                    IngredientId = ingredients.First(i=>i.Name=="Pão de Hamburger").Id,
                    Ingredient = ingredients.First(i=>i.Name=="Pão de Hamburger"),
                    SandwichId = sandwiches.First(s=>s.Name=="ITACOLOMI BURGER").Id,
                    Quantity = 1
                },
                new SandwichIngredient
                {
                    Id = Guid.NewGuid(),
                    Created = DateTime.Now,
                    CreatedBy = "System",
                    IngredientId = ingredients.First(i=>i.Name=="Hamburguer Angus").Id,
                    Ingredient = ingredients.First(i=>i.Name=="Hamburguer Angus"),
                    SandwichId = sandwiches.First(s=>s.Name=="MATHIAS BURGER").Id,
                    Quantity = 1
                },
                new SandwichIngredient
                {
                    Id = Guid.NewGuid(),
                    Created = DateTime.Now,
                    CreatedBy = "System",
                    IngredientId = ingredients.First(i=>i.Name=="Maionses especial").Id,
                    Ingredient = ingredients.First(i=>i.Name=="Maionses especial"),
                    SandwichId = sandwiches.First(s=>s.Name=="MATHIAS BURGER").Id,
                    Quantity = 1
                },
                new SandwichIngredient
                {
                    Id = Guid.NewGuid(),
                    Created = DateTime.Now,
                    CreatedBy = "System",
                    IngredientId = ingredients.First(i=>i.Name=="Cebola").Id,
                    Ingredient = ingredients.First(i=>i.Name=="Cebola"),
                    SandwichId = sandwiches.First(s=>s.Name=="MATHIAS BURGER").Id,
                    Quantity = 1
                },
                new SandwichIngredient
                {
                    Id = Guid.NewGuid(),
                    Created = DateTime.Now,
                    CreatedBy = "System",
                    IngredientId = ingredients.First(i=>i.Name=="Queijo Cheddar").Id,
                    Ingredient = ingredients.First(i=>i.Name=="Queijo Cheddar"),
                    SandwichId = sandwiches.First(s=>s.Name=="MATHIAS BURGER").Id,
                    Quantity = 1
                },
                new SandwichIngredient
                {
                    Id = Guid.NewGuid(),
                    Created = DateTime.Now,
                    CreatedBy = "System",
                    IngredientId = ingredients.First(i=>i.Name=="Pão de Hamburger").Id,
                    Ingredient = ingredients.First(i=>i.Name=="Pão de Hamburger"),
                    SandwichId = sandwiches.First(s=>s.Name=="MATHIAS BURGER").Id,
                    Quantity = 1
                }

            };

            //sandwiches.ToList().ForEach(s => s.Ingredients = sandwichIngredients.Where(si => si.SandwichId == s.Id).ToArray());
            context.PriceRules.AddRange(prices);
            context.Ingredients.AddRange(ingredients);
            context.Sandwiches.AddRange(sandwiches);
            context.SandwichIngredients.AddRange(sandwichIngredients);
            
            context.SaveChanges();
        }
    }
}
