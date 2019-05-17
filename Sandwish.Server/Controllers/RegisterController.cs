using Microsoft.AspNetCore.Mvc;
using Sandwish.Server.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sandwish.Server.Service
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private RegisterService _service;

        public RegisterController(RegisterService service)
        {
            _service = service;
        }

        [HttpGet("products")]
        public IEnumerable<Product> GetProducts()
        {
            return _service.GetProducts().GetAwaiter().GetResult() ;
        }

        [HttpPost("products")]
        public Product SetProducts(Product product)
        {
            return _service.SetProducts(product).GetAwaiter().GetResult();
        }

        [HttpGet("ingredients")]
        public IEnumerable<Ingredient> GetIngredients()
        {
            return _service.GetIngredients().GetAwaiter().GetResult();
        }

        [HttpPost("ingredient")]
        public Ingredient SetIngredient(Ingredient ingredient)
        {
            return _service.SetIngredient(ingredient).GetAwaiter().GetResult();
        }

    }
}
