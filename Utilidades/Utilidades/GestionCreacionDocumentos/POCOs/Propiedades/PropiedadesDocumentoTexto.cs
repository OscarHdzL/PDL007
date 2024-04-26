using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using Utilidades.GestionCreacionDocumentos.Implementar.Decoradores;

namespace Utilidades.GestionCreacionDocumentos.POCOs.Propiedades
{
    /// <summary>
    /// Clase que tendra las propiedades de los textos
    /// </summary>
    public class PropiedadesDocumentoTexto : PropiedadesDocumentoBase
    {
        #region Propiedades 
        public ConvertirPropiedades Renglon { get; set; }
        public List<ConvertirPropiedades> Renglones { get; set; }
        #endregion

        #region Constructor
        public PropiedadesDocumentoTexto()
        {
            Renglones = new();
        }
        #endregion
    }
}
