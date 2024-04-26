using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class ValidaCorreoRequest
    {
        public string CorreoUsuario { get; set; }
        public int? IdConvoca { get; set; }
    }
}
