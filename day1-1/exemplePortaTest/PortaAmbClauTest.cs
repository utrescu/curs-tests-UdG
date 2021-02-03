using System;
using exemplePorta.Models;
using Xunit;

namespace exemplePortaTest
{
    public class PortaAmbClauTest
    {
        private Porta _porta;
        public PortaAmbClauTest()
        {
            _porta = new Porta(true);
        }

        [Fact]
        public void ComprovaQueUnaPortaAmbClauEsPotObrir()
        {
            // Arrange


            // Act
            _porta.Acciona();

            // Assert
            Assert.Equal(true, _porta.EsOberta());
        }

        [Fact]
        public void ComprovaQueUnaPortaAmbClauEsPotTancar()
        {
            // Arrange: Obro la porta
            _porta.Acciona();

            // Act
            _porta.Acciona();

            // Assert
            Assert.Equal(false, _porta.EsOberta());
        }

        [Theory]
        [InlineData(false, 0)]
        [InlineData(true, 1)]
        [InlineData(true, 5)]
        [InlineData(true, 2501)]
        [InlineData(false, 2)]
        [InlineData(false, 100)]
        [InlineData(false, 2000)]
        public void ComprovaQueUnaPortaAmbClauEsPotObrirITancarSiNoEstaTancada(bool expected, int accions)
        {
            // Arrange

            // Act
            for (int i = 0; i < accions; i++)
            {
                _porta.Acciona();
            }

            // Assert
            Assert.Equal(expected, _porta.EsOberta());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(100)]
        public void ComprovaQueUnaPortaTancadaAmbClauNoObre(int vegades)
        {
            // ARRANGE: tanca la porta amb clau
            _porta.GiraLaClau();

            // ACT
            for (var i = 0; i < vegades; i++)
            {
                _porta.Acciona();
            }

            // Assert
            Assert.Equal(false, _porta.EsOberta());
        }

        [Fact]
        public void ComprovaQueUnaPortaTancadaAmbClauNoTanca()
        {
            // ARRANGE: obre la porta i gira la clau
            _porta.Acciona();
            _porta.GiraLaClau();

            // ACT tanca la porta
            _porta.Acciona();

            // Assert
            Assert.Equal(true, _porta.EsOberta());
        }

    }
}