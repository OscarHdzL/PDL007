using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    /// <summary>
    /// Clase encargada de regresar la autenticacion
    /// </summary>
    public class AutenticacionResponse
    {
        public string Token { get; set; }
        public string Mensaje { get; set; }
    }
}
