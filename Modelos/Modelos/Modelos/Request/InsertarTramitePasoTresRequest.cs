using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    /// <summary>
    /// Clase del request para el registro paso uno tramite
    /// </summary>
    public class InsertarTramitePasoTresRequest
    {
        public int s_cat_sjuridica { get; set; }
        public string s_f_apertura { get; set; }
        public InsertarDomicilioRequest s_domicilio { get; set; }

    }
}
