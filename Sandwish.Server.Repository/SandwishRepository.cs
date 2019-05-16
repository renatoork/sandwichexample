using Microsoft.EntityFrameworkCore;
using Sandwish.Server.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandwish.Server.Repository
{
    public class SandwishRepository : ISandwishRepository
    {
        private SandwishDbContext _context;

        public SandwishRepository(SandwishDbContext context)
        {
            _context = context;
        }
        public async Task AddCart(Cart cart)
        {
            await Task.Run(() => _context.Carts.Add(cart));
        }

        public async Task<List<Ingredient>> GetIngredients()
        {
            return await _context.Ingredients.ToListAsync();
        }

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
    }
}
