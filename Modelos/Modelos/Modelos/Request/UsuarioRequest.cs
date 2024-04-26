using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    /// <summary>
    /// Clase del request del usuario
    /// </summary>
    public class UsuarioRequest
    {
        public int? usuarioId { get; set; }
        public int? contactoId { get; set; }
        public string correo { get; set; }
        public int? idConvocatoria { get; set; }
    }
}
