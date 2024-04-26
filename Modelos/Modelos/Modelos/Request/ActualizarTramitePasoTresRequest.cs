using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class ActualizarTramitePasoTresRequest
    {
        public int s_id_tramite { get; set; }
        public int s_cat_sjuridica { get; set; }
        public string s_f_apertura { get; set; }
        public ActualizarDomicilioRequest s_domicilio { get; set; }
    }
}
