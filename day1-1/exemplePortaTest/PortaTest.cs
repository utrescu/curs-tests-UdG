using System;
using exemplePorta.Models;
using Xunit;

namespace exemplePortaTest
{
    public class PortaTest
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ComprovaQueLaPortaEsCreaTancada(bool ambClau)
        {
            // Arrange

            // Act
            var porta = new Porta(ambClau);

            // Assert
            Assert.Equal(false, porta.EsOberta());
        }

        [Fact]
        public void ComprovaQueUnaPortaSenseClauEsPotObrir()
        {
            // Arrange
            var porta = new Porta();

            // Act
            porta.Acciona();

            // Assert
            Assert.Equal(true, porta.EsOberta());
        }

        [Fact]
        public void ComprovaQueUnaPortaSenseClauEsPotTancar()
        {
            // Arrange
            var porta = new Porta();
            porta.Acciona();

            // Act
            porta.Acciona();

            // Assert
            Assert.Equal(false, porta.EsOberta());
        }

        [Theory]
        [InlineData(false, 0)]
        [InlineData(true, 1)]
        [InlineData(true, 5)]
        [InlineData(true, 2501)]
        [InlineData(false, 2)]
        [InlineData(false, 100)]
        [InlineData(false, 2000)]
        public void ComprovaQueUnaPortaSenseClauEsPotObrirITancar(bool expected, int accions)
        {
            // Arrange
            var porta = new Porta();

            // Act
            for (int i = 0; i < accions; i++)
            {
                porta.Acciona();
            }

            // Assert
            Assert.Equal(expected, porta.EsOberta());
        }
    }
}
