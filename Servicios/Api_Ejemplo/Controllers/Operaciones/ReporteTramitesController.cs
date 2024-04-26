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
using Microsoft.Extensions.Configuration;
using Modelos.Modelos.Utilidades;
using Utilidades.EnvioCorreoElectronico;

namespace Asuntos_Religiosos_Api.Controllers.Operaciones
{
    [Area("Operaciones")]
    [Route("TRAMITESDGAR/Operaciones/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    //[Authorize]
    
    public class ReporteTramitesController : Controller
    {
        #region Propiedades
        private readonly ReporteTramitesNegocio _negocio;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        #endregion
        
        #region Constructor
        public ReporteTramitesController()
        {
            _negocio = new ReporteTramitesNegocio();
        }
        #endregion
        
        #region Métodos publicos
        [HttpGet("[action]")]
        public async Task<IActionResult> General([FromQuery] ReporteTramitesRequest request)
        {
            try
            {
                var transmisiones = await _negocio.GetTransmisiones(request);
                var declaratorias = await _negocio.GetDeclaratorias(request);
                var notas = await _negocio.GetNotas(request);
                var registros = await _negocio.GetRegistros(request);
                
                transmisiones.Response.AddRange(declaratorias.Response);
                transmisiones.Response.AddRange(notas.Response);
                transmisiones.Response.AddRange(registros.Response);
                
                if (transmisiones.Status == ResponseStatus.Success)
                {
                    if (transmisiones.Response.Count > 0)
                    {
                        return Ok(transmisiones);
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
                log.LogError("ReporteTramitesController - GetTransmisiones", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> GetTransmisiones([FromQuery] ReporteTramitesRequest request)
        {
            try
            {
                var result = await _negocio.GetTransmisiones(request);
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
                log.LogError("ReporteTramitesController - GetTransmisiones", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDeclaratorias([FromQuery] ReporteTramitesRequest request)
        {
            try
            {
                var result = await _negocio.GetDeclaratorias(request);
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
                log.LogError("ReporteTramitesController - GetDeclaratorias", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> GetNota([FromQuery] ReporteTramitesRequest request)
        {
            try
            {
                var result = await _negocio.GetNotas(request);
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
                log.LogError("ReporteTramitesController - GetNota", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> GetRegistro([FromQuery] ReporteTramitesRequest request)
        {
            try
            {
                var result = await _negocio.GetRegistros(request);
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
                log.LogError("ReporteTramitesController - GetRegistro", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }
        }
        
        
        #endregion
    }
}
