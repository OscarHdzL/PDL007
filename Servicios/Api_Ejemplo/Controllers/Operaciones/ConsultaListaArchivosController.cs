using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
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
    public class ConsultaListaArchivosController : Controller
    {

        #region Propiedades
        readonly ConsultaListaArchivosNegocio _negocio;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor inicial del controllador
        /// </summary>
        public ConsultaListaArchivosController()
        {
            _negocio = new ConsultaListaArchivosNegocio();
        }
        #endregion

        #region Eventos de controllador
        [HttpGet("[action]")]
        public async Task<IActionResult> Get([FromQuery] ArchivosRequest request)
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
            catch (Exception)
            {
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }
        #endregion


    }
}
