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
    public class ConsultaListaCatalogosColoniaController : Controller
    {
        #region Propiedades
        private readonly ConsultaListaCatalogoColoniaNegocio _negocio;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public ConsultaListaCatalogosColoniaController()
        {
            _negocio = new ConsultaListaCatalogoColoniaNegocio();
        }
        #endregion

        #region Métodos publicos
        [HttpGet("[action]")]
        public async Task<IActionResult> Get([FromQuery] ConsultaListaCatalogoColoniaRequest request)
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
                log.LogError("ConsultaListaCatalogosColoniaController - Get", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }

        [HttpPost("ListaColonias")]
        public async Task<IActionResult> ListaColoniasAsync([FromQuery] ConsultaListaCatalogoColoniaRequest entidad, [FromBody] DtParametersrequest dataTablesParameters)
        {
            try
            {
                //entidad.start = dataTablesParameters.start;
                //entidad.length = dataTablesParameters.length;
                //entidad.keyword =  dataTablesParameters.search.value;
                //entidad.order = dataTablesParameters.order[0].dir;
                //entidad.column = dataTablesParameters.columns[(int)dataTablesParameters.order[0].column].data;

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
        }
        [HttpPost("ConteoColonias")]
        public async Task<IActionResult> ConteoColoniasAsync([FromQuery] ConsultaListaCatalogoColoniaRequest entidad, [FromBody] DtParametersrequest dataTablesParameters)
        {
            try
            {
                //entidad.tipoOperacion = 10; //Conteo de Colonias
                var response =await  _negocio.Conteo(entidad, dataTablesParameters);
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
