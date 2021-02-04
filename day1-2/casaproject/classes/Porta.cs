using System;
using System.Collections.Generic;
using System.Text;

namespace casaproject
{
    public class Porta : IPorta
    {

        private bool _esOberta;
        private bool _tancatAmbClau;
        private bool _teClau;

        public Porta(bool teClau = false)
        {
            TeClau = teClau;
            _tancatAmbClau = false;
        }

        public void Acciona()
        {
            if (_tancatAmbClau)
            {
                return;
            }
            _esOberta = !_esOberta;
        }

        public bool TeClau { get => _teClau; set => _teClau = value; }

        public void GiraLaClau()
        {
            if (!TeClau)
            {
                return;
            }
            _tancatAmbClau = !_tancatAmbClau;
        }

        public bool EsOberta()
        {
            return _esOberta;
        }
    }
}
