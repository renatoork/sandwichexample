using FluentAssertions;
using Sandwish.Server.Repository.Models;
using Sandwish.Server.Service;
using System;
using System.Collections.Generic;
using Xunit;

namespace Sandwish.Test
{
    public class SandwishCoreTest
    {
        private Dictionary<string, List<Ingredient>> _ingredients;
        private SandwishCore _core;

        public SandwishCoreTest()
        {
            _core = new SandwishCore();
        }

        public static IEnumerable<object[]> GetIngredientsPrice()
        {
            yield return new object[] {
                    "PriceWithoutPromotion",
                    new List<Ingredient>()
                    {
                        new Ingredient() { IngredientId = 1, Name = "Bacon", Price = 10.0},
                        new Ingredient() { IngredientId = 1, Name = "Hamburguer de Carne", Price = 10.0},
                        new Ingredient() { IngredientId = 2, Name = "Queijo", Price = 10.0}
                    },
                    30.0
            };
            yield return new object[] {
                    "PriceWithLightPromotion",
                    new List<Ingredient>()
                    {
                        new Ingredient() { IngredientId = 1, Name = "Alface", Price = 10.0},
                        new Ingredient() { IngredientId = 2, Name = "Queijo", Price = 10.0},
                        new Ingredient() { IngredientId = 3, Name = "Hamburguer de Carne", Price = 10.0}
                    },
                    27.0
            };
            yield return new object[] {
                    "PriceWithAlotOfMeatPromotion",
                    new List<Ingredient>()
                    {
                        new Ingredient() { IngredientId = 1, Name = "Bacon", Price = 10.0},
                        new Ingredient() { IngredientId = 1, Name = "Queijo", Price = 10.0},
                        new Ingredient() { IngredientId = 2, Name = "Hamburguer de Carne", Price = 10.0},
                        new Ingredient() { IngredientId = 2, Name = "Hamburguer de Carne", Price = 10.0},
                        new Ingredient() { IngredientId = 2, Name = "Hamburguer de Carne", Price = 10.0}
                    },
                    40.0
            };
            yield return new object[] {
                    "riceWithAlotOfCheesePromotion",
                    new List<Ingredient>()
                    {
                        new Ingredient() { IngredientId = 1, Name = "Bacon", Price = 10.0},
                        new Ingredient() { IngredientId = 1, Name = "Queijo", Price = 10.0},
                        new Ingredient() { IngredientId = 1, Name = "Queijo", Price = 10.0},
                        new Ingredient() { IngredientId = 1, Name = "Queijo", Price = 10.0},
                        new Ingredient() { IngredientId = 1, Name = "Queijo", Price = 10.0},
                        new Ingredient() { IngredientId = 2, Name = "Hamburguer de Carne", Price = 10.0}
                    },
                    50.0
            };
            yield return new object[] {
                    "riceWithAlotOfCheeseAndLightPromotion",
                    new List<Ingredient>()
                    {
                        new Ingredient() { IngredientId = 1, Name = "Salada", Price = 10.0},
                        new Ingredient() { IngredientId = 1, Name = "Queijo", Price = 10.0},
                        new Ingredient() { IngredientId = 1, Name = "Queijo", Price = 10.0},
                        new Ingredient() { IngredientId = 1, Name = "Queijo", Price = 10.0},
                        new Ingredient() { IngredientId = 1, Name = "Queijo", Price = 10.0},
                        new Ingredient() { IngredientId = 2, Name = "Hamburguer de Carne", Price = 10.0}
                    },
                    50.0
            };
            yield return new object[] {
                    "PriceWithAlotOfMeatAndLightPromotion",
                    new List<Ingredient>()
                    {
                        new Ingredient() { IngredientId = 1, Name = "Salada", Price = 10.0},
                        new Ingredient() { IngredientId = 1, Name = "Queijo", Price = 10.0},
                        new Ingredient() { IngredientId = 2, Name = "Hamburguer de Carne", Price = 10.0},
                        new Ingredient() { IngredientId = 2, Name = "Hamburguer de Carne", Price = 10.0},
                        new Ingredient() { IngredientId = 2, Name = "Hamburguer de Carne", Price = 10.0}
                    },
                    40.0
            };
        }
        public static IEnumerable<object[]> GetIngredientsLight()
        {
            yield return new object[] {
                    "Light10%WithLuttuceWithoutBacon",
                    new List<Ingredient>()
                    {
                        new Ingredient() { IngredientId = 1, Name = "Alface", Price = 10.0},
                        new Ingredient() { IngredientId = 2, Name = "Queijo", Price = 10.0}
                    },
                    10.0
            };
            yield return new object[] {
                    "NotLight10%WithLuttuceWithBacon",
                    new List<Ingredient>()
                    {
                        new Ingredient() { IngredientId = 1, Name = "Alface", Price = 10.0},
                        new Ingredient() { IngredientId = 2, Name = "Queijo", Price = 10.0},
                        new Ingredient() { IngredientId = 3, Name = "Bacon", Price = 10.0}
                    },
                    0
            };
            yield return new object[] {
                    "NotLight10%WithoutLuttuceWithoutBacon",
                    new List<Ingredient>()
                    {
                        new Ingredient() { IngredientId = 1, Name = "Bacon", Price = 10.0},
                        new Ingredient() { IngredientId = 2, Name = "Hamburguer", Price = 10.0}
                    },
                    0.0
            };
            yield return new object[] {
                    "NotLight10%WithoutLuttuceWithoutBacon",
                    new List<Ingredient>()
                    {
                        new Ingredient() { IngredientId = 1, Name = "Queijo", Price = 10.0},
                        new Ingredient() { IngredientId = 2, Name = "Hamburguer", Price = 10.0}
                    },
                    0.0
            };
        }

        public static List<Ingredient> GetMeal(string name, int lot)
        {
            var ret = new List<Ingredient>();
            for (var i = 0; i < lot; i++)
                ret.Add(new Ingredient() { IngredientId = i, Name = name, Price = 10.0 });
            return ret;
        }

        public static IEnumerable<object[]> GetIngredientsCheese()
        {
            for (var i = 0; i < 20; i++)
            {
                var elem = new object[]
                {
                    $"ALotOfMeat-{i}",
                    GetMeal("Queijo", i),
                    10 * (int)(i / 3)
                };
                yield return elem;
            }
        }

        public static IEnumerable<object[]> GetIngredientsMeal()
        {
            for (var i = 0; i < 20; i++)
            {
                var elem = new object[]
                {
                    $"ALotOfMeat-{i}",
                    GetMeal("Hamburguer", i),
                    10 * (int)(i / 3)
                };
                yield return elem;
            }
        }

        [Theory]
        [MemberData(nameof(GetIngredientsPrice))]
        public void GetPromotionPriceTest(string name, List<Ingredient> ingredients, double result)
        {
            double value = _core.GetPrice(ingredients);
            value.Should().Be(result);

        }

        [Theory]
        [MemberData(nameof(GetIngredientsLight))]
        public void GetPromotionLightTest(string name, List<Ingredient> ingredients, double result)
        {
            double value = _core.GetPromotionLight(ingredients, 100);
            value.Should().Be(result);
           
        }
  
        [Theory]
        [MemberData(nameof(GetIngredientsMeal))]
        public void GetPromotionALotOfMeatTest(string name, List<Ingredient> ingredients, double result)
        {
            var value = _core.GetPromotionALotOf(ingredients, "Hamburguer");
            value.Should().Be(result);
        }

        [Theory]
        [MemberData(nameof(GetIngredientsCheese))]
        public void GetPromotionALotOfCheeseTest(string name, List<Ingredient> ingredients, double result)
        {
            var value = _core.GetPromotionALotOf(ingredients, "Queijo");
            value.Should().Be(result);
        }
    }
}
