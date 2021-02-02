using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElTemps.Data
{

    public class PrevisioService
    {
        private readonly Dictionary<string, Previsio[]> Previsions;

        private static string[] Dies = {
            "Dilluns", "Dimarts", "Dimecres", "Dijous", "Divendres", "Dissabte", "Diumenge"
        };

        public static string[] Pobles = new[] {
                "Bonyeta",
                "Vilamongat",
                "Sant Ficus",
                "Torreta",
                "VilaFredat",
                "Sant Sol de PuigPelat"
        };

        public PrevisioService()
        {
            Previsions = new Dictionary<string, Previsio[]>();

            foreach (var poble in Pobles)
            {
                Previsions[poble] = GeneraPrevisio(poble);
            }
        }

        private string IncrementaDia(string dia)
        {
            for (int i = 0; i < Dies.Length; i++)
            {
                if (dia == Dies[i])
                {
                    return Dies[(i + 1) % Dies.Length];
                }
            }
            return "ERROR";
        }

        private Previsio[] GeneraPrevisio(string poble)
        {
            var previsions = new Previsio[Dies.Length];
            previsions[0] = new Previsio(poble)
            {
                Dia = Dies[0]
            };

            for (var i = 1; i < Dies.Length; i++)
            {
                previsions[i] = new Previsio(previsions[i - 1])
                {
                    Dia = Dies[i]
                };
            }
            return previsions;
        }

        public Task<Previsio[]> GetPrevisioSetmana(string poble)
        {
            return Task.FromResult(Previsions[poble]);
        }

        public Task<string[]> GetPobles()
        {
            return Task.FromResult(Pobles);
        }

    }
}
