namespace TransportDeVaques
{

    public class Vaca : IVaca
    {
        public string Nom { get; }
        public double Pes { get; set; }
        public IRaca Raça { get; }

        public Vaca(string nom, double pes, IRaca raça)
        {
            Nom = nom;
            Pes = pes;
            Raça = raça;
        }

        public double Litres => Pes * Raça.LitresPerKg;

        public override string ToString()
        {
            return "Vaca{" +
                    "nom='" + Nom + '\'' +
                    ", pes=" + Pes +
                    ", raça=" + Raça.Nom +
                    '}';
        }
    }
}