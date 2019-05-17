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

        public SandwishController(SandwishService service)
        {
            _service = service;
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
