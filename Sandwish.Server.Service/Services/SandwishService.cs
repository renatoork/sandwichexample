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

        public async Task<IEnumerable<Product>> GetCarts()
        {
            return null;// await _repository.GetCarts();
        }
        public async Task<Product> SetCarts(Cart cart)
        {
            return null;// await _repository.SetCarts(cart);
        }

    }
}
