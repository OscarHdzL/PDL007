using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class AsignarTransmisionDictaminadorRequest
    {
        public int id_transmision { get; set; }
        public int id_usuario_dictaminador { get; set; }
        public int id_usuario_asignador { get; set; }
    }
}
