using DocumentFormat.OpenXml.Wordprocessing;

namespace Utilidades.GestionCreacionDocumentos.POCOs.Propiedades
{
    /// <summary>
    /// Propiedades del documento base
    /// </summary>
    public class PropiedadesDocumentoBase
    {
        #region Propiedades base
        public JustificationValues TipoJustificacion { get; set; }
        public string TipoLetra { get => "Montserrat"; }
        #endregion
    }
}
