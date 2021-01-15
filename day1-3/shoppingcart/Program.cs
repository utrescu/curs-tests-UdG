using System;
using shopcart.Interfaces;

namespace shopcart
{
    class Program
    {
        static void Main(string[] args)
        {
            double transport = 2.0;
            IShoppingCart cart = new ShoppingCart(transport);

            cart.AddProduct(2, new Product("Coca", 12));

            System.Console.WriteLine($"Total: {cart.GetTotal()}€ (transport: {cart.GetTransportPrice()}€)");
        }
    }
}
