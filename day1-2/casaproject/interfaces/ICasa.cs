
namespace casaproject
{
    public interface ICasa
    {
        bool EntraPersona(IPersona persona);
        void ObrePorta();
        int QuantesPersonesHiHa();
        IPersona SurtPersona(string nom);
        void TancaPorta();
    }
}