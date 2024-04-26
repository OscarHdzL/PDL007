using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class ActualizarCotejoRequest
    {
        public int cotejo_tipo { get; set; }
        public int s_tipo_cotejo { get; set; }

        public int s_id { get; set; }
        public int s_us_id { get; set; }
        public int s_estatus { get; set; }
        public string s_direccion { get; set; }
        public string s_comentarios { get; set; }
        public string s_fecha{ get; set; }
        public string s_horario { get; set; }
        public string s_numero_registro { get; set; }
        public string s_noficio_entrada { get; set; }
        public string s_noficio_salida { get; set; }
    }
}
