using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    /// <summary>
    /// Clase del request para el registro completo del usuario
    /// </summary>
    public class InsertarUsuarioSistemaRequest
    {
        public string nombre { get; set; }
        public string apellido_p { get; set; } 
        public string apellido_m { get; set; } 
        public string correo_electronico { get; set; } 
        public string usuario { get; set; } 
        public string telefono_movil { get; set; } 
        public string contrasena { get; set; } 
        public bool b_privacidad { get; set; }
        public int? id_ca_perfiles { get; set; }
        public string url { get; set; }
    }
}
