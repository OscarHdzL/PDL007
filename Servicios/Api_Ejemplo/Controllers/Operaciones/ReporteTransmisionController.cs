using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Modelos.Modelos.Request;
using Modelos.Response;
using Negocio.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asuntos_Religiosos_Api.Controllers.Operaciones
{
    [Area("Operaciones")]
    [Route("TRAMITESDGAR/Operaciones/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Authorize]
    public class ReporteTransmisionController : ControllerBase
    {
        #region Propiedades
        private readonly ReporteTransmisionNegocio _negocio;
        private readonly IConfiguration _configuration;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public ReporteTransmisionController(IConfiguration configuration)
        {
            _negocio = new ReporteTransmisionNegocio();
            _configuration = configuration;
        }
        #endregion

        #region Métodos publicos
        [HttpGet("[action]")]
        public async Task<IActionResult> ConsultarFiltrosEstatusTransmision([FromQuery] ReporteTransmisionListaEstatusRequest entidad)
        {
            try
            {

                var response = await _negocio.ConsultarEstatusTransmision(entidad);
                if (response.Status == ResponseStatus.Success)
                {
                    if (!string.IsNullOrEmpty(response.respuesta) || response.Response.Count > 0)
                    {
                        return Ok(response);
                    }
                    else
                    {
                        response.mensaje = "Sin datos";
                        return Ok(response);
                    }
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                log.LogError("ReporteTransmisionController - ConsultarFiltrosEstatusTransmision", ex);
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ConsultarReporteTransmision([FromBody] ReporteTransmisionRequest entidad)
        {
            try
            {

                var response = await _negocio.ConsultarReporteTransmision(entidad);
                if (response.Status == ResponseStatus.Success)
                {
                   return Ok(response);                   
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                log.LogError("ReporteTransmisionController - ConsultarReporteTransmision", ex);
                return BadRequest(ex.Message);
            }

        }
        #endregion

    }
}
