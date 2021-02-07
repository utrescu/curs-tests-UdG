using shopcart.Interfaces;
using shopcart;
using System;
using Xunit;
using System.Diagnostics.Contracts;
using System.Numerics;

namespace shopcart
{
    public class ProductTest
    {
        [Theory] 
        // casos normals
        [InlineData(25, true)]
        [InlineData(5, false)]
        // Marges i extrems
        [InlineData(20, false)]        
        [InlineData(int.MaxValue, true)]
        [InlineData(0, false)]
        public void ComprovaQueEsRetornaCorrectamentLaFuncionalitatPesat(double pes, bool expected)
        {
            // Arrange            
            IProduct product = new Product("qualsevol", 3.5, pes);

            // Act
            var resultat = product.EsPesat();

            // Assert
            Assert.Equal(expected, resultat);
        }

        [Fact]
        public void ComprovaQueElPesImpossibleRetornenError()
        {
            var pes = -5.0;
            // Arrange
            IProduct product = new Product("qualsevol", 3.5, pes);

            // Act
            void esPesat() => product.EsPesat();

            // Assert
            var excepcio = Assert.Throws<Exception>( esPesat );

            Assert.Equal("Els pesos negatius no existeixen", excepcio.Message);

        }
    }
}
