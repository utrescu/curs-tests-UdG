using System;
using System.Linq;

namespace Saludador
{
    public interface IDateWrapper
    {
        DateTime ObtenirHora();
    }

    public class DateWrapper : IDateWrapper
    {
        public DateTime ObtenirHora()
        {
            return DateTime.Now;
        }
    }



    public class Saluda
    {
        private IDateWrapper hora;

        public Saluda(IDateWrapper objecte)
        {
            hora = objecte;
        }

        public string SaludaEn(string nom)
        {
            var missatge = "Hola";
            var senyor = "";
            var nomtmp = nom.Trim();
            nomtmp = nomtmp.First().ToString().ToUpper() + nomtmp.Substring(1);

            var horaActual = hora.ObtenirHora().Hour;

            switch (horaActual)
            {
                case int n when (n >= 6 && n < 12):
                    missatge = "Bon dia";
                    break;
                case int n when (n >= 14 && n < 21):
                    missatge = "Bona tarda";
                    break;
                case int n when (n >= 21 && n < 24 || n >=0 && n < 6):
                    missatge = "Bona nit";
                    break;
            }

            // Es senyor
            if (nomtmp.Split(' ').Length >= 3)
            {
                senyor = " senyor";
            } 

            return $"{missatge}{senyor} {nomtmp}";
        }
    }
}
