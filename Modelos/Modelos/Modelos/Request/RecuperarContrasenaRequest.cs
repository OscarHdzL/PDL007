using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Utilidades.Request
{
    public class RecuperarContrasenaRequest
    {

        public string ContraUsuario { get; set; }
        public string TokenContrasena { get; set; }
        public int? idUsuario { get; set; }

        public string? url { get; set; }

    }
}
