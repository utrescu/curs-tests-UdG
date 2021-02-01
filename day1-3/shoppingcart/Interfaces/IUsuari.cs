using System;

namespace shopcart.Interfaces
{
    public interface IUsuari
    {
        public string Nom { get; }
        public string Adreca { get; set; }
        public DateTime DataAlta {get; set;}
        public bool EsVIP();
    }
}