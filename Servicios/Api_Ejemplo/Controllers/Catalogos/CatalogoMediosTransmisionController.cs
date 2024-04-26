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
using Modelos.Interfaz;
using Modelos.Modelos.Response;

namespace Asuntos_Religiosos_Api.Controllers.Catalogos
{
    [Area("Catalogos")]
    [Route("TRAMITESDGAR/Catalogos/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Authorize]
    public class CatalogoMediosTransmisionController : Controller
    {
        #region Propiedades
        private readonly CatalogoMediosTransmisionNegocio _negocio;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public CatalogoMediosTransmisionController()
        {
            _negocio = new CatalogoMediosTransmisionNegocio();
        }
        #endregion

        #region Métodos publicos
        [HttpGet("[action]")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var model = new CatalogoMediosTransmisionResponse();
                model.tipo_operacion = (int)TipoOperacion.consultar;

                var result = await _negocio.Consultar(model);

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
                log.LogError("CatalogoMediosTransmisionResponseController - Get", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Post([FromBody] CatalogoMediosTransmisionResponse model)
        {
            try
            {
                model.tipo_operacion = (int)TipoOperacion.agregar;
                var result = await _negocio.Operacion(model);
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
                log.LogError("InsertarCatalogosColoniaController - Post", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Put([FromBody] ActualizarMediosTransmisionRequest request)
        {
            try
            {
                var result = await _negocio.Modificar(request);
                if (result.Status == ResponseStatus.Success)
                {
                    if (result.Response.Count > 0)
                    {
                        if (result.Response[0].id != 0)
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
                log.LogError("DirectorController - Put", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody] BorraMediosTransmisionRequest request)
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
