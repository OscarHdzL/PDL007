using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelos.Modelos.Utilidades
{
    /// <summary>
    /// ViewModel de los datos del usuario
    /// </summary>
    public class UsuarioViewModel
    {
        public int? IdUsuario { get; set; }
        public int? IdContacto { get; set; }
        public int? IdPerfil { get; set; }
        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public bool? AvisoPrivacidad { get; set; }
        public int? CodigoError { get; set; }
    }
}
