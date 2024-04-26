using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class ConsultaActosReligiososDocOficioRequest
    {
        public int i_id_transmision { get; set; }
        public int i_id_acto_religioso { get; set; }
        public int? s_id_us { get; set; }
        public int i_id_tramite { get; set; }
    }
}
