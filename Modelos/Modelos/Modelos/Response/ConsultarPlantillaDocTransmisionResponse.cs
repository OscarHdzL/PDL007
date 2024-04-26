using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    /// <summary>
    /// Clase de regresar el response de consulta de los documentos de transmicion
    /// </summary>
    public class ConsultarPlantillaDocTransmisionResponse
    {
        #region Propiedades
        public int? i_id { get; set; }
        public string c_nombre { get; set; }
        public string c_ruta { get; set; }
        public int? i_estatus { get; set; }
        #endregion
    }
}
