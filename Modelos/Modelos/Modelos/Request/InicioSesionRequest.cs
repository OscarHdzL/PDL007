using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Modelos.Modelos.Request
{
    /// <summary>
    /// Clase del request para el inicio de sesion
    /// </summary>
    public class InicioSesionRequest
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        public string usuarioCorreo { get; set; }

        [Required]
        public string contrasenia { get; set; }
    }
}
