using Sandwish.Server.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sandwish.Server.Repository
{
    public interface IRegisterRepository
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProduct(int id);
        Task<Product> SetProducts(Product product);
        Task<List<Ingredient>> GetIngredients();
        Task<Ingredient> GetIngredient(int id);
        Task<Ingredient> SetIngredients(Ingredient ingredient);
    }
}
