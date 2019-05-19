using Microsoft.AspNetCore.Mvc;
using Sandwish.Server.Exceptions;
using Sandwish.Server.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sandwish.Server.Service
{
    [Route("api/[controller]")]
    [ApiController]
    public class SandwishController : ControllerBase
    {
        private SandwishService _service;
        private SandwishCore _core;
        private RegisterService _register;

        public SandwishController(RegisterService register, SandwishService service, SandwishCore core)
        {
            _service = service;
            _core = core;
            _register = register;
        }

        [HttpGet("PriceByProductId")]
        public double PriceByProductId(int id)
        {
            var product = _register.GetProduct(id).GetAwaiter().GetResult();
            return _core.GetPrice(product.Ingredients);
        }

        [HttpGet("PriceByIngredients")]
        public double PriceByIngredients(List<Ingredient> ingredients)
        {
            return _core.GetPrice(ingredients);
        }

        [HttpGet("cart")]
        public IEnumerable<Cart> GetCart()
        {
            return null;// _service.GetCarts().GetAwaiter().GetResult() ;
        }

        [HttpPost("cart")]
        public Product SetCart(Cart cart)
        {
            return null; // _service.SetCarts(cart).GetAwaiter().GetResult();
        }

        [HttpGet("Exception")]
        public IActionResult Exception()
        {
            throw new ErrorTestException();
        }
    }
}
