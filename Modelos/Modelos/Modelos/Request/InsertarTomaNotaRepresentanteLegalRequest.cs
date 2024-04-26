using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class InsertarTomaNotaRepresentanteLegalRequest
    {
        public int s_id { get; set; }
        public int p_id { get; set; }
        public string p_nombre { get; set; }
        public string p_apaterno { get; set; }
        public string p_amaterno { get; set; }
        
        //public bool t_rep_legal { get; set; }

        public int c_id_tipo_movimiento { get; set; }
        public int c_id_poder { get; set; }
        public string p_cargo { get; set; }
        public string c_organo_g { get; set; }
        public bool? t_rep_legal { get; set; }
        public bool? t_rep_asociado { get; set; }
        public bool? t_ministro_culto { get; set; }
        public bool? t_organo_gob { get; set; }
    }
}
