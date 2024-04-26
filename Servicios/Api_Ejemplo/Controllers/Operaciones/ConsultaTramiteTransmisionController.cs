using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos.Modelos.Request;
using Modelos.Response;
using Negocio.Catalogos;
using Negocio.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Modelos.Interfaz;
using Modelos.Modelos.Response;

namespace Asuntos_Religiosos_Api.Controllers.Operaciones
{
    [Area("Operaciones")]
    [Route("TRAMITESDGAR/Operaciones/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Authorize]
    public class ConsultaTramiteTransmisionController : Controller
    {
        #region Propiedades
        private readonly ConsultaTramiteTransmisionNegocio _negocio;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public ConsultaTramiteTransmisionController()
        {
            _negocio = new ConsultaTramiteTransmisionNegocio();
        }
        #endregion

        #region Métodos publicos
        [HttpGet("[action]")]
        public async Task<IActionResult> Get([FromQuery] ConsultaTramiteTransmisionRequest request)
        {
            try
            {
                var result = await _negocio.Consulta(request);

                if (result.Status == ResponseStatus.Success) {

                    if (result.Response.Count > 0) {
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
                log.LogError("ConsultaTramiteTransmisionController - Get", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }

        #endregion
    }
}
