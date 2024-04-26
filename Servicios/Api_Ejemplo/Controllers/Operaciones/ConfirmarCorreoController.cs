using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Modelos.Modelos.Utilidades.Request;
using Modelos.Response;
using Negocio.Operaciones;
using Religiosos_Api.Helper.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilidades.CifradoMd5;

namespace Api.Controllers.Operaciones
{
    [Area("Operaciones")]
    [Route("TRAMITESDGAR/Operaciones/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    public class ConfirmarCorreoController : ControllerBase
    {
        #region Propiedades
        private readonly ConfirmarCorreoNegocio _negocio;
        private readonly IConfiguration _configuration;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public ConfirmarCorreoController(IConfiguration configuration)
        {
            _negocio = new ConfirmarCorreoNegocio();
            _configuration = configuration;
        }
        #endregion

        #region Métodos publicos
        [HttpPost("[action]")]
        public async Task<IActionResult> Post([FromBody] ConfirmarCorreoRequest request)
        {
            try
            {
                            CifradoMd5 cifradoMd5 = new CifradoMd5();
                //request.id_user_insert = int.Parse(HerramientasHelper.Decrypt(request.id_user));
                request.id_user_insert = int.Parse(cifradoMd5.descifrar(request.id_user));
                //var urlconfirmacion = appUrl + "/confirmar-correo/" + cifradoMd5.cifrar(resultado.Response[0].id_usuario.ToString());

                var resultado = await _negocio.Operacion(request);
                if (resultado.Status == ResponseStatus.Success)
                {
                    return Ok(resultado.Response);

                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                log.LogError("ConfirmarCorreoController - Post", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }
        #endregion

    }
}
