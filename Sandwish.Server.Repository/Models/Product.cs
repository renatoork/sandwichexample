using System;
using System.Collections.Generic;
using System.Text;

namespace Sandwish.Server.Repository.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}
