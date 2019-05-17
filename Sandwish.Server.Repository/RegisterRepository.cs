using Microsoft.EntityFrameworkCore;
using Sandwish.Server.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandwish.Server.Repository
{
    public class RegisterRepository : IRegisterRepository
    {
        private SandwishDbContext _context;

        public RegisterRepository(SandwishDbContext context)
        {
            _context = context;
        }

        #region Ingredient
        public async Task<List<Ingredient>> GetIngredients()
        {
            return await _context.Ingredients.ToListAsync();
        }

        public async Task<Ingredient>GetIngredient(int id)
        {
            return await _context.Ingredients.Where(x => x.IngredientId == id).FirstOrDefaultAsync();
        }

        public async Task<Ingredient> SetIngredients(Ingredient ingredient)
        {
            var newID = await _context.Ingredients.Select(x => x.IngredientId).MaxAsync() + 1;
            ingredient.IngredientId = newID;

            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();
            return ingredient;
        }
        #endregion

        #region product
        public async Task<List<Product>> GetProducts()
        {
            var products = await _context.Products
                                            .Include(i => i.Ingredients)
                                            .ToArrayAsync();

            var result = products.Select(i => new Product()
            {
                ProductId = i.ProductId,
                Name = i.Name,
                Ingredients = i.Ingredients.Select(s => new Ingredient{IngredientId = s.IngredientId, Name = s.Name, Price = s.Price}).ToList()                
            }).ToAsyncEnumerable().ToList();
            return await result;
        }

        public async Task<Product> SetProducts(Product product)
        {
            var newID = await _context.Products.Select(x => x.ProductId).MaxAsync() + 1;
            product.ProductId = newID;
            product.Ingredients = product.Ingredients.Select(s => GetIngredient(s.IngredientId).GetAwaiter().GetResult()).ToList();
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _context.Products.Where(x => x.ProductId == id).FirstOrDefaultAsync();
        }
        #endregion
    }
}
