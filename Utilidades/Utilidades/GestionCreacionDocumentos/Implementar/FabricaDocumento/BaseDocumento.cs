using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Utilidades.GestionCreacionDocumentos.Implementar.FabricaDocumento
{
    /// <summary>
    /// Propiedade de los estilos que contendra un documento
    /// </summary>
    public class BaseDocumento
    {
        #region Propiedades
        /// <summary>
        /// Ruta de la plantilla actual
        /// </summary>
        public string RutaPlantilla { get; set; }

        /// <summary>
        /// Propiedad que contendra los parrafos en el orden correspondiente.
        /// </summary>
        public Dictionary<int, Paragraph> Parrafos { get; set; }

        /// <summary>
        /// Propiedad que contendra las tablas.
        /// </summary>
        public Dictionary<int, Table> Tablas { get; set; }

        /// <summary>
        /// Propiedad para obtener el total de los elementos de los XML
        /// </summary>
        /// <returns></returns>
        public int ElementosXML() 
            => Parrafos.Count + Tablas.Count;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor inicial
        /// </summary>
        public BaseDocumento()
        {
            Parrafos = new();
            Tablas = new();
        }
        #endregion

        #region Métodos pricados
        /// <summary>
        /// Método para obtner las propiedades
        /// </summary>
        /// <returns></returns>
        public SectionProperties PropiedadesDocumento()
        {
            var documento = new SectionProperties();

            documento.Append(new PageSize()
            {
                Width = 12240U,
                Height = 16990U,
                Orient =  PageOrientationValues.Portrait
            });

            documento.Append(new PageMargin() 
            { 
                Top = 720,
                Bottom = 720,
                Right = 650U, 
                Left = 650U 
            });

            return documento;
        }
        #endregion
    }
}
