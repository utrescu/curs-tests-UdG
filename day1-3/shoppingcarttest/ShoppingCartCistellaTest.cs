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
    public class ShoppingCartCistellaTest
    {
        private readonly IShoppingCart shoppingcart;

        public ShoppingCartCistellaTest()
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
                            (3, new Product("b",1.0,1.0)),
                            (1, new Product("a", 1.0, 1.0)), 
                        } 
                    },
                };
        }

        [Theory]
        [MemberData(nameof(ProductesData.Data), MemberType= typeof(ProductesData))]
        public void ComprovaQueAfegirElementsComptaCorrectament(params (int qtat, Product producte)[] dades) {
            // Arrange
            var expected = 0;

            // Act
            foreach(var dada in dades) {
                expected += dada.qtat;
                shoppingcart.AddProduct(dada.qtat, dada.producte);
            }

            // Assert:  Les quantitats coincideixen
            Assert.Equal(expected, shoppingcart.GetItemsCount());
        }

        [Theory]
        [MemberData(nameof(ProductesData.Data), MemberType= typeof(ProductesData))]
        public void ComprovaQueAfegirElementsAfegeixCorrectatmentElsArticles(params (int qtat, Product producte)[] dades) {
            // Arrange
            var expected = dades.Select( d => d.producte.Nom).Distinct();

            // Act
            foreach(var dada in dades) {
                shoppingcart.AddProduct(dada.qtat, dada.producte);
            }

            // Assert: Els articles són els mateixos
            var articles = shoppingcart.Items().Select(i => i.Nom);
            Assert.Equal(expected, articles);

        }

    }
}
