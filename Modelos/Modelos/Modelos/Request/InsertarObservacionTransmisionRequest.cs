using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class InsertarObservacionTransmisionRequest
    {
        public int id_transmision { get; set; }
        public string observacion { get; set; }
        public int? id_estatus { get; set; }
        public int? id_usuario { get; set; }
    }
}
