using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("products")]
        public IEnumerable<Product> GetProducts()
        {
            return _service.GetProducts().GetAwaiter().GetResult() ;
        }

        [HttpGet("ingredients")]
        public IEnumerable<Ingredient> GetIngredients()
        {
            return _service.GetIngredients().GetAwaiter().GetResult();
        }
    }
}
