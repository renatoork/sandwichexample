using Sandwish.Server.Repository;
using Sandwish.Server.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandwish.Server.Service
{
    public class RegisterService
    {
        private IRegisterRepository _repository;
        private SandwishCore _core;

        public RegisterService(IRegisterRepository repository, SandwishCore core)
        {
            _repository = repository;
            _core = core;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            List<Product> ret = new List<Product>();
            var products = await _repository.GetProducts();
            foreach(var prod in products)
            {
                prod.Price = _core.GetPrice(prod.Ingredients);
                ret.Add(prod);
            }
            return ret;
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await _repository.GetProduct(id);
            product.Price = _core.GetPrice(product.Ingredients);
            return product;
        }

        public async Task<Product> SetProducts(Product product)
        {
            return await _repository.SetProducts(product);
        }

        public async Task<IEnumerable<Ingredient>> GetIngredients()
        {
            return await _repository.GetIngredients();
        }
        public async Task<Ingredient> SetIngredient(Ingredient ingredient)
        {
            return await _repository.SetIngredients(ingredient);
        }

    }
}
