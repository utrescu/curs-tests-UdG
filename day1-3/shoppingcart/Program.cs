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

            cart.AddProduct(2, new Product("Mantega", 6.18, 0.5));
            cart.AddProduct(1, new Product("Llet", 0.58, 0.75));
            cart.AddProduct(2, new Product("Bistec de vedella", 2.12, 0.5));
            cart.AddProduct(1, new Product("Carn picada", 0.63, 1));
            cart.AddProduct(4, new Product("Filet de Bou", 8, 0.8));

            System.Console.WriteLine($"Total: {cart.GetTotal()} euros (transport: {cart.TransportPrice} euros)");

            System.Console.WriteLine(cart);
        }
    }
}
