using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class ActualizarTomaNotaComentarioRequest
    {
        public int? c_id { get; set; }
        public string c_comentario { get; set; }
        public string c_numero_sgar { get; set; }
        public string c_denominacion { get; set; }
    }
}
