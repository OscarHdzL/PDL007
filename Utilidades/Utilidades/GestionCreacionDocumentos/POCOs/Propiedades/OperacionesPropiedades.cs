using System.Collections.Generic;
using System.Linq;
using Utilidades.GestionCreacionDocumentos.Implementar.Decoradores;
using DocumentFormat.OpenXml.Wordprocessing;
using Utilidades.GestionCreacionDocumentos.POCOs.Modelos;

namespace Utilidades.GestionCreacionDocumentos.POCOs.Propiedades
{
    /// <summary>
    /// Clase encargada de convertir los datos alas difenrentes propiedades
    /// [ PropiedadesDocumentoTabla || PropiedadesDocumentoTexto ]
    /// </summary>
    public static class OperacionesPropiedades
    {
        /// <summary>
        /// Método para obtener las configuraciones de un renglon
        /// </summary>
        /// <param name="justification">Alinacion que tendra ese renglon</param>
        /// <param name="modelo">Datos de transporte para renderizacion XML</param>
        /// <returns></returns>
        public static PropiedadesDocumentoTexto ObtenerConfiguracionRenglon(JustificationValues justification, ModeloValor modelo)
        {
            return new PropiedadesDocumentoTexto
            {
                TipoJustificacion = justification,
                Renglon = new ConvertirPropiedades(modelo)
            };
        }

        /// <summary>
        /// Métodos para obtener las configuraciones de un parrafo
        /// </summary>
        /// <param name="justification">Alinacion que tendra ese renglon</param>
        /// <param name="lstModelos">Datos de transporte para renderizacion XML</param>
        /// <returns></returns>
        public static PropiedadesDocumentoTexto ObtenerConfiguracionParrafo(JustificationValues justification, List<ModeloValor> lstModelos)
        {
            return new PropiedadesDocumentoTexto
            {
                TipoJustificacion = justification,
                Renglones = lstModelos.Select(s => new ConvertirPropiedades(s)).ToList()
            };
        }

        /// <summary>
        /// Métodos para obtener los datos para creacion de una tabla
        /// </summary>
        /// <param name="justification">Alinacion que tendra ese renglon</param>
        /// <param name="tabla">Datos de transporte para renderizacion XML</param>
        /// <returns></returns>
        public static PropiedadesDocumentoTabla ObtenerConfiguracionTabla(JustificationValues justification, ModeloTabla tabla)
        {
            return new PropiedadesDocumentoTabla
            {
                TipoJustificacion = justification,
                CabezerasTabla = tabla.TitulosCabezera.Select(s => new ConvertirPropiedades(s)).ToList(),
                ContenidoTablas = tabla.DatosContenido.Select(s => s.Select(elemento => new ConvertirPropiedades(elemento)).ToList()).ToList(),
            };
        }

    }
}
