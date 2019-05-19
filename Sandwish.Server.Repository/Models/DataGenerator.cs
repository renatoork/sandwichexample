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

                if (context.Ingredients.Any())
                {
                    return;
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
                    }
                 );
                context.SaveChanges();

                if (context.Products.Any())
                {
                    return;
                }

                List<Ingredient> ing = context.Ingredients.Where(i => i.IngredientId == 2 || i.IngredientId == 3 || i.IngredientId == 5).ToList();
                var prod = new Product
                {
                    ProductId = 1,
                    Name = "X-Bacon",
                    Ingredients = ing
                };
                context.Products.Add(prod);
                context.SaveChanges();
            };
        }
    }
}
