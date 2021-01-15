using System;
using System.Collections.Generic;
using System.Linq;

namespace casaproject
{
    public class Casa
    {

        private Porta PortaEntrada;
        private List<Persona> GentADins;

        public Casa(Porta portaEntrada)
        {
            PortaEntrada = portaEntrada;
            GentADins = new List<Persona>();
        }

        public void ObrePorta() => PortaEntrada.Obre();

        public void TancaPorta() => PortaEntrada.Tanca();

        public bool EntraPersona(Persona persona) {
            if (PortaEntrada.EstaOberta()) {
                GentADins.Add(persona);
                return true;
            }
            return false;
        }

        public Persona SurtPersona(string nom)
        {
            if (!PortaEntrada.EstaOberta()) {
                return null;
            }

            Persona persona = GentADins.FirstOrDefault(p => p.Nom == nom);
            if (persona != null) {
                GentADins.Remove(persona);
            }
            return persona;
        }

        public int QuantesPersonesHiHa() => GentADins.Count;



    }

}