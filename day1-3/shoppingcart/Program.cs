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

            cart.AddProduct(2, new Product("Coca", 12, 0.5));
            cart.AddProduct(1, new Product("Martell", 10.5, 0.75));

            System.Console.WriteLine($"Total: {cart.GetTotal()} euros (transport: {cart.GetTransportPrice()} euros)");
        }
    }
}
