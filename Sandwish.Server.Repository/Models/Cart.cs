using System;
using System.Collections.Generic;
using System.Text;

namespace Sandwish.Server.Repository.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public string Customer { get; set; }
        public List<ItemCart> Items { get; set; }
    }
}
