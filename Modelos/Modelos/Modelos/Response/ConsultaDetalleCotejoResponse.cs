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
    public class ConsultaDetalleCotejoResponse
    {
        public int s_id { get; set; }
        public int s_tipo_cotejo { get; set; }
        public int s_estatus { get; set; }
        public string s_comentarios { get; set; }
        public string s_direccion { get; set; }
        public string s_fecha { get; set; }
        public string s_numero_registro { get; set; }
        public bool? s_cumple { get; set; }
        public bool? s_doc1 { get; set; }
        public bool? s_doc2 { get; set; }
        public string s_noficio_entrada { get; set; }
        public string s_noficio_salida { get; set; }
    }
}
