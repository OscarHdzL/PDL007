using DinkToPdf;
using DinkToPdf.Contracts;
using Modelos.Modelos.Utilidades;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades.GestorImagenes;
using Utilidades.ReportesPDF.Reporte.Interfaces;

namespace Utilidades.ReportesPDF.Reporte.Implementacion
{
    /// <summary>
    /// Clase encargada de cumplir los contrato, para la creacion del reportes
    /// </summary>
    public class ReporteOperaciones : IReporteOperaciones
    {
        #region Métodos Publicos
        /// <summary>
        /// Método encargado de la ejecutar el proceso de la creacion de un reporte.
        /// </summary>
        /// <param name="_convert"></param>
        /// <param name="htmlPlantilla"></param>
        /// <returns></returns>
        public ResponseGeneric<DataReport> ProcesoCrearPDF(IConverter _convert, string htmlContenido)
        {
            try
            {
                GestorArchivos.DirectoriosTempReporte();

                string archivoPDF = $"{Guid.NewGuid()}.pdf";

                var archivoPdf = _convert.Convert(new HtmlToPdfDocument
                {
                    GlobalSettings = ObtenerConfiguracionInicial(archivoPDF),
                    Objects = { ObtenerDocumentoHTML(htmlContenido) }
                });

                return new ResponseGeneric<DataReport>(new DataReport
                {
                    NombreDocumento = archivoPDF,
                    DocumentoPDF = archivoPdf
                });
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<DataReport>(ex);
            }
        }
        #endregion

        #region Métodos privados
        /// <summary>
        /// Método encargado de iniciar las caracteristicas que tendra el documento de pdf
        /// </summary>
        /// <returns></returns>
        private static GlobalSettings ObtenerConfiguracionInicial(string nombreArchivoPDF)
        {
            return new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.Letter,
                DPI = 300,
                Margins = new MarginSettings()
                {
                    Bottom = 10,
                    Left = 15,
                    Right = 15,
                    Top = 10
                },
                DocumentTitle = "Reporte-Basico",
                Out = Path.Combine(Directory.GetCurrentDirectory(), "AlmacenArchivos", "Reportes", "ReportesTemporales", nombreArchivoPDF)
            };
        }

        /// <summary>
        /// Método encargado de ejecutar el proceso de renderizado y dibujado de html con sus componentes
        /// </summary>
        /// <param name="htmlContenido">Plantilla de ya renderizada del html</param>
        /// <returns></returns>
        private static ObjectSettings ObtenerDocumentoHTML(string htmlContenido)
        {
            return new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlContenido,
                WebSettings =
                {
                    DefaultEncoding = "utf-8",
                    UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "ReportesPDF", "Recursos", "bootstrap.min.css"),
                    EnableJavascript = true
                },
            };
        }
        #endregion

    }
}
