using System;
using shoppingcart.Interfaces;

namespace shoppingcart
{
    class Program
    {
        static void Main(string[] args)
        {
            double transport = 2.0;
            IShopingCart cart = new ShopingCart(transport);

            cart.AddProduct(1, new Product("Coca", 12));
        }
    }
}
