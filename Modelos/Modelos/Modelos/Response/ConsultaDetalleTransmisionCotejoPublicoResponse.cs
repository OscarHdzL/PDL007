using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class ConsultaDetalleTransmisionCotejoPublicoResponse
    {
        public int s_id_tramite { get; set; }
        public int? s_estatus { get; set; }
        public string s_fecha { get; set; }
        public string s_direccion { get; set; }
        public string s_comentarios { get; set; }
        public string usuario { get; set; }
    }
}