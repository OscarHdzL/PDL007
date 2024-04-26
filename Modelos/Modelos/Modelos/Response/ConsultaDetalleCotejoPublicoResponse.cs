using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    /// <summary>
    /// Clase del response para consultar datos
    /// </summary>
    public class ConsultaDetalleCotejoPublicoResponse
    {
        public int s_id { get; set; }
        public int s_tipo_cotejo { get; set; }
        public string s_fecha { get; set; }
        public string s_comentarios { get; set; }
        public bool? s_cotejo_v { get; set; }
        public string s_direccion { get; set; }
        public bool? s_cotejo_f { get; set; }
        public string s_comentario_f { get; set; }
        public bool? s_cotejo_c { get; set; }
        public bool? s_cotejo_r { get; set; }
        public string s_comentario_r { get; set; }
        public string s_comentario_c { get; set; }
        public bool? s_doc1 { get; set; }
        public bool? s_doc2 { get; set; }
        public string usuario { get; set; }
        public string s_noficio_entrada { get; set; }
        public string s_noficio_salida { get; set; }

    }
}
