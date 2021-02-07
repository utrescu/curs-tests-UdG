using System;
using contra;
using Xunit;

namespace contraTest
{
    public class ValidatorTest
    {
        private Validator validador;

        public ValidatorTest()
        {
            validador = new Validator("Usuari12");
        }

        [Fact]
        public void ComprovaQueUnaontrasenyaComplexaValida()
        {
            // Arrange
            var contrasenya = "Pe1x3ts!"; 
            var expected = true;

            // Act
            var resultat = validador.Valida(contrasenya);

            // Assert
            Assert.Equal(expected, resultat);

        }

        [Fact]
        public void ComprovaQueSiLaContrasenyaNoTe8CaractersFalla()
        {
            // Arrange
            var contrasenya = "Hola";
            var expected = false;

            // Act
            var resultat = validador.Valida(contrasenya);

            // Assert
            Assert.Equal(expected, resultat);
        }

        [Theory]
        [InlineData("abcdefgh")]
        [InlineData("ab1cdefgh")]
        [InlineData("abcd11efg")]
        [InlineData("111111111")]
        public void ComprovaQueSiNoConteDosDigitsDiferentsFalla(string contrasenya) 
        {
            // Arrange
            var expected = false;

            // Act
            var resultat = validador.Valida(contrasenya);

            // Assert
            Assert.Equal(expected, resultat);
        }       

        [Fact]
        public void ComprovaQueSiNoTeMinusculesFalla() 
        {
            // Arrange
            var contrasenya = "AB12EFGGHI";
            var expected = false;

            // Act
            var resultat = validador.Valida(contrasenya);

            // Assert
            Assert.Equal(expected, resultat);
        }       

        [Fact]
        public void ComprovaQueSiNoTeMajusculesFalla() 
        {
            // Arrange
            var contrasenya = "ab12cdefghy";
            var expected = false;

            // Act
            var resultat = validador.Valida(contrasenya);

            // Assert
            Assert.Equal(expected, resultat);
        }     

        [Fact]
        public void ComprovaQueLaContrasenyaNoEsElNomDeUsuari()
        {
            var contrasenya = validador.Username;
            var expected = false;

            var resultat = validador.Valida(contrasenya);

            Assert.Equal(expected, resultat);
        }  

        [Fact]
        public void ComprovaQueLaContrasenyaNoEsElNomDeUsuariAlReves()
        {
            var contrasenya = "21irausU";
            var expected = false;

            var resultat = validador.Valida(contrasenya);

            Assert.Equal(expected, resultat);
        }  


 
    }
}
