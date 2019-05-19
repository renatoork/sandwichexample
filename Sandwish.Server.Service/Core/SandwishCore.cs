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
            try
            {
                double price = ingredients.Select(v => v.Price).Sum();
                price -= GetPromotionLight(ingredients, price);
                price -= GetPromotionALotOf(ingredients, "Hamburguer");
                price -= GetPromotionALotOf(ingredients, "Queijo");
                return price;
            } catch
            {
                return 0;
            }
        }

        public double GetPromotionALotOf(List<Ingredient> ingredients, string meal)
        {
            try
            {
                double price = ingredients.Where(x => x.Name.Contains(meal)).FirstOrDefault().Price;
                double meats = ingredients.Where(x => x.Name.Contains(meal)).Count();
                return price * (int)(meats / 3);
            } catch
            {
                return 0;
            }
        }

        public double GetPromotionLight(List<Ingredient> ingredients, double price)
        {
            try
            {
                var lettuce = ingredients.Exists(l => l.Name.Equals("Alface"));
                var bacon = ingredients.Exists(l => l.Name.Equals("Bacon"));
                if (lettuce && !bacon) return price * 0.1;
                return 0;
            } catch
            {
                return 0;
            }
        }
    }
}
