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

        public virtual void Acciona()
        {
            if (_tancatAmbClau)
            {
                return;
            }
            _esOberta = !_esOberta;
        }

        public virtual bool TeClau { get => _teClau; set => _teClau = value; }

        public virtual void GiraLaClau()
        {
            if (!TeClau)
            {
                return;
            }
            _tancatAmbClau = !_tancatAmbClau;
        }

        public virtual bool EsOberta()
        {
            return _esOberta;
        }
    }
}
