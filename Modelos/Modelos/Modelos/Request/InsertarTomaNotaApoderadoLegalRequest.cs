using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class InsertarTomaNotaApoderadoLegalRequest
    {
        public int s_id { get; set; }
        public int p_id { get; set; }
        public string p_nombre { get; set; }
        public string p_apaterno { get; set; }
        public string p_amaterno { get; set; }

        public int c_id_tipo_movimiento { get; set; }
        public int c_id_poder { get; set; }

        public string p_nacionalidad { get; set; }
        public int p_edad { get; set; }
    }
}
