using System;
using System.Linq;

namespace ElTemps.Data
{
    public class Previsio
    {
        private Random random = new Random();
        public Previsio(string poble)
        {
            Poble = poble;
            calculaPrevisio();
        }

        public Previsio(Previsio anterior)
        {
            var canvia = random.Next(2);
            if (canvia == 0) {
                Poble = anterior.Poble;
                TextPrevisio = anterior.TextPrevisio;
            }
            else
            {
                Poble = anterior.Poble;
                var com = random.Next(2);
                if (com == 0) {
                    TextPrevisio = Previsions
                                   .SkipWhile(item => item != anterior.TextPrevisio)
                                   .Skip(1)
                                   .FirstOrDefault();
                }
                else
                {
                    var B = Previsions.ToList();
                    B.Reverse();
                    TextPrevisio = B.SkipWhile(item => item != anterior.TextPrevisio)
                     .Skip(1)
                     .FirstOrDefault();
                    if (TextPrevisio == null) {
                        TextPrevisio = Previsions[0];
                    }
                }
            }
        }

        private void calculaPrevisio() {
            var quin = random.Next(Previsions.Length);
            TextPrevisio = Previsions[quin];
        }

        public int Id { get; set; }
        public string Poble { get; set; }
        public string Dia { get; set; }
        public string TextPrevisio { get; set;}
        public string Imatge { get => $"/img/{TextPrevisio}.png"; }

         private static readonly string[] Previsions = new[]
        {
            "Tempesta", "Pluja", "Núvol", "Assoleiat", "Sol"
        };

    }

}