using shopcart;
using shopcart.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace shoppingcarttest
{
    public class ShoppingCartTest
    {
        private readonly IShoppingCart shoppingcart;

        public ShoppingCartTest()
        {
            shoppingcart = new ShoppingCart(2);
        }

        [Fact]
        public void ComprovaQueUnaCistellaNovaNoTeProductes()
        {
            // Arrange

            // Act
            var resultat = shoppingcart.GetItemsCount();

            // Arrange
            Assert.Equal(0, resultat);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(int.MaxValue)]
        public void ComprovaQueEsPodenAfegirElementsEnUnaCistellaBuida(int quants)
        {
            // Arrange
            shoppingcart.AddProduct(quants, new Product("", 1.0, 1.0));

            // Act
            var resultat = shoppingcart.GetItemsCount();

            // Assert
            Assert.Equal(quants, resultat);
        }

        [Fact]        
        public void ComprovaQueNoEsPodenAfegirElementsNegatiusEnUnaCistellaBuida()
        {
            // Arrange
            var quants = -1;
            shoppingcart.AddProduct(quants, new Product("", 1.0, 1.0));

            // Act
            var resultat = shoppingcart.GetItemsCount();

            // Assert
            Assert.Equal(quants, resultat);
        }

    }
}
