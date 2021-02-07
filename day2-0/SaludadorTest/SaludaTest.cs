using Moq;
using Saludador;
using System;
using Xunit;

namespace SaludadorTest
{
    public class SaludaTest
    {
        private readonly Saluda saluda;
        private readonly Mock<IDateWrapper> datewrapper;

        public SaludaTest()
        {
            datewrapper = new Mock<IDateWrapper>();
            saluda = new Saluda(datewrapper.Object);
        }

        [Theory]
        [InlineData("Pere")]
        [InlineData("Joan")]        
        public void ComprovaQueSaluda(string nom)
        {
            // Arrange 

            // Act
            var resultat = saluda.SaludaEn(nom);

            // Assert
            Assert.EndsWith(nom, resultat);
        }

        [Theory]
        [InlineData("  Joan", "Joan")]
        [InlineData("  Joan       ", "Joan")]
        public void ComprovaQueElsEspaisDelNomSonEliminats(string nom, string expected)
        {
            // Arrange 

            // Act
            var resultat = saluda.SaludaEn(nom);

            // Assert
            Assert.DoesNotContain(nom, resultat);
            Assert.EndsWith(expected, resultat);
            Assert.Matches(@$"^\w+ (\w+ )*{expected}$", resultat);
        }

        [Theory]
        [InlineData("pere", "Pere")]
        [InlineData("joan", "Joan")]
        public void ComprovaQueLaPrimeraLletraDelNomEsConvertidaAMajuscules(string nom, string expected)
        {
            // Arrange 

            // Act
            var resultat = saluda.SaludaEn(nom);

            // Assert
            Assert.Contains(expected, resultat);
        }

        [Theory]
        [InlineData("Pere", 6, 00)]
        [InlineData("Pere", 9, 22)]
        [InlineData("Pere", 11, 59)]
        public void ComprovaQuePelMatiDiuBonDia(string nom, int hora, int minuts)
        {
            var expected = "Bon dia";
            // Arrange 
            datewrapper.Setup(d => d.ObtenirHora()).Returns(DateTime.Today.AddHours(hora).AddMinutes(minuts));

            // Act
            var resultat = saluda.SaludaEn(nom);

            // Assert
            Assert.Equal($"{expected} {nom}", resultat);
        }

        [Theory]
        [InlineData("Pere", 14, 00)]
        [InlineData("Pere", 19, 22)]
        [InlineData("Pere", 20, 59)]
        public void ComprovaQuePerLaTardaDiuBonaTarda(string nom, int hora, int minuts)
        {
            var expected = "Bona tarda";
            // Arrange 
            datewrapper.Setup(d => d.ObtenirHora()).Returns(DateTime.Today.AddHours(hora).AddMinutes(minuts));

            // Act
            var resultat = saluda.SaludaEn(nom);

            // Assert
            Assert.Equal($"{expected} {nom}", resultat);
        }

        [Theory]
        [InlineData("Pere", 21, 00)]
        [InlineData("Pere", 3, 22)]
        [InlineData("Pere", 05, 59)]
        public void ComprovaQuePerLaNitDiuBonaNit(string nom, int hora, int minuts)
        {
            var expected = "Bona nit";
            // Arrange 
            datewrapper.Setup(d => d.ObtenirHora()).Returns(DateTime.Today.AddHours(hora).AddMinutes(minuts));

            // Act
            var resultat = saluda.SaludaEn(nom);

            // Assert
            Assert.Equal($"{expected} {nom}", resultat);
        }

        [Theory]
        [InlineData("Joan de Palol")]
        public void ComprovaQueSiTeMesDeTresNomsEsUnSenyor(string nom)
        {
            // Arrange 

            // Act
            var resultat = saluda.SaludaEn(nom);

            // Assert
            Assert.EndsWith("senyor " + nom, resultat);
        }

    }
}
