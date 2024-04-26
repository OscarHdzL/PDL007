using Acceso_Datos.Operaciones;
using Modelos.Modelos.Request;
using Modelos.Modelos.Response;
using Modelos.Response;
using Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilidades.GestionCreacionDocumentos.Implementar;
using Utilidades.FileManager;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.IO;
using Microsoft.Office.Interop.Word;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Negocio.Operaciones
{
    public class GenerarOficioNegocio : BaseNegocio
    {
        #region Propiedades

        private readonly ConsultaOficioTransmisionAccesoDatos _oficioDatos;
        private readonly ConsultaDetalleTramiteTransmisionAccesoDatos _detalleDatos;
        private readonly ConsultaActosReligiososAccesoDatos _actosDatos;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor inicial
        /// </summary>
        public GenerarOficioNegocio() : base()
        {
            _oficioDatos = new ConsultaOficioTransmisionAccesoDatos();
            _detalleDatos = new ConsultaDetalleTramiteTransmisionAccesoDatos();
            _actosDatos = new ConsultaActosReligiososAccesoDatos();
        }

        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Método encargado de ejecutar el llamado de Acceso Datos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<GenerarOficioRespose>>> GenerarOficio(ConsultaOficioTransmisionRequest request, string ruta)
        {
            try
            {
                var informacionOficio = await _oficioDatos.Consultar(request);

                List<GenerarOficioRespose> generarOficio = new List<GenerarOficioRespose>();
                GenerarOficioRespose oficio = new GenerarOficioRespose();

                if (informacionOficio.Status == ResponseStatus.Success && informacionOficio.Response.Any())
                {
                    DateTimeFormatInfo dtfi = CultureInfo.GetCultureInfo("en-ES").DateTimeFormat;
                    DateTime fechaActual = DateTime.Today;

                    ConsultaTramiteTransmisionRequest modeloDetalle = new ConsultaTramiteTransmisionRequest();
                    modeloDetalle.s_id_us = 0;
                    modeloDetalle.i_id_transmision = request.i_id_transmision;
                    var detalle = await _detalleDatos.Consultar(modeloDetalle);

                    ConsultaActosReligiososRequest modeloActo = new ConsultaActosReligiososRequest();
                    modeloActo.i_id_transmision = request.i_id_transmision;
                    var actos = await _actosDatos.ConsultarActos(modeloActo);


                    object oMissing = System.Reflection.Missing.Value;
                    Word._Application oWord;
                    Word._Document oDoc;
                    oWord = new Word.Application();
                    //oWord.Visible = true;

                    var currentDir = Directory.GetCurrentDirectory();
                    object oTemplate = $"{ruta}//Template.docx";
                    oDoc = oWord.Documents.Add(ref oTemplate, ref oMissing, ref oMissing, ref oMissing);

                    oDoc.Bookmarks["num_referencia"].Range.Text = informacionOficio.Response[0].referencia;
                    oDoc.Bookmarks["num_expediente"].Range.Text = informacionOficio.Response[0].expediente;
                    oDoc.Bookmarks["num_oficio"].Range.Text = informacionOficio.Response[0].oficio;
                    oDoc.Bookmarks["dia"].Range.Text = fechaActual.Day.ToString();
                    oDoc.Bookmarks["mes"].Range.Text = dtfi.GetMonthName(3); //CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(fechaActual.Month); //("MMMM");
                    oDoc.Bookmarks["anio"].Range.Text = fechaActual.Year.ToString();
                    oDoc.Bookmarks["nombre_representante"].Range.Text = detalle.Response[0].rep_nombre_completo;
                    oDoc.Bookmarks["denominacion_religiosa"].Range.Text = detalle.Response[0].denominacion;
                    oDoc.Bookmarks["num_sgar"].Range.Text = detalle.Response[0].numero_sgar;
                    oDoc.Bookmarks["direccion"].Range.Text = detalle.Response[0].domicilio;
                    oDoc.Bookmarks["puesto_firmante"].Range.Text = informacionOficio.Response[0].puesto_firmante;
                    oDoc.Bookmarks["tit_firmante"].Range.Text = informacionOficio.Response[0].titulo_firmante;
                    oDoc.Bookmarks["nombre_firmante"].Range.Text = informacionOficio.Response[0].nombre_firmante;
                    oDoc.Bookmarks["tit_ccp"].Range.Text = informacionOficio.Response[0].titulo_ccp;
                    oDoc.Bookmarks["nombre_ccp"].Range.Text = informacionOficio.Response[0].nombre_ccp;

                    object objEndOfDocFlag = "\\endofdoc";

                    for (int i = 0; i < actos.Response.Count; i++) {

                        ConsultaActosReligiososRequest consultarEmisora = new ConsultaActosReligiososRequest();
                        consultarEmisora.i_id_acto_religioso = actos.Response[i].i_id_acto;

                        var emisoras = await _actosDatos.ConsultarActosMediosTransmision(consultarEmisora);
                        int tE = emisoras.Response.Count;

                        var fechas = await _actosDatos.ConsultarActosFechas(consultarEmisora);

                        /*string bookmarkName = "actos_religiosos";

                        if (oDoc.Bookmarks.Exists(bookmarkName)) {
                            Object name = bookmarkName;
                            Word.Table tabEmisoras;
                            Word.Range range = oDoc.Bookmarks.get_Item("actos_religiosos").Range;

                             tabEmisoras = oDoc.Tables.Add(range, tE, 5, ref oMissing, ref oMissing);
                            tabEmisoras.Range.ParagraphFormat.SpaceAfter = 6;
                            int iRow, iCol;

                            for (iRow = 1; iRow <= 1; iRow++)
                            {
                                tabEmisoras.Cell(iRow, 1).Range.Text = "#";
                                tabEmisoras.Cell(iRow, 2).Range.Text = "Frecuencicia/Canal";
                                tabEmisoras.Cell(iRow, 3).Range.Text = "Proveedor de Servicio";
                                tabEmisoras.Cell(iRow, 4).Range.Text = "Televisora/Radiodifusora";
                                tabEmisoras.Cell(iRow, 5).Range.Text = "Lugar de Transmisión";
                            }

                             object newRange = range;

                             oWord.Selection.TypeParagraph();

                             oDoc.Bookmarks.Add("actos_religiosos", range);
                         }*/


                        for (int j = 0; j < emisoras.Response.Count; j++)
                        {
                            Word.Paragraph seccionDetalleEmisoras;
                            object _oRng5 = oDoc.Bookmarks.get_Item("actos_religiosos").Range;
                            seccionDetalleEmisoras = oDoc.Content.Paragraphs.Add(ref _oRng5);
                            seccionDetalleEmisoras.Range.Text = emisoras.Response[j].frecuencia_canal + "\n";
                            seccionDetalleEmisoras.Range.InsertParagraphAfter();
                        }

                        Word.Paragraph seccionEmisoras;
                        object _oRng4 = oDoc.Bookmarks.get_Item("actos_religiosos").Range;
                        seccionEmisoras = oDoc.Content.Paragraphs.Add(ref _oRng4);
                        seccionEmisoras.Range.Text = "\nMedios de Transmisión \n";
                        seccionEmisoras.Range.Font.Bold = 2;
                        seccionEmisoras.Range.InsertParagraphAfter();

                        for (int x = 0; x < fechas.Response.Count; x++)
                        {
                            Word.Paragraph seccionDetalleFechas;
                            object _oRng3 = oDoc.Bookmarks.get_Item("actos_religiosos").Range;
                            seccionDetalleFechas = oDoc.Content.Paragraphs.Add(ref _oRng3);

                            if (fechas.Response[x].i_id_cat_periodo != null)
                            {
                                /*seccionDetalleFechas.Range.Text = fechas.Response[x].c_periodo + " " +
                                    fechas.Response[x].cat_dia + " " +
                                    fechas.Response[x].cat_mes + " " +
                                    fechas.Response[x].cat_anio + " " +
                                    fechas.Response[x].c_hora_inicio + " - " + fechas.Response[x].c_hora_fin;*/
                                seccionDetalleFechas.Range.Text = fechas.Response[x].c_periodo + " " +
                                   fechas.Response[x].c_hora_inicio + " - " + fechas.Response[x].c_hora_fin;
                            }
                            else
                            {
                                seccionDetalleFechas.Range.Text = fechas.Response[x].c_fecha_inicio + " - " + fechas.Response[x].c_fecha_fin
                                                          + " " + fechas.Response[x].c_hora_inicio + " - " + fechas.Response[x].c_hora_fin;

                            }

                            seccionDetalleFechas.Range.InsertParagraphAfter();

                        }

                        Word.Paragraph seccionFechas;
                        object _oRng2 = oDoc.Bookmarks.get_Item("actos_religiosos").Range;
                        seccionFechas = oDoc.Content.Paragraphs.Add(ref _oRng2);
                        seccionFechas.Range.Text = "\nFechas y Horarios \n";
                        seccionFechas.Range.Font.Bold = 2;
                        seccionFechas.Range.InsertParagraphAfter();

                        Word.Paragraph seccionActos;
                        object _oRng = oDoc.Bookmarks.get_Item("actos_religiosos").Range;
                        seccionActos = oDoc.Content.Paragraphs.Add(ref _oRng);
                        seccionActos.Range.Text = "\nActo Religioso  \n" + actos.Response[i].c_nombre + " \n";
                        seccionActos.Range.Font.Bold = 2;
                        seccionActos.Range.InsertParagraphBefore();

                    }

                    var uid = Guid.NewGuid();
                    //string nombreLibro = $"/OficioGenerado_{uid}.docx";
                    //var newFilePath = $"{ruta}{nombreLibro}";
                    //oDoc.SaveAs2(newFilePath);
                    //oWord.Documents.Open(newFilePath);

                    var pathDocumento = ruta + "\\" + "Oficio_" + uid + ".pdf";
                    oDoc.ExportAsFixedFormat(pathDocumento, WdExportFormat.wdExportFormatPDF);

                    object doNotSaveChanges = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
                    oDoc.Close(ref doNotSaveChanges, ref oMissing, ref oMissing);

                    //File.WriteAllBytes(@"c:\yourfile", Convert.FromBase64String(yourBase64String));

                    //informacionOficio.Response[0].oficio = $"{ruta}{nombreLibro}";
                    oficio.path_archivo = pathDocumento;
                    generarOficio.Add(oficio);
                }
                else
                {
                    generarOficio = null;
                }

                return new ResponseGeneric<List<GenerarOficioRespose>>(generarOficio);

            }
            catch (Exception ex)
            {
                LogErrores("Negocio GenerarLibroAsuntoNegocio-Operacion", ex);
                throw;
            }
        }

        #endregion

        #region Métodos privados crecion del docuemnto

        #endregion
    }
}
