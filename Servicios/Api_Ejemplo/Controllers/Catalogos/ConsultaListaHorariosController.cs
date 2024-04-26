using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos.Modelos.Request;
using Modelos.Modelos.Response;
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
    public class ConsultaListaHorariosController : Controller
    {
        #region Propiedades
        //private readonly ConsultaListaCatalogoCotejoNegocio _negocio;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public ConsultaListaHorariosController()
        {
            //_negocio = new ConsultaListaCatalogoCotejoNegocio();
        }
        #endregion
        #region Métodos publicos
        [HttpGet("[action]")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listahorarios = new List<ConsultaListaHorariosResponse>();
                listahorarios.Add(new ConsultaListaHorariosResponse { c_id = "07:00", c_nombre_n = "07:00 - 08:00" });
                listahorarios.Add(new ConsultaListaHorariosResponse { c_id = "08:00", c_nombre_n = "08:00 - 09:00" });
                listahorarios.Add(new ConsultaListaHorariosResponse { c_id = "09:00", c_nombre_n = "09:00 - 10:00" });
                listahorarios.Add(new ConsultaListaHorariosResponse { c_id = "10:00", c_nombre_n = "10:00 - 11:00" });
                listahorarios.Add(new ConsultaListaHorariosResponse { c_id = "11:00", c_nombre_n = "11:00 - 12:00" });
                listahorarios.Add(new ConsultaListaHorariosResponse { c_id = "12:00", c_nombre_n = "12:00 - 13:00" });
                listahorarios.Add(new ConsultaListaHorariosResponse { c_id = "13:00", c_nombre_n = "13:00 - 14:00" });
                listahorarios.Add(new ConsultaListaHorariosResponse { c_id = "14:00", c_nombre_n = "14:00 - 15:00" });
                listahorarios.Add(new ConsultaListaHorariosResponse { c_id = "15:00", c_nombre_n = "15:00 - 16:00" });
                listahorarios.Add(new ConsultaListaHorariosResponse { c_id = "16:00", c_nombre_n = "16:00 - 17:00" });
                listahorarios.Add(new ConsultaListaHorariosResponse { c_id = "17:00", c_nombre_n = "17:00 - 18:00" });

                var result =  new ResponseGeneric<List<ConsultaListaHorariosResponse>>(listahorarios);
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
                log.LogError("ConsultaListaHorariosController - Get", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }
        #endregion
    }
}
