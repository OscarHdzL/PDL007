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
    //[Authorize]
    
    public class CatalogosTramiteDeclaratoriaController : Controller
    {
        
        #region Propiedades
        private readonly CatalogosTramiteDeclaratoriaNegocio _negocio;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        #endregion
        
        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public CatalogosTramiteDeclaratoriaController()
        {
            _negocio = new CatalogosTramiteDeclaratoriaNegocio();
        }
        #endregion
        
        #region Métodos publicos
        
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUsoInmueble()
        {
            try
            {
                var result = await _negocio.GetUsoInmueble();
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
                log.LogError("CatalogosTramiteDeclaratoriaController - GetUsoInmueble", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEstatus()
        {
            try
            {
                var result = await _negocio.GetEstatus();
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
                log.LogError("CatalogosTramiteDeclaratoriaController - GetEstatus", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDictaminadores()
        {
            try
            {
                var result = await _negocio.GetDictaminadores();
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
                log.LogError("CatalogosTramiteDeclaratoriaController - GetDictaminadores", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEstatusReporte([FromQuery] int p_tipo_tramite)
        {
            try
            {
                var result = await _negocio.GetEstatusReporte(p_tipo_tramite);
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
                log.LogError("CatalogosTramiteDeclaratoriaController - GetDictaminadores", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }
        
        #endregion
        
        
    }
}
