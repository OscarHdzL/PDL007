using Religiosos_Api.Helper.Operaciones;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Modelos.Modelos.Request;
using Modelos.Response;
using Negocio.Operaciones;
using System;
using System.Threading.Tasks;
using Utilidades.GestionCreacionDocumentos.Implementar;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;

namespace Religiosos_Api.Controllers.Operaciones
{
    [Area("Operaciones")]
    [Route("TRAMITESDGAR/Operaciones/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    public class InicioSesionController : Controller
    {
        #region Propiedades
        private readonly InicioSesionNegocio _negocio;
        private readonly IConfiguration _configuration;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public InicioSesionController(IConfiguration configuration)
        {
            _negocio = new InicioSesionNegocio();
            _configuration = configuration;
        }
        #endregion

        #region Métodos publicos
        [HttpPost("[action]")]
        public async Task<IActionResult> Post([FromBody] InicioSesionRequest request)
        {
            try
            {
               
                var resultado = await _negocio.Consultar(request);
                if (resultado.Status == ResponseStatus.Success)
                {
                    return Ok(AutenticacionHelper.ProcesarRespuestaSesion(resultado.Response, _configuration));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                log.LogError("InicioSesionController - Post", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }
        #endregion


    }
}
