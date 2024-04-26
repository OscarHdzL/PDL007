using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Utilidades.GestionCreacionDocumentos.Implementar.Decoradores;
using Utilidades.GestionCreacionDocumentos.Interface;
using Utilidades.GestionCreacionDocumentos.POCOs.Propiedades;

namespace Utilidades.GestionCreacionDocumentos.Implementar.FabricaDocumento
{
    /// <summary>
    /// Clase encargada de generar un documento 
    /// </summary>
    public class DocumentoOficio : BaseDocumento, IDocumento, ISeccionDocumento, IDisposable
    {
        #region Constructor
        /// <summary>
        /// Constructor inical
        /// </summary>
        /// <param name="rutaDocumento"></param>
        public DocumentoOficio(string rutaDocumento)
            : base()
        {
            RutaPlantilla = rutaDocumento;
        }
        #endregion

        #region Métodos publicados
        /// <summary>
        /// Método encargado de agregar el texto de un parrafo.
        /// </summary>
        public void AgregarParrafo(PropiedadesDocumentoTexto propiedades, int OrdenElemento)
        {
            var parrafo = new DecoradorParrafo(propiedades);
            Parrafos.Add(OrdenElemento, parrafo.Parrafo);
        }

        /// <summary>
        /// Método encargado de agregar una tabla al documento.
        /// </summary>
        public void AgregarTabla(PropiedadesDocumentoTabla propiedade, int OrdenElemento)
        {
            var tabla = new DecoradorTabla(propiedade);
            Tablas.Add(OrdenElemento, tabla.Tabla);
        }

        /// <summary>
        /// Método enccargado de crear el documento
        /// </summary>
        /// <returns></returns>
        public ValueTask<byte[]> GenerarDocumento()
        {
            try
            {

                using MemoryStream memoriaTemp = new();

                using var fileStream = new FileStream(RutaPlantilla, FileMode.Open, FileAccess.Read);
                          fileStream.CopyTo(memoriaTemp);

                using var docPlantilla = WordprocessingDocument.Open(memoriaTemp, isEditable: true);

                docPlantilla.ChangeDocumentType(DocumentFormat.OpenXml.WordprocessingDocumentType.Document);

                docPlantilla.MainDocumentPart.Document.Body.Append(PropiedadesDocumento());

                for (int orden = 1; orden <= ElementosXML(); orden++)
                {
                    if (Parrafos.ContainsKey(orden))
                    {
                        var elementParrafo = Parrafos.GetValueOrDefault(orden);
                        if (elementParrafo != null)
                            docPlantilla.MainDocumentPart.Document.AppendChild(elementParrafo);
                    }
                    else if (Tablas.ContainsKey(orden))
                    {
                        var elementTabla = Tablas.GetValueOrDefault(orden);
                        if (elementTabla != null)
                            docPlantilla.MainDocumentPart.Document.AppendChild(elementTabla);
                    }
                }

                docPlantilla.Close();

                return new ValueTask<byte[]>(memoriaTemp.ToArray());
            }
            finally
            {
                Dispose();
            }
        }

        /// <summary>
        /// Método encargado de liberar la memoria de recuros
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
