﻿using Microsoft.AspNetCore.Authorization;
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

namespace Asuntos_Religiosos_Api.Controllers.Operaciones
{
    [Area("Operaciones")]
    [Route("TRAMITESDGAR/Operaciones/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Authorize]
    public class ActualizarSolicitudEscritoTomaNotaController : Controller
    {
        #region Propiedades
        private readonly ActualizarSolicitudEscritoTomaNotaNegocio _negocio;
        private readonly IConfiguration _configuration;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public ActualizarSolicitudEscritoTomaNotaController(IConfiguration configuration)
        {
            _negocio = new ActualizarSolicitudEscritoTomaNotaNegocio();
            _configuration = configuration;
        }
        #endregion

        #region Métodos publicos
        [HttpGet("[action]")]
        public async Task<IActionResult> Get([FromQuery] ActualizarSolicitudEscritoTomaNotaRequest request)
        {
            try
            {
                var resultado = await _negocio.Operacion(request);
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
                log.LogError("ActualizarSolicitudEscritoTomaNotaController - Post", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }
        #endregion
    }
}
