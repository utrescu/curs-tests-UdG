using shopcart;
using shopcart.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace shopcart
{
    public class ShoppingCartTransportTest
    {
        private readonly IShoppingCart shoppingcart;

        public ShoppingCartTransportTest()
        {
            shoppingcart = new ShoppingCart(2);
        }
    }
}