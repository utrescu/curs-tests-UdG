using System;
using System.Linq;
using System.Threading.Tasks;

namespace ElTemps.Data
{

    public class PrevisioService
    {
        private static readonly string[] Previsions = new[]
        {
            "Neu", "Tempesta", "Pluja", "Núvol", "Assoleiat", "Sol"
        };

        public Task<Previsio[]> GetPrevisioSetmana(string poble)
        {
            var rng = new Random();
            return Task.FromResult(Enumerable.Range(1, 7).Select(index => new Previsio
            {

                Dia = "1",
                Poble = poble,
                Imatge = "/img/cerilla.png"

            }).ToArray());

        }

        public Task<string[]> GetPobles()
        {
            return Task.FromResult(new[] { "a", "b", "c" });
        }
    }

}
