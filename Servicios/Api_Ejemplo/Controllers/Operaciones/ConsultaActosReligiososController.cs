using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Modelos.Modelos.Request;
using Modelos.Response;
using Negocio.Catalogos;
using Negocio.Operaciones;
using System;
using System.Threading.Tasks;
using Utilidades.GestionCreacionDocumentos.POCOs.Documentos;
using System.IO;
using Utilidades.CifradoMd5;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Asuntos_Religiosos_Api.Controllers.Operaciones
{
    [Area("Catalogos")]
    [Route("TRAMITESDGAR/Operaciones/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Authorize]
    public class ConsultaActosReligiososController : Controller
    {
        #region Propiedades
        private IHostingEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly ConsultaActosReligiososNegocio _negocio;
        private readonly InsertarArchivoNegocio negocioFile;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public ConsultaActosReligiososController(IConfiguration configuration, IHostingEnvironment env)
        {
            _negocio = new ConsultaActosReligiososNegocio();
            negocioFile = new InsertarArchivoNegocio();
            _configuration = configuration;
            _env = env;
        }
        #endregion

        #region Métodos publicos
        [HttpGet("[action]")]
        public async Task<IActionResult> GetActosReligiosos([FromQuery] ConsultaActosReligiososRequest request)
        {
            try
            {
                var result = await _negocio.ConsultarActos(request);

                if (result.Status == ResponseStatus.Success)
                {
                    if (result.Response.Count > 0)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                log.LogError("ConsultaActosReligiososController - GetActosReligiosos", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetActosMediosTransmision([FromQuery] ConsultaActosReligiososRequest request)
        {
            try
            {
                var result = await _negocio.ConsultarActosMediosTransmision(request);

                if (result.Status == ResponseStatus.Success)
                {
                    if (result.Response.Count > 0)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                log.LogError("ConsultaActosReligiososController - GetActosMediosTransmision", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetActosFechas([FromQuery] ConsultaActosReligiososRequest request)
        {
            try
            {
                var result = await _negocio.ConsultarActosFechas(request);

                if (result.Status == ResponseStatus.Success)
                {
                    if (result.Response.Count > 0)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                log.LogError("ConsultaActosReligiososController - GetActosFechas", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }

        /// <summary>
        /// Método encargado de crear un documento .docx
        /// Para crear el oficio de transmiciones.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> ObtenerDocumento([FromQuery] ConsultaActosReligiososDocOficioRequest request)
        {

            try
            {
                var result = await _negocio.ObtenerDatosDocTransmicion(request);

                if (result.Status == ResponseStatus.Success)
                {
                    var miDocuemto = new DocTransmicion(result.Response);
                    var docGenerado = await miDocuemto.Documento;

                    /*ArchivoRequest infoFile = new ArchivoRequest();
                    infoFile.id = request.i_id_transmision;
                    infoFile.archivo = Convert.ToBase64String(docGenerado);
                    infoFile.idArchivoTramite = 29;
                    infoFile.archivo = await GuardarArchivo(Convert.ToBase64String(docGenerado), request.i_id_transmision, 29, "docx");
                    var resultado = await negocioFile.Operacion(infoFile);*/

                    return File(docGenerado, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", $"{Guid.NewGuid()}");
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                log.LogError("ConsultaActosReligiososController - GetActosFechas", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log" + ex.Message));
            }
        }
        
        private async Task<string> GuardarArchivo(string archivoBase64, long id, long idArchivoTramite, string ext)
        {
            var rootPath = _env.ContentRootPath;
            
            var path = Path.Combine(_configuration["pathHostDocuments:RutaArchivo"], $"{id}");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var fileName = Path.Combine(path, $"{idArchivoTramite}_{Guid.NewGuid()}.{ext}");
            await System.IO.File.WriteAllBytesAsync(fileName, Convert.FromBase64String(archivoBase64));

            CifradoMd5 cifradoMd5 = new CifradoMd5();
            string fileNameCifrado = cifradoMd5.cifrar(fileName);

            return fileNameCifrado;
        }

        #endregion
    }
}
