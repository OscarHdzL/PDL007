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

namespace Religiosos_Api.Controllers.Operaciones
{
    [Area("Operaciones")]
    [Route("TRAMITESDGAR/Operaciones/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Authorize]
    public class ConsultaListaTramitesDictaminadorController : Controller
    {
        #region Propiedades
        private readonly ConsultaListaTramitesDictaminadorNegocio _negocio;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public ConsultaListaTramitesDictaminadorController()
        {
            _negocio = new ConsultaListaTramitesDictaminadorNegocio();
        }
        #endregion

        #region Métodos publicos
        [HttpGet("ListaTramites")]
        public async Task<IActionResult> ListaTramitesDictaminadorAsync([FromQuery] ConsultaListaTramitesDictaminadorgetRequest request)
        {
            try
            {

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
                log.LogError("ListaTramitesDictaminadorAsync - Get", ex);
                return BadRequest(new ResponseGeneric<string>(ex.Message));
            }
        }
       /*
        [HttpPost("ListaTramites")]
        public async Task<IActionResult> ListaTramitesDictaminadorAsync([FromQuery] ConsultaListaTramitesDictaminadorRequest entidad, [FromBody] DtParametersrequest dataTablesParameters)
        {
            try
            {

                var response = await _negocio.Consultar(entidad,dataTablesParameters);
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
        }*/
        [HttpPost("ConteoTramites")]
        public async Task<IActionResult> ConteoTramitesDictaminadorAsync([FromQuery] ConsultaListaTramitesDictaminadorRequest entidad,[FromBody] DtParametersrequest dataTablesParameters)
        {
            try
            {
                entidad.keyword = dataTablesParameters.search.value;
                //entidad.tipoOperacion = 10; //Conteo de TramitesDictaminador
                var response =await  _negocio.Conteo(entidad);
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
