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
            Assert.True(shoppingcart.IsEmpty());

        }

        [Fact]
        public void ComprovaQueSiNoTeUsuariLaCistellaEsAnonima()
        {
            // Arrange
            
            // Act
            var resultat = shoppingcart.GetUsuari();

            // Assert
            Assert.Equal("anònim", resultat);
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


        public class ProductesData
        {
            public static IEnumerable<object[]> Data =>
                new List<object[]>
                {
                    new object[] { new[] { (1, new Product("a", 1.0, 1.0)) } },
                    new object[] { new[] { 
                            (2, new Product("a", 1.0, 1.0)), 
                            (3, new Product("b",1.0,1.0)) 
                        } 
                    },
                };
        }

        [Theory]
        [MemberData(nameof(ProductesData.Data), MemberType= typeof(ProductesData))]
        public void ComprovaQueAfegirElementsComptaCorrectament(params (int, Product)[] productes) {
            // Arrange
            var expected = 0;
            foreach(var producte in productes) {
                expected += producte.Item1;
                shoppingcart.AddProduct(producte.Item1, producte.Item2);
            }

            // Act
            var resultat = shoppingcart.GetItemsCount();

            // Assert
            Assert.Equal(expected, resultat);
        }

    }
}
