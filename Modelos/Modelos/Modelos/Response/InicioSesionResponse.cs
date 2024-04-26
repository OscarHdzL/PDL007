using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    /// <summary>
    /// Clase del response de inicio de session
    /// </summary>
    public class InicioSesionResponse
    {
        public int? IdUsuario { get; set; }

        //public int? IdContacto { get; set; }

        public int? IdPerfil { get; set; }

        public string Nombre { get; set; }

        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public bool? AvisoPrivacidad { get; set; }

        public int? CodigoError { get; set; }
    }
}
