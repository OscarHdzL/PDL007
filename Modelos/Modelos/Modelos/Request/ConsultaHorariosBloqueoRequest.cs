using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class ConsultaHorariosBloqueoRequest
    {
        public int? id_usuario_dictaminador { get; set; }
        public string fecha_cotejo_inicio { get; set; }
        public string fecha_cotejo_fin { get; set; }
    }
}
