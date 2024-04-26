using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
  public  class ConsultaListaRegistrosTomaNotaRequest
    {
        public int id_usuario { get; set; }
        public string denominacion_desc { get; set; }
        public string nombre_desc { get; set; }
        public int? estatus_desc { get; set; }
    }
}
