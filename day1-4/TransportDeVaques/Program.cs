using System;
using System.Collections.Generic;
using System.Linq;

namespace TransportDeVaques
{
    class Program
    {
        private static Dictionary<string, IRaca> races = new  Dictionary<string, IRaca> {
            { "Holstein-Friesian", new Raca { Nom="Holstein-Friesian", LitresPerKg=0.3 } },
            { "Jersey", new Raca { Nom="Jersey", LitresPerKg=0.1 } },
            { "Simental", new Raca { Nom="Simental", LitresPerKg=0.05 } },
            { "Ayshire", new Raca { Nom="Ayshire", LitresPerKg=0.12 } },
            { "Guernsey", new Raca { Nom="Guernsey", LitresPerKg=0.09 } },
        };

        private static List<IVaca> vaques = new List<IVaca> {
            new Vaca("Toñi", 360.3, races["Holstein-Friesian"]),
            new Vaca("Pepa", 250.25, races["Jersey"]),
            new Vaca("Flor", 400.5, races["Simental"]),
            new Vaca("Maria", 180.7, races["Holstein-Friesian"]),
            new Vaca("Blanca", 99.8, races["Ayshire"]),
            new Vaca("Conxi", 201.7, races["Holstein-Friesian"]),
            new Vaca("Guenya", 173.0, races["Guernsey"]),
            new Vaca("Marta", 280.2, races["Simental"]),
        };

        static void Main(string[] args)
        {
           var pesMaxim = 800;
            var camio = new Camio(pesMaxim);

            // Provem de posar totes les vaques al camió i a veure què passa
            foreach(var vaca in vaques) {
                camio.EntraVaca(vaca);
            }

            System.Console.WriteLine($"Camió porta {camio.PesActual}/{pesMaxim} Kg que faran {camio.Litres} litres de llet");
            var noms = string.Join(",",camio.Vaques().Select(v => v.Nom));
            System.Console.WriteLine($"Vaques: {noms}");

        }
    }
}
