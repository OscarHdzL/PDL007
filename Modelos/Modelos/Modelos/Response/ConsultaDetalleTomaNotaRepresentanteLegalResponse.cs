using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class ConsultaDetalleTomaNotaRepresentanteLegalResponse
    {
        public int s_id { get; set; }
        public int p_id { get; set; }
        public int r_id { get; set; }
        public string p_nombre_completo { get; set; }
        public string p_nombre { get; set; }
        public string p_apaterno { get; set; }
        public string p_amaterno { get; set; }
        public string p_telefono { get; set; }
        public string p_correo { get; set; }
        public string c_organo_g { get; set; }
        public string p_cargo { get; set; }
        public bool p_ine_exists { get; set; }
        public bool p_acta_exists { get; set; }
        public bool p_curp_exists { get; set; }
        public bool? t_rep_legal { get; set; }
        public bool? t_rep_asociado { get; set; }
        public bool? t_ministro_culto { get; set; }
        public bool? t_organo_gob { get; set; }

        public int c_poder { get; set; }
        public int c_tipo_movimiento { get; set; }
    }
}
