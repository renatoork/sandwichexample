using Sandwish.Server.Repository;
using Sandwish.Server.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sandwish.Server.Service
{
    public class SandwishService
    {
        private ISandwishRepository _repository;

        public SandwishService(ISandwishRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _repository.GetProducts();
        }
        public async Task<IEnumerable<Ingredient>> GetIngredients()
        {
            return await _repository.GetIngredients();
        }
    }
}
