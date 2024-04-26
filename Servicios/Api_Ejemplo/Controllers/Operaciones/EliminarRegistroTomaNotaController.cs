using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Modelos.Modelos.Request;
using Modelos.Response;
using Negocio.Operaciones;
using Religiosos_Api.Helper.Operaciones;
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
    public class EliminarRegistroTomaNotaController : Controller
    {
        #region Propiedades
        private readonly EliminarRegistroTomaNotaNegocio _negocio;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public EliminarRegistroTomaNotaController()
        {
            _negocio = new EliminarRegistroTomaNotaNegocio();
        }
        #endregion

        #region Métodos publicos

        [HttpPost("[action]")]
        public async Task<IActionResult> EliminarRegistro([FromBody] EliminarRegistroTomaNotaRequest request)
        {
            try
            {

                var response = await _negocio.EliminarRegistro(request);
                if (response.Status == ResponseStatus.Success)
                {

                    return Ok(response);


                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion
    }
}
