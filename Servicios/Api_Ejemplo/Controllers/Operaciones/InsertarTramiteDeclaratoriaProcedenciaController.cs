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

namespace Asuntos_Religiosos_Api.Controllers.Operaciones
{
    [Area("Operaciones")]
    [Route("TRAMITESDGAR/Operaciones/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    //[Authorize]
    
    public class InsertarTramiteDeclaratoriaProcedenciaController  : Controller
    {
        #region Propiedades
        private readonly InsertarTramiteDeclaratoriaProcedenciaNegocio _negocio;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        #endregion
        
        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public InsertarTramiteDeclaratoriaProcedenciaController()
        {
            _negocio = new InsertarTramiteDeclaratoriaProcedenciaNegocio();
        }
        
        #endregion
        
        #region Métodos publicos
        
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertarPaso1([FromBody] InsertarTramiteDeclaratoriaPaso1 request)
        {
            try
            {
                var resultado = await _negocio.InsertarPaso1(request);
                if (resultado.Status == ResponseStatus.Success)
                {
                    return Ok(resultado);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                log.LogError("TramiteDeclaratoria - InsertarPaso1", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            } 

        }
        
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertarPaso2([FromBody] InsertarTramiteDeclaratoriaPaso2 request)
        {
            try
            {
                var resultado = await _negocio.InsertarPaso2(request);
                if (resultado.Status == ResponseStatus.Success)
                {
                    return Ok(resultado);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                log.LogError("TramiteDeclaratoria - InsertarPaso2", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            } 

        }
        
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertarPaso4([FromBody] InsertarTramiteDeclaratoriaPaso4 request)
        {
            try
            {
                var resultado = await _negocio.InsertarPaso4(request);
                if (resultado.Status == ResponseStatus.Success)
                {
                    return Ok(resultado);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                log.LogError("TramiteDeclaratoria - InsertarPaso4", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            } 

        }
        
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertarPaso5([FromBody] InsertarTramiteDeclaratoriaPaso5 request)
        {
            try
            {
                var resultado = await _negocio.InsertarPaso5(request);
                if (resultado.Status == ResponseStatus.Success)
                {
                    return Ok(resultado);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                log.LogError("TramiteDeclaratoria - InsertarPaso5", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            } 

        }
        #endregion
        
    }
}