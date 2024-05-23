using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class ConsultaEstatusTransmisionResponse
    {
        public int? id_tbl_transmision { get; set; }
        public string c_numero_sgar { get; set; }
        public string c_denominacion { get; set; }
        public string c_fecha { get; set; }
        public int? id_tbl_estatus { get; set; }
        public string c_estatus { get; set; }
        public int? i_id_tbl_dictaminador { get; set; }
        public string nombre_dictaminador { get; set; }
    }
}
