using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class ConsultaDetalleUsuarioSistemaResponse
    {
        public int? id_usuario { get; set; }
        public int? id_persona { get; set; }
        public int? id_perfil { get; set; }
        public string usuario { get; set; }
        public string nombre { get; set; }
        public string apellido_paterno { get; set; }
        public string apellido_materno { get; set; }
        public string telefono_movil { get; set; }
        public string nombre_perfil { get; set; }
        public Boolean? estatus { get; set; }
    }
}
