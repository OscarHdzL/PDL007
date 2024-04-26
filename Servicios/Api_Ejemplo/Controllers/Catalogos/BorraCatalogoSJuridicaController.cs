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

namespace Religiosos_Api.Controllers.Catalogos
{
    [Area("Catalogos")]
    [Route("TRAMITESDGAR/Catalogos/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Authorize]
    public class BorraCatalogoSJuridicaController : Controller
    {
        #region Propiedades
        private readonly BorraCatalogoSJuridicaNegocio _negocio;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public BorraCatalogoSJuridicaController()
        {
            _negocio = new BorraCatalogoSJuridicaNegocio();
        }
        #endregion

        #region Métodos publicos
        [HttpPost("[action]")]
        public async Task<IActionResult> Post([FromBody] BorraCatalagoSJuridicaRequest request)
        {
            try
            {
                var result = await _negocio.Operacion(request);
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
                log.LogError("BorraCatalogoSJuridicaController - Delete", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }
        #endregion
    }
}
