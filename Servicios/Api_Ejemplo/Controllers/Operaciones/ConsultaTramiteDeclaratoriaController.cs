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
   // [Authorize]
    
    public class ConsultaTramiteDeclaratoriaController : Controller
    {
        #region Propiedades
        private readonly ConsultaTramiteDeclaratoriaNegocio _negocio;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public ConsultaTramiteDeclaratoriaController()
        {
            _negocio = new ConsultaTramiteDeclaratoriaNegocio();
        }
        #endregion
        
        #region Métodos publicos
        
        [HttpGet("[action]")]
        public async Task<IActionResult> ConsultaPaso1([FromQuery] int id_declaratoria)
        {
            try
            {
                var result = await _negocio.ConsultaPaso1(id_declaratoria);
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
                log.LogError("ConsultaTramiteDeclaratoriaController - Paso1", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> ConsultaPaso2([FromQuery] int id_declaratoria, int tipo_domicilio)
        {
            try
            {
                var result = await _negocio.ConsultaPaso2(id_declaratoria, tipo_domicilio);
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
                log.LogError("ConsultaTramiteDeclaratoriaController - Paso2", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> ConsultaPaso4([FromQuery] int id_declaratoria)
        {
            try
            {
                var result = await _negocio.ConsultaPaso4(id_declaratoria);
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
                log.LogError("ConsultaTramiteDeclaratoriaController - Paso1", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> ConsultaPaso5([FromQuery] int id_declaratoria)
        {
            try
            {
                var result = await _negocio.ConsultaPaso5(id_declaratoria);
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
                log.LogError("ConsultaTramiteDeclaratoriaController - Paso1", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> ConsultaAvance([FromQuery] int id_declaratoria)
        {
            try
            {
                var result = await _negocio.ConsultaAvance(id_declaratoria);
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
                log.LogError("ConsultaTramiteDeclaratoriaController - Avance", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> ConsultaLista([FromQuery] int id_usuario = 0, int id_rol = 0)
        {
            try
            {
                var result = await _negocio.ConsultaLista(id_usuario, id_rol);
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
                log.LogError("ConsultaTramiteDeclaratoriaController - Avance", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ConsultaListaFiltros([FromBody] ConsultaTramiteDeclaratoriaListaFiltrosRequest request)
        {
            try
            {
                var result = await _negocio.ConsultaLista(request);
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
                log.LogError("ConsultaTramiteDeclaratoriaController - ConsultaListaFiltros", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }
        #endregion

    }
}