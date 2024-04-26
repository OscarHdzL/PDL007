using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
    public class ConsultaDetalleTomaNotaController : Controller
    {
        #region Propiedades
        private readonly ConsultaDetalleTomaNotaNegocio _negocio;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public ConsultaDetalleTomaNotaController()
        {
            _negocio = new ConsultaDetalleTomaNotaNegocio();
        }
        #endregion

        #region Métodos publicos
        [HttpGet("[action]")]
        public async Task<IActionResult> Get([FromQuery] ConsultaDetalleTomaNotaRequest request)
        {
            try
            {
                //var us_session = AutenticacionHelper.UsuarioActualEnSesion(HttpContext);
                //request.i_id_c = us_session.IdUsuario;
                //if (request.s_id_us is null && (bool)request.dictaminador)
                //    return NoContent();
                //if ((bool)request.dictaminador)
                //    request.s_id_us = null;
                var result = await _negocio.Consultar(request);

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
                log.LogError("ConsultaDetalleTomaNotaController - Get", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Post([FromBody] RequestParamMovimientos request)
        {
            try
            {
                var result = await _negocio.OperacionMovimientos(request);

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
                log.LogError("ConsultaDetalleTomaNotaController - Get", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }
        #endregion
    }
}
