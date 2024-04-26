using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class ValidaCorreoResponse
    {
        public int? IdUsuario {get;set;}
        public string Correo {get;set;}
        public int? IdConvocatoria { get; set; }
        public string Mensaje {get;set;}
        public bool? EsAceptado { get; set; }
    }
}
