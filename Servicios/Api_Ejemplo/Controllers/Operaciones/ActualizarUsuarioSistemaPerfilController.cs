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

namespace Api.Controllers.Operaciones
{
    [Area("Operaciones")]
    [Route("TRAMITESDGAR/Operaciones/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Authorize]
    public class ActualizarUsuarioSistemaPerfilController : Controller
    {
        #region Propiedades
        private readonly ActualizarUsuarioSistemaPerfilNegocio _negocio;
        private readonly IConfiguration _configuration;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public ActualizarUsuarioSistemaPerfilController(IConfiguration configuration)
        {
            _negocio = new ActualizarUsuarioSistemaPerfilNegocio();
            _configuration = configuration;
        }
        #endregion

        #region Métodos publicos
        [HttpPost("[action]")]
        public async Task<IActionResult> Post([FromBody] ActualizarUsuarioSistemaRequest request)
        {
            try
            {
                var resultado = await _negocio.Operacion(request);
                if (resultado.Status == ResponseStatus.Success)
                {
                    if (!string.IsNullOrEmpty(resultado.respuesta) || resultado.Response.Count > 0)
                    {
                        //if (resultado.Response[0].proceso_exitoso == true)
                        //{
                        //    string nombreUsuario = request.nombre;
                        //    string appUsuario = request.apellido_p;
                        //    string apmUsuario = request.apellido_m;

                        //    //EmailAddress emailAddress = new EmailAddress();
                        //    //emailAddress.Name = request.correo_electronico; ;
                        //    //emailAddress.Address = request.correo_electronico; ;
                        //    EmailMessage emailMessage = new EmailMessage();
                        //    String bodyCorreo = "<HTML><h1>" + nombreUsuario + " " + appUsuario + " " + apmUsuario + "</h1>" +
                        //                 "<br>Esta es tu contraseña de acceso:<strong>" + resultado.Response[0].contrasenia + "<strong></br>" +
                        //                 "<br>" +
                        //                 "<br><span class='es - button - border'><a href='" + request.url + "' class='es-button' target='_blank'>" +
                        //                 "INICIAR SESIÓN </a></span><br></HTML>";

                        //    //emailMessage.ToAddresses.Add(emailAddress);
                        //    //emailMessage.FromAddresses.Add(emailAddress);
                        //    String subjectCorreo = "Registro Usuario";

                        //    EnvioCorreoSMTP envioCorreo = new EnvioCorreoSMTP();
                        //    //envioCorreo.Send(request.correo_electronico, subjectCorreo, bodyCorreo, _configuration["Correo:email"], _configuration["Correo:contrasena"], _configuration["Correo:smtp"]);
                        //    envioCorreo.Send(request.correo_electronico.ToString(), subjectCorreo, bodyCorreo, _configuration["Correo:email"], _configuration["Correo:contrasena"], _configuration["Correo:smtp"], _configuration["Correo:puerto"], _configuration["Correo:usuario"]);
                        //}
                    }

                    return Ok(resultado.Response);

                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                log.LogError("ActualizarUsuarioSistemaPerfilController - Post", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }
        #endregion
    }
}
