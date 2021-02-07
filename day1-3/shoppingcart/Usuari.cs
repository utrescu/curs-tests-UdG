using System;
using shopcart.Interfaces;

namespace shopcart
{
    public class Usuari : IUsuari
    {

        public Usuari(string usuari)
        {
            Nom = usuari;
            Adreca = "";
            DataAlta = DateTime.Now;

        }

        public Usuari(string nom, string adreca, DateTime dataAlta)
        {
            Nom = nom;
            Adreca = adreca;
            DataAlta = DateTime.Now;
        }
        
        public string Nom { get; set; }
        public string Adreca { get; set; }
        public DateTime DataAlta { get; set; }

        public bool EsVIP()
        {
            var ara = DateTime.Now;
            return DateTime.Now - DataAlta >= TimeSpan.FromDays(60);
        }

        public override string ToString()
        {
            return $"{Nom}";
        }
    }
}