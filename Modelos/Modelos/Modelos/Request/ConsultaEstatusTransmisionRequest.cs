using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class ConsultaEstatusTransmisionRequest
    {
        public int? id_estatus { get; set; }
        public int? id_dictaminador { get; set; }
    }

    public class ConsultaEstatusTransmisionFiltradoRequest
    {
        public int? id_asignador { get; set; }
        public int? id_estatus { get; set; }
        public int? id_dictaminador { get; set; }
        public string busqueda { get; set; }
    }
}
