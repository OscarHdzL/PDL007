using Microsoft.AspNetCore.Authorization;
using Religiosos_Api.Helper.Operaciones;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Modelos.Modelos.Request;
using Modelos.Response;
using Negocio.Operaciones;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Utilidades.CifradoMd5;

namespace Api.Controllers.Operaciones
{
    [Area("Operaciones")]
    [Route("TRAMITESDGAR/Operaciones/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Authorize]
    public class InsertarArchivoController : Controller
    {
        #region Propiedades

        private IHostingEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly InsertarArchivoNegocio _negocio;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public InsertarArchivoController(IConfiguration configuration, IHostingEnvironment env)
        {
            _negocio = new InsertarArchivoNegocio();
            _configuration = configuration;
            _env = env;
        }
        #endregion

        #region Métodos publicos
        [HttpPost("[action]")]
        public async Task<IActionResult> Post([FromBody] ArchivoRequest request)
        {
            try
            {
                string[] archivoCodificado = request.archivo.Split(',');
                string extension = (archivoCodificado[0].Length > 35) ? "docx" : archivoCodificado[0].Split('/')[1].Split(';')[0];
                string archivoBase64 = archivoCodificado[1];
                request.archivo = await GuardarArchivo(archivoBase64, request.id, request.idArchivoTramite, extension);

                var resultado = await _negocio.Operacion(request);
                return Ok(resultado.Response);
            }
            catch (Exception ex)
            {
                log.LogError("InsertarArchivoController - Post", ex);
                return BadRequest(ex.Message);
            }

        }
        #endregion

        #region Métodos Privados

        private async Task<string> GuardarArchivo(string archivoBase64, long id, long idArchivoTramite, string ext)
        {
            var rootPath = _env.ContentRootPath;

            //var path = Path.Combine("/home/tramitesdgar/tramites", $"{id}");      //QA
            //var path = Path.Combine("/var/www/html/tramitesdgar/tramites", $"{id}");      //DEV
            var path = Path.Combine(_configuration["pathHostDocuments:RutaArchivo"], $"{id}");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //path = Path.Combine(path, $"{id}");
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}

            var fileName = Path.Combine(path, $"{idArchivoTramite}_{Guid.NewGuid()}.{ext}");
            await System.IO.File.WriteAllBytesAsync(fileName, Convert.FromBase64String(archivoBase64));

            CifradoMd5 cifradoMd5 = new CifradoMd5();
            string fileNameCifrado = cifradoMd5.cifrar(fileName);

            return fileNameCifrado;
        }

        #endregion
    }
    //    private async Task<string> GuardarArchivo(string archivoBase64, long id, long idArchivoTramite)
    //    {
    //        var rootPath = _env.ContentRootPath;

    //        var path = Path.Combine(rootPath, "tramites");
    //        if (!Directory.Exists(path))
    //        {
    //            Directory.CreateDirectory(path);
    //        }

    //        path = Path.Combine(path, $"{id}");
    //        if (!Directory.Exists(path))
    //        {
    //            Directory.CreateDirectory(path);
    //        }

    //        var fileName = Path.Combine(path, $"{idArchivoTramite}_{Guid.NewGuid()}.pdf");
    //        await System.IO.File.WriteAllBytesAsync(fileName, Convert.FromBase64String(archivoBase64));

    //        CifradoMd5 cifradoMd5 = new CifradoMd5();
    //        string fileNameCifrado = cifradoMd5.cifrar(fileName);

    //        return fileNameCifrado;
    //    }

    //    #endregion
    //}
}
