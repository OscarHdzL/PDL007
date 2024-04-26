using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Modelos.Modelos.Request;
using Modelos.Response;
using Negocio.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers.Operaciones
{
    [Area("Operaciones")]
    [Route("TRAMITESDGAR/Operaciones/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Authorize]
    public class ReporteController : Controller
    {
        #region Propiedades
        private readonly ReporteNegocio _negocio;
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public ReporteController(IConfiguration configuration)
        {
            _negocio = new ReporteNegocio();
            _configuration = configuration;
        }
        #endregion

        #region Métodos publicos
        [HttpPost("[action]")]
        public async Task<IActionResult> ConsultarReporte([FromBody] ReporteRequest entidad)
        {
            try
            {

                var response = await _negocio.Consultar(entidad);
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

                return BadRequest(ex.Message);
            }

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ConsultarReporteTnota([FromBody] ReporteRequest entidad)
        {
            try
            {

                var response = await _negocio.ConsultarTnota(entidad);
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

                return BadRequest(ex.Message);
            }

        }
        #endregion
    }
}
