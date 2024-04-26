using DinkToPdf.Contracts;
using Modelos.Modelos.Utilidades;
using Modelos.Response;

namespace Utilidades.ReportesPDF.Reporte.Interfaces
{
    /// <summary>
    /// Conntrato de reporte
    /// </summary>
    public interface IReporteOperaciones
    {
        /// <summary>
        /// Método encargado de la ejecutar el proceso de la creacion de un reporte.
        /// </summary>
        /// <param name="_convert">Convertidor para PDF TO HTML</param>
        /// <param name="htmlPlantilla">Plantilla de ya renderizada del html</param>
        /// <returns></returns>
        ResponseGeneric<DataReport> ProcesoCrearPDF(IConverter _convert, string htmlPlantilla);

    }
}
