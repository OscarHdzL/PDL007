using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class ConsultaDetalleTomaNotaDomicilioLegalRequest
    {
        public int? i_id_c { get; set; }
        public int? s_id_us { get; set; }
        public int? c_id_trtn { get; set; }
        public int? c_tipo_domicilio { get; set; }
    }
}
