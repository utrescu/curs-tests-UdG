using Moq;
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
        private const double preuBaseTransport = 2.0;

        public ShoppingCartTransportTest()
        {
            shoppingcart = new ShoppingCart(preuBaseTransport);
        }

        [Theory]
        [InlineData(0.01)]
        [InlineData(2)]
        [InlineData(4.99)]
        public void ComprovaQueElTransportEsElValorPerDefecteQuanElPesEsBaixINoEs50(double pes)
        {
            // Arrange
            shoppingcart.AddProduct(1, new Product("A", 20, pes));

            // Act
            var resultat = shoppingcart.TransportPrice;

            // Assert
            Assert.Equal(preuBaseTransport, resultat);
        }

        [Theory]
        [InlineData(50, 2, 0)]
        [InlineData(51, 2, 0)]
        [InlineData(50, 100, 0)]
        [InlineData(51, 100, 0)]
        public void ComprovaQueElTransportEsGratuitSiElPreuEsSuperiorA50(double preu, double pes, double expected)
        {
            // Arrange
            shoppingcart.AddProduct(1, new Product("A", preu, pes));

            // Act
            var resultat = shoppingcart.TransportPrice;

            // Assert
            Assert.Equal(0, resultat);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(50)]
        [InlineData(51)]
        public void ComprovaQueElTransportEsGratuitPelsUsuarisVip(double preu)
        {
            // Arrange
            var usuari = new Mock<IUsuari>();
            usuari.Setup(u => u.EsVIP()).Returns(true);

            shoppingcart.AddUsuari(usuari.Object);
            shoppingcart.AddProduct(1, new Product("A", preu, 2));

            // Act
            var resultat = shoppingcart.TransportPrice;

            // Assert
            Assert.Equal(0, resultat);
        }

        [Theory]
        [InlineData(3.0, preuBaseTransport)]
        [InlineData(5.0, preuBaseTransport+1)]
        [InlineData(6.0, preuBaseTransport+1)]
        [InlineData(10.0, preuBaseTransport+2)]
        [InlineData(49.0, preuBaseTransport+10)]
        public void ComprovaElPreuDelTransportPerUsuarisNormalsSegonsElPes(double pes, double expected)
        {
            // Arrange
            var preu = 2.0;

            shoppingcart.AddProduct(1, new Product("A", preu, pes));

            // Act
            var resultat = shoppingcart.TransportPrice;

            // Assert
            Assert.Equal(expected, resultat);
        }

    }
}