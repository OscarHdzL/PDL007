using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class ReporteTransmisionListaEstatusResponse
    {
        public int? estatus_id { get; set; }
        public string estatus_nombre { get; set; }
        public bool? estatus_transmision { get; set; }

    }
}
