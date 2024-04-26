using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    /// <summary>
    /// Clase del response de Pre-Registro
    /// </summary>
    public class PreRegistroResponse
    {
        public string contrasenaTemporal { get; set; }
        public int idusuario { get; set; }
    }
}
