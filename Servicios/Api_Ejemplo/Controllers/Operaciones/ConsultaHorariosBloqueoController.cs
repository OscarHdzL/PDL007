using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Modelos.Modelos.Request;
using Modelos.Modelos.Response;
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
    public class ConsultaHorariosBloqueoController : Controller
    {
        #region Propiedades
        private readonly ConsultaHorariosBloqueoNegocio _negocio;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public ConsultaHorariosBloqueoController()
        {
            _negocio = new ConsultaHorariosBloqueoNegocio();
        }
        #endregion

        #region Métodos publicos
        [HttpGet("[action]")]
        public async Task<IActionResult> Get([FromQuery] ConsultaHorariosBloqueoRequest request)
        {
            try
            {
                var listahorarios = new List<ConsultaListaHorariosResponse>();
                var result = await _negocio.Consultar(request);

                if (result.Status == ResponseStatus.Success)
                {
                    if (result.Response.Count > 0)
                    {
                        //int hora = 7;
                        int hora = 10;
                        int horariobloqueado = result.Response.Count;
                        int j = 0;
                        for (int i = 0; i <= 3; i++) { //10
                            if (horariobloqueado != j)
                            {
                                if ((hora + i) == int.Parse(((result.Response[j].fecha_cotejo).Split(" ")[1]).Split(":")[0]))
                                {
                                    j++;
                                }
                                else { 
                                    listahorarios.Add(new ConsultaListaHorariosResponse { c_id = (hora + i).ToString().PadLeft(2, '0') + ":00", c_nombre_n = (hora + i).ToString().PadLeft(2, '0') + ":00 - " + (hora + i + 1).ToString().PadLeft(2, '0') + ":00" });
                                }
                            }
                            else { 
                                    listahorarios.Add(new ConsultaListaHorariosResponse { c_id = (hora + i).ToString().PadLeft(2, '0') + ":00", c_nombre_n = (hora + i).ToString().PadLeft(2, '0') + ":00 - " + (hora + i + 1).ToString().PadLeft(2, '0') + ":00" });
                            }
                        }
                        var resultado = new ResponseGeneric<List<ConsultaListaHorariosResponse>>(listahorarios);
                        return Ok(resultado);
                    }
                    else
                    {
                        //listahorarios.Add(new ConsultaListaHorariosResponse { c_id = "07:00", c_nombre_n = "07:00 - 08:00" });
                        //listahorarios.Add(new ConsultaListaHorariosResponse { c_id = "08:00", c_nombre_n = "08:00 - 09:00" });
                        //listahorarios.Add(new ConsultaListaHorariosResponse { c_id = "09:00", c_nombre_n = "09:00 - 10:00" });
                        listahorarios.Add(new ConsultaListaHorariosResponse { c_id = "10:00", c_nombre_n = "10:00 - 11:00" });
                        listahorarios.Add(new ConsultaListaHorariosResponse { c_id = "11:00", c_nombre_n = "11:00 - 12:00" });
                        listahorarios.Add(new ConsultaListaHorariosResponse { c_id = "12:00", c_nombre_n = "12:00 - 13:00" });
                        listahorarios.Add(new ConsultaListaHorariosResponse { c_id = "13:00", c_nombre_n = "13:00 - 14:00" });
                        //listahorarios.Add(new ConsultaListaHorariosResponse { c_id = "14:00", c_nombre_n = "14:00 - 15:00" });
                        //listahorarios.Add(new ConsultaListaHorariosResponse { c_id = "15:00", c_nombre_n = "15:00 - 16:00" });
                        //listahorarios.Add(new ConsultaListaHorariosResponse { c_id = "16:00", c_nombre_n = "16:00 - 17:00" });
                        //listahorarios.Add(new ConsultaListaHorariosResponse { c_id = "17:00", c_nombre_n = "17:00 - 18:00" });

                        var resultado = new ResponseGeneric<List<ConsultaListaHorariosResponse>>(listahorarios);
                        return Ok(resultado);

                    }
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                log.LogError("ConsultaHorariosBloqueoController - Get", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }
        #endregion
    }
}
