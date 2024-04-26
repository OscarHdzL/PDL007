using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades.GestionCreacionDocumentos.POCOs.Propiedades;
using Xceed.Words.NET;

namespace Utilidades.GestionCreacionDocumentos.POCOs
{
    /// <summary>
    /// Propiedades del documento
    /// </summary>
    public class DocumentoWord : ElementoDocumento
    {
        #region Propiedades
        public DocX Documento { get; set; }
        public List<string> Parrafos { get; set; }
        public string NombreDocumento { get; set; }
        #endregion

        #region Constructor
        public DocumentoWord(string rutaArchivo)
        {
            Parrafos = new List<string>();
            NombreDocumento = ObtenerNombreDocumento(rutaArchivo);
            Documento = DocX.Create(NombreDocumento);
        }
        #endregion

        #region Métodos privados
        private string ObtenerNombreDocumento(string rutaArchivo)
            => $"{rutaArchivo}Cuerpo_Oficio_{ Guid.NewGuid()}.docx";
        #endregion
    }
}
