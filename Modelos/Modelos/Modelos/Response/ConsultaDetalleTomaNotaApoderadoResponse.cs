using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class ConsultaDetalleTomaNotaApoderadoResponse
    {
        public int s_id { get; set; }
        public int p_id { get; set; }
        public int r_id { get; set; }
        public string p_nombre_completo { get; set; }
        public string p_nombre { get; set; }
        public string p_apaterno { get; set; }
        public string p_amaterno { get; set; }
        public int c_poder { get; set; }
        public string c_nacionalidad { get; set; }
        public int c_edad { get; set; }
        public int c_tipo_movimiento { get; set; }
    }
}
