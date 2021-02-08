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

        #region AfegirProductes

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(int.MaxValue)]
        public void ComprovaQueEsPodenAfegirQuantitatsDelMateixProducteEnUnaCistellaBuida(int quants)
        {
            // Arrange
            shoppingcart.AddProduct(quants, new Product("", 1.0, 1.0));

            // Act
            var resultat = shoppingcart.GetItemsCount();

            // Assert
            Assert.Equal(quants, resultat);
        }

        [Fact]        
        public void ComprovaQueNoEsPodenAfegirQuantitatsNegativesDeProductesEnUnaCistellaBuida()
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
                    new object[] { new[] {
                            (2, new Product("a", 1.0, 1.0)),
                            (3, new Product("a",1.0,1.0)),
                            (1, new Product("a", 1.0, 1.0)),
                        }
                    },
                };
        }

        [Theory]
        [MemberData(nameof(ProductesData.Data), MemberType= typeof(ProductesData))]
        public void ComprovaQueEnAfegirProductesEsComptaCorrectamentLesQuantitats(params (int qtat, Product producte)[] dades) {
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
        public void ComprovaQueAfegirProductesAfegeixCorrectatmentElsProductes(params (int qtat, Product producte)[] dades) {
            // Arrange
            var expected = dades.Select( d => d.producte.Nom).Distinct();

            // Act
            foreach(var dada in dades) {
                shoppingcart.AddProduct(dada.qtat, dada.producte);
            }

            // Assert: Els productes són els mateixos
            var productes = shoppingcart.Items().Select(i => i.Nom);
            Assert.Equal(expected, productes);

        }

        [Fact]
        public void ComprovaQueNoEsPodenAfegirProductesNull()
        {
            // Arrange

            // Act
            void addProducte() => shoppingcart.AddProduct(1, null);

            // Assert
            var ex = Assert.Throws<ArgumentNullException>(addProducte);
 
        }

        #endregion

        #region TreureProductes

        [Fact]
        public void ComprovaQueNoEsPodenTreureProductesNull()
        {
            // Arrange

            // Act
            void removeProducte() => shoppingcart.RemoveProduct(1, null);

            // Assert
            var ex = Assert.Throws<ArgumentNullException>(removeProducte);

        }

        [Fact]
        public void ComprovaQueNoEsPodenTreureMesProductesDelsQueHiHa()
        {
            // Arrange
            shoppingcart.AddProduct(1, new Product("b", 1.0, 1.0));
            // Act
            void removeProducte() => shoppingcart.RemoveProduct(2, "b");

            // Assert
            var ex = Assert.Throws<Exception>(removeProducte);
            Assert.Equal("Vols treure més productes dels que hi ha", ex.Message);
        }

        [Theory]
        [InlineData("a", "a", "b", "c", "d")]
        [InlineData("d", "a", "b", "e", "c", "d")]
        [InlineData("b", "a", "b", "c", "e", "d")]
        public void ComprovaQueEsPodenTreureProductesDeLaCistella(string treure, params string[] productes)
        {
            // Arrange
            foreach (var producte in productes)
            {
                shoppingcart.AddProduct(1, new Product(producte, 1.0, 1.0));
            }

            var productesExpected = productes.Where(x => x != treure).ToArray();

            // Act
            shoppingcart.RemoveProduct(1, treure);

            // Assert
            var productesALaLlista = shoppingcart.Items().Select(i => i.Nom);
            Assert.Equal(productesExpected, productesALaLlista);
        }

        [Theory]
        [InlineData("b", "a", "b", "c", "e", "d")]
        public void ComprovaQueTreureProductesDeLaCistellaSiNoHiSonNoCanviaRes(params string[] productes)
        {
            // Arrange
            foreach (var article in productes)
            {
                shoppingcart.AddProduct(1, new Product(article, 1.0, 1.0));
            }

            var productesExpected = shoppingcart.Items().Select(i => i.Nom);
            var itemsExpected = shoppingcart.GetItemsCount();
            var treure = "ArticleQueNoEstaEnLaCistella";            

            // Act
            var ex = Assert.Throws<Exception>(() =>
            {
                shoppingcart.RemoveProduct(1, treure);
            });

            // Assert

            Assert.Equal("El producte no està a la cistella", ex.Message);
            Assert.Equal(itemsExpected, shoppingcart.GetItemsCount());
            var productesALaLlista = shoppingcart.Items().Select(i => i.Nom);
            Assert.Equal(productesExpected, productesALaLlista);
        }

        [Theory]
        [InlineData("a", "a", "b", "c", "d")]
        [InlineData("d", "a", "b", "e", "c", "d")]
        [InlineData("b", "a", "b", "c", "e", "d")]
        public void ComprovaQueEsPodenTreureDeLaCistellaQuantitatsNoTotalsDeProductes(string treure, params string[] productes)
        {
            var enPoso = 2;
            var enTrec = 1;
            // Arrange
            var expectedQuantitat = 0;
            foreach (var producte in productes)
            {
                if (producte == treure)
                {
                    shoppingcart.AddProduct(enPoso, new Product(producte, 1.0, 1.0));
                    expectedQuantitat += enPoso;
                }
                else
                {
                    shoppingcart.AddProduct(1, new Product(producte, 1.0, 1.0));
                    expectedQuantitat += 1;
                }
            }
            expectedQuantitat -= enTrec;

            var productesExpected = productes;

            // Act: en trec 1
            shoppingcart.RemoveProduct(enTrec, treure);

            // Assert: La quantitat ha baixat
            Assert.Equal(expectedQuantitat, shoppingcart.GetItemsCount());

            // l'article encara hi és
            var productesALaLlista = shoppingcart.Items().Select(i => i.Nom);
            Assert.Equal(productesExpected, productesALaLlista);
        }

        #endregion

    }
}
