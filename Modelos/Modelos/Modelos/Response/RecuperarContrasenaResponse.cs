using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class RecuperarContrasenaResponse
    {
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string CorreoUsuario { get; set; }
        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public string ContrasenaUsuario { get; set; }
    }
}
