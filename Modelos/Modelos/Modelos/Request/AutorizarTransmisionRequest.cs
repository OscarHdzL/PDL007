using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class AutorizarTransmisionRequest
    {
        public int id_transmision { get; set; }
        public string fecha { get; set; }
        public string horario { get; set; }
        public string direccion { get; set; }
        public int? id_usuario { get; set; }

    }
}
