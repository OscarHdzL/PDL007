using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class ActualizarCatalogoPaisoRequest
    {
        public int c_id { get; set; }
        public string c_nombre_n { get; set; }
        public string c_descripcion_n { get; set; }
        public string c_f_inic_vig { get; set; }
        public string c_f_fin_vig { get; set; }
    }
}
