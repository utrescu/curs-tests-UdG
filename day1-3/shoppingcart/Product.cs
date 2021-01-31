using System;
using shopcart.Interfaces;

namespace shopcart
{
    public class Product : IProduct
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
            if (Pes < 0)
            {
                throw new Exception("Els pesos negatius no existeixen");
            }
            return Pes > 20.0;
        }
    }
}