using shopcart.Interfaces;

namespace shopcart
{
    internal class Product : IProduct
    {
        public Product(string nom, double preu, double pes)
        {
            Nom = nom;
            Preu = preu;
            Pes = pes;
        }

        public string Nom { get; }
        public double Preu { get; set; }

        public double Pes { get; }
        public bool EsPesat()
        {
            return Pes > 20.0;
        }
    }
}