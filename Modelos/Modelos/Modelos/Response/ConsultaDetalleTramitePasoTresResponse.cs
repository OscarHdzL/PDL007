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
    public class ConsultaDetalleTramitePasoTresResponse
    {
        public int s_id_tramite { get; set; }
        public int? s_cat_sjuridica { get; set; }
        public string s_f_apertura { get; set; }
        public bool s_file_exist_manifiesto { get; set; }
        public bool s_file_exist_sjuridica { get; set; }

        public int? d_id_domicilio { get; set; }
        public int? d_tipo_domicilio { get; set; }
        public string d_numeroe { get; set; }
        public string d_numeroi { get; set; }
        public int? d_colonia { get; set; }
        public string d_calle { get; set; }
        public string c_cpostal_n { get; set; }
    }
}
