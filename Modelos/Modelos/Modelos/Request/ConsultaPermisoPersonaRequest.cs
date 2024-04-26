using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class  ConsultaPermisoPersonaRequest
    {
        public int? id_usuario { get; set; }
        public int? id_tramite { get; set; }
    }
}
