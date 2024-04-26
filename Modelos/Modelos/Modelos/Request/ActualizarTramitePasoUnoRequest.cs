using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class ActualizarTramitePasoUnoRequest
    {
        public int s_id { get; set; }
        public int s_cat_credo { get; set; }
        public int s_cat_solicitud_escrito { get; set; }
        public string s_cat_denominacion { get; set; }
        public string s_numero_registro { get; set; }
        public int s_pais_origen { get; set; }
        public string c_matriz { get; set; }
        public ActualizarDomicilioRequest s_domicilio { get; set; }
    }
}
