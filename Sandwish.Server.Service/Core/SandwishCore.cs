using Sandwish.Server.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandwish.Server.Service
{
    public class SandwishCore
    {
        public double GetPrice(List<Ingredient> ingredients)
        {
            double price = ingredients.Select(v => v.Price).Sum();
            price = GetPromotionLight(ingredients, price);
            price = GetPromotionALotOf(ingredients, price, "Hamburguer");
            price = GetPromotionALotOf(ingredients, price, "Queijo");
            return price;
        }

        public double GetPromotionALotOf(List<Ingredient> ingredients, double price, string meal)
        {
            int meats = ingredients.Where(x => x.Name.Contains(meal)).Count() / 3;
            price = 2 * meats * ingredients.Where(x => x.Name.Contains(meal)).FirstOrDefault().Price;
            return price;
        }

        public double GetPromotionLight(List<Ingredient> ingredients, double price)
        {
            var lettuce = ingredients.Exists(l => l.Name.Equals("Alface"));
            var bacon = ingredients.Exists(l => l.Name.Equals("Bacon"));
            if (lettuce && !bacon) return price*0.9;
            return price;
        }
    }
}
