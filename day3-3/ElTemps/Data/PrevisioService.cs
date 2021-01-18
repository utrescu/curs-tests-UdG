using System;
using System.Linq;
using System.Threading.Tasks;

namespace ElTemps.Data
{

    public class PrevisioService
    {
        private static readonly string[] Pobles = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<Previsio[]> GetPrevisioSetmana(string poble)
        {
            var rng = new Random();
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new Previsio
            {

                Dia = "1",
                Poble = poble,
                Imatge = ""

            }).ToArray());

        }

        public Task<string[]> GetPobles()
        {
            return Task.FromResult(new[] { "a", "b", "c" });
        }
    }

}
