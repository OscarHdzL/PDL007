using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class ConsultaDetalleCotejoRequest
    {
        public int? cotejo_tipo { get; set; }
        public int? s_id_us { get; set; }
        public int? c_id { get; set; }
    }
}
