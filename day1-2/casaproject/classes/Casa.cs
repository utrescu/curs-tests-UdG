using System;
using System.Collections.Generic;
using System.Linq;

namespace casaproject
{

    public class Casa : ICasa
    {

        private IPorta PortaEntrada;
        private List<IPersona> GentADins;

        public Casa(IPorta portaEntrada)
        {
            PortaEntrada = portaEntrada;
            GentADins = new List<IPersona>();
        }
        public void ObrePorta() => PortaEntrada.Acciona();

        public void TancaPorta() => PortaEntrada.Acciona();

        public bool EntraPersona(IPersona persona)
        {
            if (PortaEntrada.EsOberta())
            {
                GentADins.Add(persona);
                return true;
            }
            return false;
        }

        public IPersona SurtPersona(string nom)
        {
            if (!PortaEntrada.EsOberta())
            {
                return null;
            }

            IPersona persona = GentADins.FirstOrDefault(p => p.Nom == nom);
            if (persona != null)
            {
                GentADins.Remove(persona);
            }
            return persona;
        }

        public int QuantesPersonesHiHa() => GentADins.Count;

    }

}