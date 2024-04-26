using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    /// <summary>
    /// Clase del response del registtro compleo del usuario
    /// </summary>
    public class RegistroCompletoResponse
    {
        public string usuariosInformacion { get; set; }
        public string mensaje { get; set; }
        public bool? seProcesoExiosamente { get; set; }
    }
}
