﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Modelos.Modelos.Request;
using Modelos.Response;
using Negocio.Operaciones;
using Religiosos_Api.Helper.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers.Operaciones
{
    [Area("Operaciones")]
    [Route("TRAMITESDGAR/Operaciones/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Authorize]
    public class ConsultaListaPerfilesController : Controller
    {
        //public class ConsultaListaConvocatoriasController : Controller
        //{
            #region Propiedades
            private readonly ConsultaListaPerfilesNegocio _negocio;
            private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
            #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public ConsultaListaPerfilesController()
            {
                _negocio = new ConsultaListaPerfilesNegocio();
            }
            #endregion

            #region Métodos publicos
            [HttpGet("[action]")]
            public async Task<IActionResult> Get([FromQuery] ConsultaListaPerfilesRequest request)
            {
                try
                {

                var us_session = AutenticacionHelper.UsuarioActualEnSesion(HttpContext);
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
                log.LogError("ConsultaListaPerfilesController - Get", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
                }

            }
            #endregion
        //}
    }
}
