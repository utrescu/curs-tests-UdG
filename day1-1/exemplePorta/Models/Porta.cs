using System;
using System.Collections.Generic;
using System.Text;

namespace exemplePorta.Models
{
    class Porta
    {
        private bool _esOberta;

        public void Acciona()
        {
            _esOberta = !_esOberta;
        }

        public bool EsOberta()
        {
            // Error!
            return false;
        }
    }
}
