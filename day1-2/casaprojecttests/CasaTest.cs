using System;
using casaproject;
using Xunit;
using Moq;

namespace casaprojecttests
{
    public class CasaTest
    {
        [Fact]
        public void ComprovaQueEsPotEntrarALaCasaSiLaPortaEstaOberta()
        {
            // Arrange
            var porta = new Porta();
            var casa = new Casa(porta);
            casa.ObrePorta();

            // Act
            casa.EntraPersona(new Persona());

            // Assert
            Assert.Equal(1, casa.QuantesPersonesHiHa());


        }

        [Fact]
        public void ComprovaQueEsPotEntrarALaCasaSiLaPortaEstaOberta2()
        {
            // Arrange
            var porta = new Mock<Porta>(false);            // No accepta paràmetres per defecte
            porta.Setup(p => p.EsOberta()).Returns(true);  // Els mètodes han de ser virtual

            var casa = new Casa(porta.Object);
            casa.ObrePorta();

            // Act
            var resultat = casa.EntraPersona(new Persona());

            // Assert
            Assert.True(resultat);
            Assert.Equal(1, casa.QuantesPersonesHiHa());


        }
    }
}
