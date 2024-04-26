using Modelos.Modelos.Response;
using System.Collections.Generic;

namespace Utilidades.GestionCreacionDocumentos.POCOs.Modelos
{
    /// <summary>
    /// Clase de convertir el texto.
    /// </summary>
    public class ModeloOficio
    {
        #region Propiedades
        public List<ModeloValor> DatosOficio { get; private set; }
        #endregion

        #region Constructor
        public ModeloOficio(ConsultaOficioTransmisionResponse response)
        {
            DatosOficio = new List<ModeloValor>
            {
                new ModeloValor("REFERENCIA", ':', response.referencia),
                new ModeloValor("EXPEDIENTE", ':', response.expediente),
                new ModeloValor("OFICIO",     ':', response.oficio)
            };
        }
        #endregion
    }
}
