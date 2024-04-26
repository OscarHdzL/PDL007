using System;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using Utilidades.GestionCreacionDocumentos.POCOs.Modelos;

namespace Utilidades.GestionCreacionDocumentos.Implementar.Decoradores
{
    /// <summary>
    /// Clase de las propiedades de cada texto para convertir en  XML
    /// </summary>
    public class ConvertirPropiedades
    {
        #region Propiedades
        /// <summary>
        /// Etiqueta de XML para un texto
        /// </summary>
        public Text Texto { get; set; }

        /// <summary>
        /// Etiqueta de XML para un texto compuesto
        /// </summary>
        public Tuple<Text, Text> TextoCompuesto { get; set; }

        /// <summary>
        /// Etiqueta para XML en negritas.
        /// </summary>
        public Bold Negritas { get; set; }

        /// <summary>
        /// Etiqueta para XML en negritas base.
        /// </summary>
        public Bold NegritasBase { get => new() { Val = OnOffValue.FromBoolean(true) }; }

        /// <summary>
        /// Etiqueta para XML en un salto de siguiente.
        /// </summary>
        public Break SaltoLinea { get => new(); }

        /// <summary>
        /// Propiedad para validar si es un texto compuesto.
        /// </summary>
        public bool EsTextoCompuesto { get => TextoCompuesto != null; }
        #endregion

        #region Constructor
        /// <summary>
        /// Contructor vacio
        /// </summary>
        public ConvertirPropiedades() { }

        /// <summary>
        /// Contructor de ejecutar la convercion de parametros to XML
        /// </summary>
        /// <param name="modeloValor"></param>
        public ConvertirPropiedades(ModeloValor modeloValor)
        {
            Negritas = modeloValor.EsTituloResaltado ? new Bold { Val = OnOffValue.FromBoolean(true) } : null;

            Texto = modeloValor.EsTextoCompuesto ? null : new Text 
                                                              {
                                                                 Text = modeloValor.Descripcion,
                                                                 Space = SpaceProcessingModeValues.Preserve
                                                              };

            TextoCompuesto = modeloValor.EsTextoCompuesto ? new Tuple<Text, Text>(new Text 
                                                                                 {
                                                                                        Text = modeloValor.ClaveDescricion,
                                                                                        Space = SpaceProcessingModeValues.Preserve
                                                                                  }, new Text 
                                                                                  {
                                                                                        Text = modeloValor.Descripcion,
                                                                                        Space = SpaceProcessingModeValues.Preserve
                                                                                  }) : null;
            
        }
        #endregion
    }
}
