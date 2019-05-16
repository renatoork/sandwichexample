using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandwish.Server.Repository.Models
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SandwishDbContext(serviceProvider.GetRequiredService<DbContextOptions<SandwishDbContext>>()))
            {

                SetIngredients(context);
                SetProducts(context);

            }

            void SetIngredients(SandwishDbContext context)
            {
                if (context.Ingredients.Any())
                {
                    return;   // Database has been seeded
                }

                context.Ingredients.AddRange(
                    new Ingredient
                    {
                        IngredientId = 1,
                        Name = "Alface",
                        Price = 0.40
                    },
                    new Ingredient
                    {
                        IngredientId = 2,
                        Name = "Bacon",
                        Price = 2
                    },
                    new Ingredient
                    {
                        IngredientId = 3,
                        Name = "Hamburguer de Carne",
                        Price = 3
                    },
                    new Ingredient
                    {
                        IngredientId = 4,
                        Name = "Ovo",
                        Price = 0.80
                    },
                    new Ingredient
                    {
                        IngredientId = 5,
                        Name = "Queijo",
                        Price = 1.5
                    });
                context.SaveChanges();

            }
            void SetProducts(SandwishDbContext context)
            {
                if (context.Products.Any())
                {
                    return;   // Database has been seeded
                }

                context.Products.AddRange(
                    new Product
                    {
                        ProductId = 1,
                        Name = "X-Bacon",
                        Ingredients = new List<Ingredient>()
                        {
                            context.Ingredients.Where(x => x.IngredientId==2).FirstOrDefault(),
                            context.Ingredients.Where(x => x.IngredientId==3).FirstOrDefault(),
                            context.Ingredients.Where(x => x.IngredientId==5).FirstOrDefault(),

                        }
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
