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
    [Authorize]
    public class InsertarTransmisionActosReligiososController : Controller
    {
        #region Propiedades
        private readonly InsertarTransmisionActosReligiososNegocio _negocio;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public InsertarTransmisionActosReligiososController()
        {
            _negocio = new InsertarTransmisionActosReligiososNegocio();
        }
        #endregion

        #region Métodos publicos
        [HttpPost("[action]")]
        public async Task<IActionResult> Post([FromBody] InsertarTransmisionActosReligiososRequest model)
        {
            try
            {
                var result = await _negocio.Operacion(model);
                if (result.Status == ResponseStatus.Success)
                {
                    return Ok(this.GuardarFechasEmisoras(model.cat_FechasHorario, model.cat_Emisoras, result.Response[0].id_acto_religioso));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                log.LogError("InsertarTransmisionActosReligiososController - Post", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }


        [HttpPost("[action]")]
        public IActionResult GuardarFechas([FromBody] InsertarActosFechasRequest model)
        {
            try
            {
                var result = _negocio.GuardarFechas(model);

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
                log.LogError("InsertarTransmisionActosReligiososController - Post", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }
        }


        [HttpPost("[action]")]
        public IActionResult GuardarEmisoras([FromBody] InsertarActosEmisorasRequest model)
        {
            try
            {
                var result = _negocio.GuardarEmisoras(model);

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
                log.LogError("InsertarTransmisionActosEmisorasController - Post", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }
        }

        [DisableRequestSizeLimit]
        [HttpPost("[action]")]
        public ActionResult<List<string>> GuardarFechasEmisoras([FromBody] List<InsertarActosFechasRequest> fechas, List<InsertarActosEmisorasRequest> emisoras, int id_acto_religioso)
        {
            List<string> storageResultList = new List<string>();

            fechas.ForEach(actoFecha => {

                actoFecha.i_id_tbl_acto_religioso = id_acto_religioso;

                ObjectResult respuesta = this.GuardarFechas(actoFecha) as ObjectResult;

                storageResultList.Add(respuesta is null ? "null" : (respuesta.Value is null ? "null" : respuesta.Value.ToString()));
            });

            emisoras.ForEach(actoEmisora => {

                actoEmisora.i_id_acto = id_acto_religioso;

                ObjectResult respuesta = this.GuardarEmisoras(actoEmisora) as ObjectResult;

                storageResultList.Add(respuesta is null ? "null" : (respuesta.Value is null ? "null" : respuesta.Value.ToString()));
            });

            return storageResultList;
        }

        #endregion
    }
}
