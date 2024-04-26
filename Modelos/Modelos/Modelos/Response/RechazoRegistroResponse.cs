using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class RechazoRegistroResponse
    {
        public string CorreoUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public bool? bndine { get; set; }
        public bool? bndformato { get; set; }
        public bool? bndconstancia { get; set; }

    }
}
