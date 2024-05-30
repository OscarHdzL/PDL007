using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class ConsultaActosReligiososResponse
    {
        public int i_id_acto { get; set; }
        public int i_id_tbl_transmision { get; set; }
        public string c_nombre { get; set; }
        public bool b_activo { get; set; }
    }

    public class ConsultaActosMediosTrasmisionResponse
    {
        public int i_id_emisora { get; set; }
        public int? i_id_acto { get; set; }
        public string frecuencia_canal { get; set; }
        public string proveedor { get; set; }
        public string televisora_radiodifusora { get; set; }
        public bool b_televisora { get; set; }
        public int i_id_estado { get; set; }
        public string lugar_transmision { get; set; }
    }

    public class  ConsultaActosFechasResponse
    {
        public int i_id_acto { get; set; }
        public int i_id { get; set; }
        public string c_fecha_inicio { get; set; }
        public string c_fecha_fin { get; set; }
        public string c_hora_inicio { get; set; }
        public string c_hora_fin { get; set; }
        public int? i_id_cat_periodo { get; set; }
        public string c_periodo { get; set; }
        public string cat_dia { get; set; }
        public string cat_mes { get; set; }
        public string cat_anio { get; set; }
    }
}
