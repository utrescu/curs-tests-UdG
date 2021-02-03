using System.Collections.Generic;

namespace TransportDeVaques
{

    public class Camio : ICamio
    {
        private double MaxPes;
        public double PesActual { get; private set; }
        public double Litres { get; private set; }
        private List<IVaca> vaques;

        public Camio(double maxPes)
        {
            MaxPes = maxPes;
            PesActual = 0;
            Litres = 0;
            vaques = new List<IVaca>();
        }

        public List<IVaca> Vaques()
        {
            return vaques;
        }

        public void BuidaCamio()
        {
            PesActual = 0;
            Litres = 0;
            vaques.Clear();
        }

        public bool EntraVaca(IVaca vaca)
        {
            if (PesActual + vaca.Pes <= MaxPes)
            {
                PesActual += vaca.Pes;
                Litres += vaca.Litres;
                vaques.Add(vaca);
                return true;
            }
            return false;
        }

        public void TreuVaca(IVaca vaca)
        {
            PesActual -= vaca.Pes;
            Litres -= vaca.Litres;
            vaques.Remove(vaca);
        }
    }
}