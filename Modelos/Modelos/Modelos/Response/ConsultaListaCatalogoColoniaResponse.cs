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
    public class ConsultaListaCatalogoColoniaResponse
    {
        public int c_id { get; set; }
        public string c_nombre_n { get; set; }
        public string c_descripcion_n { get; set; }
        public DateTime c_f_inic_vig { get; set; }
        public DateTime c_f_fin_vig { get; set; }
        public string municipio { get; set; }
        public string c_cpostal_n { get; set; }
        public int id_municipio { get; set; }
        public int id_estado { get; set; }
        public string estado { get; set; }


        public bool c_activo { get; set; }
    }
}
