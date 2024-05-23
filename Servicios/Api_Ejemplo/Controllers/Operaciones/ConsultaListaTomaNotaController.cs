using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Modelos.Modelos.Request;
using Modelos.Response;
using Negocio.Operaciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Asuntos_Religiosos_Api.Controllers.Operaciones
{
    [Area("Operaciones")]
    [Route("TRAMITESDGAR/Operaciones/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Authorize]
    public class ConsultaListaTomaNotaController : Controller
    {
        #region Propiedades
        private readonly ConsultaListaTomaNotaNegocio _negocio;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public ConsultaListaTomaNotaController()
        {
            _negocio = new ConsultaListaTomaNotaNegocio();
        }
        #endregion

        #region Métodos publicos

        [HttpPost("ListaTomaNota")]
        public async Task<IActionResult> ListaTomaNotaAsync([FromQuery] ConsultaListaTomaNotaRequest entidad, [FromBody] DtParametersrequest dataTablesParameters)
        {
            try
            {
                var response = await _negocio.Consultar(entidad, dataTablesParameters);
                if (response.Status == ResponseStatus.Success)
                {
                    if (response.Response.Count > 0)
                    {
                        return Ok(response);
                    }
                    else
                    {
                        return Ok(null);
                    }
                }
                else
                {
                    return Ok(null);
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("ListaTomaNotaByAsignador")]
        public async Task<IActionResult> ListaTomaNotaByAsignador([FromQuery] ConsultaListaTomaNotaByAsignadorRequest entidad, [FromBody] DtParametersrequest dataTablesParameters)
        {
            try
            {
                var response = await _negocio.Consultar(entidad, dataTablesParameters);
                if (response.Status == ResponseStatus.Success)
                {
                    if (response.Response.Count > 0)
                    {
                        return Ok(response);
                    }
                    else
                    {
                        return Ok(null);
                    }
                }
                else
                {
                    return Ok(null);
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("ConteoTomaNota")]
        public async Task<IActionResult> ConteoTomaNotaAsync([FromQuery] ConsultaListaTomaNotaRequest entidad)
        {
            try
            {
                //entidad.tipoOperacion = 10; //Conteo de Tramites
                var response = await _negocio.Conteo(entidad);
                if (response.Status == ResponseStatus.Success)
                {
                    if (response.Response.Count > 0)
                    {
                        return Ok(response);
                    }
                    else
                    {
                        return NoContent();
                    }
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

        [HttpPost("ConteoTomaNotaByAsignador")]
        public async Task<IActionResult> ConteoTomaNotaByAsignador([FromQuery] ConsultaListaTomaNotaByAsignadorRequest entidad)
        {
            try
            {
                //entidad.tipoOperacion = 10; //Conteo de Tramites
                var response = await _negocio.Conteo(entidad);
                if (response.Status == ResponseStatus.Success)
                {
                    if (response.Response.Count > 0)
                    {
                        return Ok(response);
                    }
                    else
                    {
                        return NoContent();
                    }
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
