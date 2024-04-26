using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class BorraUsuarioSistemaRequest
    {
        public int? id_usuario { get; set; }
        public int? estatus { get; set; }
    }
}
