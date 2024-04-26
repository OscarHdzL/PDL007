using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class ReporteTransmisionRequest
    {
        public string medio_comunicacion { get; set; }
        public int? estatus_transmision { get; set; }
        public string denominacion { get; set; }
        public string acto_religioso { get; set; }
        public DateTime? fecha_inicio { get; set; }
        public DateTime? fecha_fin { get; set; }
    }
}
