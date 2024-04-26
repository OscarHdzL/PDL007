using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using System.Collections.Generic;
using System.Linq;
using Utilidades.GestionCreacionDocumentos.POCOs.Propiedades;
using System;

namespace Utilidades.GestionCreacionDocumentos.Implementar.Decoradores
{
    /// <summary>
    /// Clase encargada de corrar el parrafo.
    /// </summary>
    public class DecoradorParrafo : BaseDecorador, IDisposable
    {
        #region Propiedades
        /// <summary>
        /// Texto del parrafo principal
        /// </summary>
        public Paragraph Parrafo { get => ObtenerParrafo(); }

        /// <summary>
        /// Collecion de datos
        /// </summary>
        private List<ConvertirPropiedades> DatosTexto { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor inicial
        /// </summary>
        /// <param name="propiedades"></param>
        public DecoradorParrafo(PropiedadesDocumentoTexto propiedades)
            : base(propiedades)
        {
            DatosTexto = new ();
            Ejecutar(propiedades);
        }
        #endregion

        #region Métodos privados
        /// <summary>
        /// Método de agregar decoradores de los textos
        /// </summary>
        /// <param name="propiedades"></param>
        private void Ejecutar(PropiedadesDocumentoTexto propiedades)
        {
            if (propiedades.Renglon != null)
                DatosTexto.Add(propiedades.Renglon);

            if (propiedades.Renglones.Any())
                DatosTexto.AddRange(propiedades.Renglones);

        }

        /// <summary>
        /// Método encargado de crear un parrafo.
        /// </summary>
        /// <returns></returns>
        private Paragraph ObtenerParrafo()
        {
            var miParrafo = new Paragraph();

            try
            {
                foreach (var item in DatosTexto)
                {
                    if (item.EsTextoCompuesto)
                        ProcesarRenglonCompuesto(item, ref miParrafo);
                    else
                        ProcesarRenglon(item, ref miParrafo);
                }
            }
            finally 
            {
                Dispose();
            }

            return miParrafo;
        }

        /// <summary>
        /// Método encargado de renderizar un reglon compuesto.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="miParrafoActual"></param>
        private void ProcesarRenglonCompuesto(ConvertirPropiedades item, ref Paragraph miParrafoActual)
        {
            miParrafoActual.Append(IniciarPropiedadesParrafo());
            miParrafoActual.Append(DecoradorUnicoRenglon(item.TextoCompuesto.Item1, item.NegritasBase));
            miParrafoActual.Append(DecoradorUnicoRenglon(item.TextoCompuesto.Item2, null));
            miParrafoActual.Append(DecoradorUnicoSalto());
            
        }

        /// <summary>
        /// Método encargado de renderizar un reglon .
        /// </summary>
        /// <param name="item"></param>
        /// <param name="miParrafoActual"></param>
        private void ProcesarRenglon(ConvertirPropiedades item, ref Paragraph miParrafoActual)
        {
            miParrafoActual.Append(IniciarPropiedadesParrafo());
            miParrafoActual.Append(DecoradorUnicoRenglon(item.Texto, item.Negritas != null ? item.NegritasBase : null));
            miParrafoActual.Append(item.SaltoLinea);
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
