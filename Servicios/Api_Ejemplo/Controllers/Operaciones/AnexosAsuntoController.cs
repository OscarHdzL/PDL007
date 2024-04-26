using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Modelos.Modelos.Request;
using Modelos.Response;
using Negocio.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Carranza.Controllers.Operaciones
{
    [Area("Operaciones")]
    [Route("TRAMITESDGAR/Operaciones/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class AnexosAsuntoController : Controller
    {
        #region Propiedades

        private readonly AnexosAsuntoNegocio _Negocio;
        private readonly IConfiguration _configuration;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();

        #endregion

        #region Constructor

        public AnexosAsuntoController(IConfiguration configuration)
        {
            _Negocio = new AnexosAsuntoNegocio();
            _configuration = configuration;
        }

        #endregion

        #region Métodos Públicos

        [HttpPost("[action]")]
        public async Task<IActionResult> ConsultarListaAnexo(ConsultaListaAnexoAsuntoRequest request)
        {
            try
            {
                var resultado = await _Negocio.ConsultarListaAnexo(request);
                if (resultado.Status == ResponseStatus.Success)
                {
                    return Ok(resultado);
                }
                else
                {
                    resultado.mensaje = "Error al consultar la información.";
                    resultado.Status = ResponseStatus.Failed;
                    return BadRequest(resultado);
                }
            }
            catch (Exception ex)
            {
                log.LogError("AnexosAsuntoController - ConsultarListaAnexo", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ConsultarDetalleAnexo(ConsultaDetalleAnexoAsuntoRequest request)
        {
            try
            {
                var resultado = await _Negocio.ConsultarDetalleAnexo(request, _configuration["pathHostDocuments:RutaArchivo"]);
                if (resultado.Status == ResponseStatus.Success)
                {
                    return Ok(resultado);
                }
                else
                {
                    resultado.mensaje = "Error al consultar la información.";
                    resultado.Status = ResponseStatus.Failed;
                    return BadRequest(resultado);
                }
            }
            catch (Exception ex)
            {
                log.LogError("AnexosAsuntoController - ConsultarDetalleAnexo", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> InsertarAnexoAsunto([FromForm] InsertarAnexoAsuntoRequest request)
        {
            try
            {
                var resultado = await _Negocio.InsertarAnexoAsunto(request, _configuration["pathHostDocuments:RutaArchivo"]);
                if (resultado.Status == ResponseStatus.Success)
                {
                    var respuesta = resultado.Response[0];
                    if (respuesta.proceso_exitoso)
                    {
                        return Ok(resultado);
                    }
                    else
                    {
                        resultado.mensaje = respuesta.mensaje;
                        resultado.Status = ResponseStatus.Failed;
                        return BadRequest(resultado);
                    }
                }
                else
                {
                    resultado.mensaje = "Error al insertar la información.";
                    resultado.Status = ResponseStatus.Failed;
                    return BadRequest(resultado);
                }
            }
            catch (Exception ex)
            {
                log.LogError("AnexosAsuntoController - InsertarAnexoAsunto", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> BorrarAnexoAsunto(BorrarAnexoRequest request)
        {
            try
            {
                var resultado = await _Negocio.BorrarAnexoAsunto(request, _configuration["pathHostDocuments:RutaArchivo"]);
                if (resultado.Status == ResponseStatus.Success)
                {
                    var respuesta = resultado.Response[0];
                    if (respuesta.proceso_exitoso)
                    {
                        return Ok(resultado);
                    }
                    else
                    {
                        resultado.mensaje = respuesta.mensaje;
                        resultado.Status = ResponseStatus.Failed;
                        return BadRequest(resultado);
                    }
                }
                else
                {
                    resultado.mensaje = "Error al eliminar la información.";
                    resultado.Status = ResponseStatus.Failed;
                    return BadRequest(resultado);
                }
            }
            catch (Exception ex)
            {
                log.LogError("AnexosAsuntoController - BorrarAnexoAsunto", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }
        }

        #endregion
    }
}
