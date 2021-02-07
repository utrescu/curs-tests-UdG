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

        // override object.Equals
        public override bool Equals(object obj)
        {
            
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            Product other = obj as Product;
            if (other.Nom == Nom) 
            {
                return true;
            }
            return false;
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            var hash = 123456;
            return hash * Nom.GetHashCode();
        }
    }
}