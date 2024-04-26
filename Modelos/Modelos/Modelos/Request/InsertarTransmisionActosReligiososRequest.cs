using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class InsertarTransmisionActosReligiososRequest
    {
        public int i_id_tbl_transmision { get; set; }
        public int i_id_acto { get; set; }
        public string c_nombre { get; set; }
        public List<InsertarActosFechasRequest> cat_FechasHorario { get; set; }
        public List<InsertarActosEmisorasRequest> cat_Emisoras { get; set; }
    }

    public class InsertarActosFechasRequest
    {
        public int i_id_tbl_acto_religioso { get; set; } 
        public string c_fecha_inicio { get; set; }
        public string c_fecha_fin { get; set; }
        public string c_hora_inicio { get; set; }
        public string c_hora_fin { get; set; }
        public int i_id_cat_periodo { get; set; }
        public string cat_dia { get; set; }
        public string cat_mes { get; set; }
        public string cat_anio { get; set; }
    }

    public class InsertarActosEmisorasRequest {
        public string frecuencia_canal { get; set; }
        public string proveedor { get; set; }
        public string televisora_radiodifusora { get; set; }
        public int i_id_estado { get; set; }
        public bool televisora { get; set; }
        public int i_id_acto { get; set; }
        public string municipio { get; set; }

    }
}