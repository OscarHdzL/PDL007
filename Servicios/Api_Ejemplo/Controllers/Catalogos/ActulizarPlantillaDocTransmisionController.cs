using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Modelos.Modelos.Request;
using Modelos.Response;
using Negocio.Catalogos;
using Negocio.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Asuntos_Religiosos_Api.Controllers.Catalogos
{
    [Area("Catalogos")]
    [Route("TRAMITESDGAR/Catalogos/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Authorize]
    public class ActulizarPlantillaDocTransmisionController : Controller
    {
        #region Propiedades
        private readonly ActulizarPlantillaDocTransmisionNegocio _negocio;
        private readonly IConfiguration _configuration;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public ActulizarPlantillaDocTransmisionController(IConfiguration configuration)
        {
            _configuration = configuration;
            _negocio = new ActulizarPlantillaDocTransmisionNegocio(configuration);
        }
        #endregion

        #region Métodos publicos
        [HttpPost("[action]")]
        public async Task<IActionResult> Post([FromBody] ActulizarPlantillaRequest request)
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
                log.LogError("ActulizarPlantillaDocTransmisionController - Post", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SeleccionaPlantilla([FromBody] ActulizarPlantillaRequest request)
        {
            try
            {
                var result = await _negocio.OperacionSeleccionaPlantilla(request);
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
                log.LogError("ActulizarPlantillaDocTransmisionController - SeleccionaPlantilla", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }
        #endregion
    }
}
