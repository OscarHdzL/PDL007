using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    /// <summary>
    /// Clase de regresar el resquest de la operacion
    /// </summary>
    public class BorrarPlantillaDocTransmisionRequest
    {
        public int? i_id { get; set; }
        public string c_nombre { get; set; }
        public string c_ruta { get; set; }
    }
}
