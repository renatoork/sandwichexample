namespace Sandwish.Server.Repository.Models
{
    public class ItemCart
    {
        public int ItemCartId{ get; set; }
        public Product Product { get; set; }
        public double Price { get; set; }
        public double PriceOff { get; set; }
    }
}