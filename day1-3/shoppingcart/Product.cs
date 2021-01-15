using shoppingcart.Interfaces;

namespace shoppingcart
{
    internal class Product : IProduct
    {
        public Product(string nom, double preu)
        {
            Nom = nom;
            Preu = preu;
        }

        public string Nom { get; }
        public double Preu { get; set; }
    }
}