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
    public class ShoppingCartUsuariTest
    {
        private readonly IShoppingCart shoppingcart;

        public ShoppingCartUsuariTest()
        {
            shoppingcart = new ShoppingCart(2, new Usuari("Pere"));
        }


        [Fact]
        public void ComprovaQueSiNoTeUsuariLaCistellaEsAnonima()
        {
            // Arrange
            var anonimShoppingCart = new ShoppingCart(2);
            
            // Act
            var resultat = anonimShoppingCart.GetUsuari();

            // Assert
            Assert.Equal("anònim", resultat);
        }

        [Fact]
        public void ComprovaQueUnaCistellaAmbUsuariNoEsAnonima()
        {
            // Arrange

            // Act
            var resultat = shoppingcart.GetUsuari();

            // Assert
            Assert.Equal("Pere", resultat);
        }

        [Fact]
        public void ComprovaQueNoEsPotCanviarUsuariDeCistella()
        {
            // Arrange

            // Act

            // Assert
            var ex = Assert.Throws<Exception>( () => shoppingcart.AddUsuari(new Usuari("joan")) );

            Assert.Equal("Aquesta cistella ja pertany a un usuari", ex.Message);
        }








    }
}
