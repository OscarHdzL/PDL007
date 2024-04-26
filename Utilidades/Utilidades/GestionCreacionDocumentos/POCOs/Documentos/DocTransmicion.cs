using DocumentFormat.OpenXml.Wordprocessing;
using Modelos.Modelos.Response;
using System;
using System.Linq;
using System.Threading.Tasks;
using Utilidades.GestionCreacionDocumentos.Implementar.FabricaDocumento;
using Utilidades.GestionCreacionDocumentos.POCOs.Modelos;
using Utilidades.GestionCreacionDocumentos.POCOs.Propiedades;

namespace Utilidades.GestionCreacionDocumentos.POCOs.Documentos
{
    /// <summary>
    /// Clase encargada de obtener la configuracion del documentos de medios de transmicion
    /// </summary>
    public class DocTransmicion : IDisposable
    {
        #region Propiedades
        /// <summary>
        /// Documento virtual creado
        /// </summary>
        public ValueTask<byte[]> Documento { get => DocumentoActual.GenerarDocumento(); }

        /// <summary>
        /// Propiedad del documentos de medios de transmicion
        /// </summary>
        private DocumentoOficio DocumentoActual { get; set; }

        /// <summary>
        /// Propiedad de los datos del oficio
        /// </summary>
        private ModeloOficio Oficio { get; set; }

        /// <summary>
        /// Propiedad de los datos del detalle oficio
        /// </summary>
        private ModeloDetalleTransmicion DetalleTransmicion { get; set; }

        /// <summary>
        /// Propiedad de contindos response
        /// </summary>
        private ContenidoConsultaActosReligiososResponse ContenidoResponse { get; set; }

        #endregion

        #region Propiedades privadas
        /// <summary>
        /// Método encargado de obtener el orden de las etiquetas (XML) 
        /// se van air agregando al documento word
        /// </summary>
        /// <param name="orden">indice del elemento</param>
        /// <returns></returns>
        private int ElementoSiguiente(ref int orden)
            => ++orden;
        #endregion

        #region Constructo
        /// <summary>
        /// Constructor de iniciar el docmuento
        /// </summary>
        /// <param name="response">objetos de transporte de la información que viene de la Base de datos.</param>
        public DocTransmicion(ContenidoConsultaActosReligiososResponse response)
        {
            ContenidoResponse = response;
            DocumentoActual = new DocumentoOficio(response.RutaDocumento);
            Oficio = new ModeloOficio(response.ConsultaOficioTransmisions);
            DetalleTransmicion = new ModeloDetalleTransmicion(response.ConsultaDetalleTramiteTransmisions);
            EjecutarCreacionXML();
        }

        #endregion

        #region Eventos
        /// <summary>
        /// Método encargado de ejecutar la creacion del documento  XML.
        /// </summary>
        private void EjecutarCreacionXML()
        {
            try
            {
                int ordenElemenos = 1;

                // Se agrega el encabezado del oficio.
                DocumentoActual.AgregarParrafo(OperacionesPropiedades.ObtenerConfiguracionParrafo(JustificationValues.Right, ModeloDescripcion.DatosEncabezadoDerecho), ordenElemenos);

                // Se agregar el parrafo de los datos del oficio.
                DocumentoActual.AgregarParrafo(OperacionesPropiedades.ObtenerConfiguracionParrafo(JustificationValues.Right, Oficio.DatosOficio), ElementoSiguiente(ref ordenElemenos));

                // Se agregar el parrafo de los detalles del oficio y la fecha.
                DocumentoActual.AgregarParrafo(OperacionesPropiedades.ObtenerConfiguracionRenglon(JustificationValues.Right, DetalleTransmicion.Fecha), ElementoSiguiente(ref ordenElemenos));
                DocumentoActual.AgregarParrafo(OperacionesPropiedades.ObtenerConfiguracionParrafo(JustificationValues.Left,  DetalleTransmicion.DatosDetalle), ElementoSiguiente(ref ordenElemenos));

                // Se agrega parrafo de los datos referidos el documento.
                DocumentoActual.AgregarParrafo(OperacionesPropiedades.ObtenerConfiguracionRenglon(JustificationValues.Left, ModeloDescripcion.DatosRefiero), ElementoSiguiente(ref ordenElemenos));

                //Ejecutar Procesar los datos de los actos
                if (ContenidoResponse.ConsultaActosReligiosos.Any())
                    ProcesarDatosActos(ref ordenElemenos);

                // Se agrega al parrafos saltos de linea
                DocumentoActual.AgregarParrafo(OperacionesPropiedades.ObtenerConfiguracionRenglon(JustificationValues.Center, ModeloDescripcion.Salto), ElementoSiguiente(ref ordenElemenos));

                // Se agrega parrafo de referido el documento AsociacionReligiosa
                DocumentoActual.AgregarParrafo(OperacionesPropiedades.ObtenerConfiguracionRenglon(JustificationValues.Left, ModeloDescripcion.DatosDescripcionAsociacionReligiosa), ElementoSiguiente(ref ordenElemenos));

                // Se agrega parrafo de referido el documento Articulos
                DocumentoActual.AgregarParrafo(OperacionesPropiedades.ObtenerConfiguracionRenglon(JustificationValues.Left, ModeloDescripcion.DatosDescripcionArticulos), ElementoSiguiente(ref ordenElemenos));

                // Se agrega parrafo de referido el documento Fraccion
                DocumentoActual.AgregarParrafo(OperacionesPropiedades.ObtenerConfiguracionRenglon(JustificationValues.Left, ModeloDescripcion.DatosDescripcionFraccion), ElementoSiguiente(ref ordenElemenos));

                // Se agrega parrafo de referido el documento Consideraciones
                DocumentoActual.AgregarParrafo(OperacionesPropiedades.ObtenerConfiguracionRenglon(JustificationValues.Left, ModeloDescripcion.DatosDescripcionConsidereaciones), ElementoSiguiente(ref ordenElemenos));

                // Se agrega parrafo de referido el documento Atentamente
                DocumentoActual.AgregarParrafo(OperacionesPropiedades.ObtenerConfiguracionRenglon(JustificationValues.Center, ModeloDescripcion.DatosDescripcionAtentamente), ElementoSiguiente(ref ordenElemenos));
                DocumentoActual.AgregarParrafo(OperacionesPropiedades.ObtenerConfiguracionRenglon(JustificationValues.Center, ModeloDescripcion.AgregarPuesto(ContenidoResponse.ConsultaOficioTransmisions.puesto_firmante)), ElementoSiguiente(ref ordenElemenos));

                // Se agrega al parrafos saltos de linea
                DocumentoActual.AgregarParrafo(OperacionesPropiedades.ObtenerConfiguracionRenglon(JustificationValues.Center, ModeloDescripcion.Salto), ElementoSiguiente(ref ordenElemenos));

                // Se agrega al parrafos saltos de linea
                DocumentoActual.AgregarParrafo(OperacionesPropiedades.ObtenerConfiguracionRenglon(JustificationValues.Center,
                                                                                                  ModeloDescripcion.AgregarPuesto(ContenidoResponse.ConsultaOficioTransmisions.nombre_firmante)), ElementoSiguiente(ref ordenElemenos));

                // Se agrega al parrafos saltos de Copia copia para
                DocumentoActual.AgregarParrafo(OperacionesPropiedades.ObtenerConfiguracionRenglon(JustificationValues.Center,
                ModeloDescripcion.AgregarCopiaCopiaPara($"{ContenidoResponse.ConsultaOficioTransmisions.titulo_ccp} {ContenidoResponse.ConsultaOficioTransmisions.nombre_ccp}" )),
                ElementoSiguiente(ref ordenElemenos));
            }
            finally 
            {
                Dispose();
            }
        }

        /// <summary>
        /// Método encargado de procesar los datos de los actos
        /// Fechas || Frecuencias
        /// Creacion de tablas en XML
        /// </summary>
        private void ProcesarDatosActos(ref int ordenElemenos) 
        {
            try
            {
                // Obtiene lista de actos religiosos
                var lstActos = ContenidoResponse.ConsultaActosReligiosos.GroupBy(g => g.i_id_acto)
                                                                        .Select(s => new { i_id_acto = s.Key, Contenido = s.ToList() })
                                                                        .ToList();

                foreach (var item in lstActos)
                {

                    DocumentoActual.AgregarParrafo(OperacionesPropiedades.ObtenerConfiguracionParrafo(JustificationValues.Left, new ModeloActosReligiosos(item.Contenido).DatosReligioso), ElementoSiguiente(ref ordenElemenos));
                    
                    // Se obtiene el modelo de la lista de frecuencia y fechas.
                    var tablaFechaHora = new ModeloFechasFrecuencia(ContenidoResponse.ConsultaActosFechas.Where(w => w.i_id_acto == item.i_id_acto).ToList(),
                                                                    ContenidoResponse.ConsultaActosFrecuencia.Where(w => w.i_id_acto == item.i_id_acto).ToList());

                    if (tablaFechaHora.DatosFechas.Any())
                    {
                        DocumentoActual.AgregarParrafo(OperacionesPropiedades.ObtenerConfiguracionRenglon(JustificationValues.Left, tablaFechaHora.Titulo), ElementoSiguiente(ref ordenElemenos));
                        DocumentoActual.AgregarParrafo(OperacionesPropiedades.ObtenerConfiguracionParrafo(JustificationValues.Left, tablaFechaHora.DatosFechas), ElementoSiguiente(ref ordenElemenos));
                    }

                    if (tablaFechaHora.DatosFrecuencias != null && tablaFechaHora.DatosFrecuencias.DatosContenido.Any())
                        DocumentoActual.AgregarTabla(OperacionesPropiedades.ObtenerConfiguracionTabla(JustificationValues.Left, tablaFechaHora.DatosFrecuencias), ElementoSiguiente(ref ordenElemenos));

                    // Se obtiene el modelo de tabla de modelos de transmision
                    var tablaTransmicion = new ModeloTabla(ContenidoResponse.ConsultaActosMediosTrasmision.Where(w => w.i_id_acto == item.i_id_acto).ToList());

                    if (tablaTransmicion.DatosContenido.Any()) 
                    {
                        DocumentoActual.AgregarParrafo(OperacionesPropiedades.ObtenerConfiguracionRenglon(JustificationValues.Left, 
                                                                                                          new ("Medios de Transmisión", esTitulo: true)), ElementoSiguiente(ref ordenElemenos));
                        DocumentoActual.AgregarTabla(OperacionesPropiedades.ObtenerConfiguracionTabla(JustificationValues.Left, tablaTransmicion), ElementoSiguiente(ref ordenElemenos));
                    }
                   
                }
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
