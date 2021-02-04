using System.Collections.Generic;

namespace TransportDeVaques
{
    public interface ICamio
    {
        double PesActual { get; }
        double Litres { get; }
        void BuidaCamio();
        bool EntraVaca(IVaca vaca);
        List<IVaca> Vaques();
        void TreuVaca(IVaca vaca);
    }
}