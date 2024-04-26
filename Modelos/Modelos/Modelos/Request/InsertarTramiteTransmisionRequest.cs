using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class InsertarTramiteTransmisionRequest
    {
        public int id_transmision { get; set; }
        public int id_usuario { get; set; }
        public string denominacion { get; set; }
        public string numero_sgar { get; set; }
        public string domicilio { get; set; }
        public string correo_electronico { get; set; }
        public string numero_tel { get; set; }
        public string rep_nombre_completo { get; set; }
    }
}
