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
    public class ConsultaListaTramitesResponse
    {
        public int reg_id { get; set; }
        public string reg_cat_solicitud_escrito { get; set; }
        public string reg_cat_denominacion { get; set; }
        public DateTime reg_fecha { get; set; }
        public string reg_numero_registro { get; set; }
        public string reg_pais_origen { get; set; }
        public string reg_estatus { get; set; }
        public string correo_dic { get; set; }
        
        //public bool? c_activo { get; set; }
    }
}
