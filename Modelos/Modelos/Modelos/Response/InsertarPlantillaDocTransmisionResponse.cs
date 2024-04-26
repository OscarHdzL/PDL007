using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    /// <summary>
    /// Clase de regresar el response de la operacion
    /// </summary>
    public class InsertarPlantillaDocTransmisionResponse
    {
        public int? id { get; set; }
        public bool proceso_exitoso { get; set; }
        public string mensaje { get; set; }
    }

    /// <summary>
    /// Clase de regresar el response de la operacion actulizar
    /// </summary>
    public class ActulizarPlantillaDocTransmisionResponse
    {
        public int? id { get; set; }
        public bool proceso_exitoso { get; set; }
        public string mensaje { get; set; }
    }

}
