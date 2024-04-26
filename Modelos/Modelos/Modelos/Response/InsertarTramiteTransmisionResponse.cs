using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class InsertarTramiteTransmisionResponse
    {
        public int i_id_transmision { get; set; }
        public string mensaje { get; set; }
        public bool proceso_exitoso { get; set; }
    }
}
