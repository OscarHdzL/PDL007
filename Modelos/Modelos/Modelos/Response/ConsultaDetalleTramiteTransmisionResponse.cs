using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class ConsultaDetalleTramiteTransmisionResponse
    {
        public int c_us { get; set; }
        public int i_id_tbl_transmision { get; set; }
        public string numero_sgar { get; set; }
        public string denominacion { get; set; }
        public string numero_tel { get; set; }
        public string correo_electronico { get; set; }
        public string domicilio { get; set; }
        public string rep_nombre_completo { get; set; }
        public int? b_identificacion { get; set; }
        public int? b_solicitudTrans { get; set; }
        public int? estatus { get; set; }

    }
}
