using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades.GestionCreacionDocumentos.Interface;
using Utilidades.GestionCreacionDocumentos.POCOs;
using Utilidades.GestionCreacionDocumentos.POCOs.Propiedades;

namespace Utilidades.GestionCreacionDocumentos.Implementar
{
    /// <summary>
    /// Clase encargada de cumplir en contrato para la construccion del docuemnto
    /// </summary>
    public class WordConstructor : IDocumentoConstructor
    {
        #region Propiedades
        private DocumentoWord _documentoWord;
        private string RutaEscritura { get; set; } 
        public string Nombre { get => _documentoWord.NombreDocumento;  }
            
        #endregion

        #region Constructor
        public WordConstructor(string rutaArchivo) 
        {
            RutaEscritura = rutaArchivo;
            Iniciar();
        }
        #endregion

        #region Métodos de construccion 
        /// <summary>
        /// Método encargado de iniciar un nuevo documento.
        /// </summary>
        public void Iniciar()
            => _documentoWord = new DocumentoWord(RutaEscritura);

        /// <summary>
        ///  Método encargado de agregar un parrafo al docuemnto.
        /// </summary>
        /// <param name="elemento"></param>
        public void AgregarElemento(string elemento)
             =>  _documentoWord.Parrafos.Add(elemento);

        /// <summary>
        /// Método encargado de crear o escribir el docuemnto.
        /// </summary>
        /// <returns></returns>
        public void Crear()
            => _documentoWord.Documento.Save();

        /// <summary>
        /// Método encargado de crear un nuevo parrafo.
        /// </summary>
        /// <param name="propiedades"></param>
        public void RenderizaParrafo(ElementoDocumento propiedades) 
        {
            var _parrfoActual = _documentoWord.Documento.InsertParagraph();
            _parrfoActual.Alignment = propiedades.Alinacion;
            _parrfoActual.SpacingAfter(propiedades.EspacioEntreParrafos);
            _parrfoActual.Bold(propiedades.Negritas);
            _parrfoActual.FontSize(propiedades.TamanioLetra);
            _parrfoActual.Font(new Xceed.Document.NET.Font($"{propiedades.Letra}"));
            _parrfoActual.Append(ObtenerTexto());
            LimpiarParrafo();
        }

        /// <summary>
        /// Método encargado de rendizar texto en forma de renglones
        /// </summary>
        /// <param name="propidades"></param>
        public void RendirazarRenglones(ElementoDocumento propiedades) 
        {
            var _parrfoActual = _documentoWord.Documento.InsertParagraph();
            _parrfoActual.Alignment = propiedades.Alinacion;
            _parrfoActual.Bold(propiedades.Negritas);
            _parrfoActual.FontSize(propiedades.TamanioLetra);
            _documentoWord.Parrafos.ForEach(renglon =>  _parrfoActual.AppendLine(renglon) );
            LimpiarParrafo();
        }

        #endregion

        #region Métodos privados
        /// <summary>
        /// Método encargado d obtener el texto siguiente.
        /// </summary>
        /// <returns></returns>
        private string ObtenerTexto()
            => _documentoWord.Parrafos.Aggregate((arg, argSiguiente) => $"{arg} {argSiguiente}");

        /// <summary>
        /// Método encargado de limpiar el parrafo actual.
        /// </summary>
        private void LimpiarParrafo()
            => _documentoWord.Parrafos.Clear();
        #endregion
    }
}
