using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class ConsultaTramiteDeclaratoriaListaFiltrosRequest
    {
        public int id_usuario { get; set; } = 0;
        public int id_rol { get; set; } = 0;
        public string denominacion { get; set; }
        public string folio { get; set; }
        public int? estatus { get; set; }
    }
}
