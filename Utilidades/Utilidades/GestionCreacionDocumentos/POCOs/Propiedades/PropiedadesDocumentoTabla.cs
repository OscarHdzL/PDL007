using System.Collections.Generic;
using Utilidades.GestionCreacionDocumentos.Implementar.Decoradores;

namespace Utilidades.GestionCreacionDocumentos.POCOs.Propiedades
{
    /// <summary>
    /// Clase que tendra las propiedades de la tablas
    /// </summary>
    public class PropiedadesDocumentoTabla : PropiedadesDocumentoBase
    {
        #region Propiedades 
        /// <summary>
        /// Descripcion de las propiedades que tendran las cabezeras de las tablas
        /// </summary>
        public List<ConvertirPropiedades> CabezerasTabla { get; set; }

        /// <summary>
        /// Propriedad que contendra el contenido de las tablas
        /// </summary>
        public List<List<ConvertirPropiedades>> ContenidoTablas { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor inicial de la tablas
        /// </summary>
        public PropiedadesDocumentoTabla()
        {
            CabezerasTabla = new ();
            ContenidoTablas = new ();
        }
        #endregion
    }
}
