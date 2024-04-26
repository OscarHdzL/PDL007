using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Utilidades.Response
{
    public class ConsultaListaUsuariosSistemaResponse
    {
        public int? id_usuario { get; set; }
        public string usuario { get; set; }
        public string nombre { get; set; }
        public string apellido_paterno { get; set; }
        public string apellido_materno { get; set; }
        public string nombre_perfil { get; set; }
        public int? estatus { get; set; }
    }
}
