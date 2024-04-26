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
    public class ActualizarDomicilioRequest
    { 
        public int? d_id_domicilio { get; set; }
        public int? d_tipo_domicilio { get; set; }
        public string d_numeroe { get; set; }
        public string d_numeroi { get; set; }
        public int? d_colonia { get; set; }
        public string d_calle { get; set; }

    }
}
