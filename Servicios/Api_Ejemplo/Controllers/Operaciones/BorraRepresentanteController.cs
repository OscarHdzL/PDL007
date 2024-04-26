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

namespace Api.Controllers.Operaciones
{
    [Area("Operaciones")]
    [Route("TRAMITESDGAR/Operaciones/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Authorize]
    public class BorraRepresentanteController : Controller
    {
        #region Propiedades
        private readonly BorraRepresentanteNegocio _negocio;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public BorraRepresentanteController()
        {
            _negocio = new BorraRepresentanteNegocio();
        }
        #endregion

        #region Métodos publicos
        [HttpGet("[action]")]
        public async Task<IActionResult> Get([FromQuery] BorraRepresentanteRequest request)
        {
            try
            {
                var result = await _negocio.Consultar(request);
                if (result.Status == ResponseStatus.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                log.LogError("BorrarRepresentateControler - Get", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }
        #endregion
    }
}
