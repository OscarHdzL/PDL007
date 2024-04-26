using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class ReporteResponse
    {
        //public int? i_id_tramite { get; set; }
        public string c_denominacion { get; set; }
        public string c_nRegistro { get; set; }
        public string estatus_tramite { get; set; }
        public string domicilio_legal { get; set; }
        public string representante_legal { get; set; }
        public string credo { get; set; }
        public DateTime? fecha_registro { get; set; }
    }

    public class ReporteResponseTnota
    {
        public string c_denominacion { get; set; }
        public string c_nRegistro { get; set; }
        public string estatus_tramite { get; set; }
        public DateTime? fecha_registro { get; set; }
        public string movimientostnota { get; set; }
    }
}
