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

namespace Asuntos_Religiosos_Api.Controllers.Catalogos
{
    [Area("Catalogos")]
    [Route("TRAMITESDGAR/Catalogos/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Authorize]
    public class DirectorController : ControllerBase
    {

        #region Propiedades
        private readonly CatalogoDirectorNegocio _negocio;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public DirectorController()
        {
            _negocio = new CatalogoDirectorNegocio();
        }
        #endregion

        #region Métodos publicos
        [HttpGet("[action]")]
        public async Task<IActionResult> Get([FromQuery] ConsultaListaDirectorRequest request)
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
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                log.LogError("DirectorController - Get", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Post([FromBody] InsertarCatalogoDirectorRequest[] request)
        {
            try
            {
                var result = await _negocio.Insertar(request);
                if (result.Status == ResponseStatus.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                log.LogError("DirectorController - Post", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Put([FromBody] ActualizarDirectorRequest request)
        {
            try
            {
                var result = await _negocio.Modificar(request);
                if (result.Status == ResponseStatus.Success)
                {
                    if (result.Response.Count > 0)
                    {
                        if (result.Response[0].proceso_exitoso == true)
                        {
                            return Ok(result);
                        }
                        else {
                            return BadRequest(result);
                        }
                    }
                    else
                    {
                        return BadRequest(result);
                    }
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                log.LogError("DirectorController - Put", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody] EliminarDirectorRequest request)
        {
            try
            {
                var result = await _negocio.Eliminar(request);
                if (result.Status == ResponseStatus.Success)
                {
                    if (result.Response.Count > 0)
                    {
                        if (result.Response[0].proceso_exitoso == true)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return BadRequest(result);
                        }
                    }
                    else
                    {
                        return BadRequest(result);
                    }
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                log.LogError("DirectorController - Delete", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }

        #endregion

    }
}
