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
    public class ConsultaDetalleTramitePasoSextoResponse
    {
        public int s_id { get; set; }
        public int? s_cat_notarioarr { get; set; }
        public int? s_cat_modalidad { get; set; }
        public bool s_doc_ext_1 { get; set; }
        public bool s_doc_ext_2 { get; set; }
        public bool s_doc_notario { get; set; }
        public int? s_id_cnotorioarr { get; set; }
        
    }
}
