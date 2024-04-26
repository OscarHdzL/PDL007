using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    /// <summary>
    /// Clase del request la insertar una plantilla
    /// </summary>
    public class InsertarPlantillaRequest
    {
        #region Propiedades
        public string c_nombre { get; set; }
        public string ArchivoBase64 { get; set; }
        [JsonIgnore]
        public string c_ruta { get; set; }
        [JsonIgnore]
        public int? i_estatus { get; set; }
        #endregion
    }

    /// Clase del request la actuliza una plantilla
    /// </summary>
    public class ActulizarPlantillaRequest
    {
        #region Propiedades
        public int? i_id { get; set; }
        public string c_nombre { get; set; }
        public string ArchivoBase64 { get; set; }
        public int? i_estatus { get; set; }
        [JsonIgnore]
        public string c_ruta { get; set; }
       
        #endregion
    }
}
