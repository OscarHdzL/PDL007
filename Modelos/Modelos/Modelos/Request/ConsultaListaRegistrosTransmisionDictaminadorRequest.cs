using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
   public class ConsultaListaRegistrosTransmisionDictaminadorRequest
    {
        public int id_usuario { get; set; }
        public string numero_sgar_desc { get; set; }
        public string denominacion_desc { get; set; }
        public int? estatus_desc { get; set; }
        public string representante_desc { get; set; }
    }
}
