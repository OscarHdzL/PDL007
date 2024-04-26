
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Modelos.Modelos.Request;
using Modelos.Response;
using Negocio.Operaciones;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Asuntos_Religiosos_Api.Controllers.Operaciones
{
    [Area("Operaciones")]
    [Route("TRAMITESDGAR/Operaciones/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Authorize]
    public class GenerarOficioController : Controller
    {
        #region Propiedades

        private readonly GenerarOficioNegocio _Negocio;
        private readonly IConfiguration _configuration;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();

        #endregion

        #region Constructor

        public GenerarOficioController(IConfiguration configuration)
        {
            _Negocio = new GenerarOficioNegocio();
            _configuration = configuration;
        }

        #endregion

        #region Métodos Públicos


        [HttpPost("[action]")]
        public async Task<IActionResult> GenerarOficio([FromBody] ConsultaOficioTransmisionRequest request)
        {
            try
            {

                var resultado = await _Negocio.GenerarOficio(request, _configuration["OficioRuta"]);
                if (resultado.Status == ResponseStatus.Success)
                {
                    return Ok(resultado);
                }
                else
                {
                    resultado.mensaje = "Error al consultar la información.";
                    resultado.Status = ResponseStatus.Failed;
                    return BadRequest(resultado);
                }
            }
            catch (Exception ex)
            {
                log.LogError("GenerarOficioController - GenerarOficio", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ObtenerArchivo([FromQuery] string ruta)
        {
            try
            {
                byte[] fileArray = System.IO.File.ReadAllBytes(ruta);
                return File(fileArray, "application/pdf");

            }
            catch (Exception ex)
            {
                log.LogError("GenerarOficioController - ObtenerArchivo", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }
        }

        #endregion
    }
}
