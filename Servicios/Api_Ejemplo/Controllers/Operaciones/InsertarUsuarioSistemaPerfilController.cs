using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Modelos.Modelos.Request;
using Modelos.Modelos.Response;
using Modelos.Modelos.Utilidades;
using Modelos.Response;
using Negocio.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilidades.EnvioCorreoElectronico;

namespace Api.Controllers.Operaciones
{
    [Area("Operaciones")]
    [Route("TRAMITESDGAR/Operaciones/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Authorize]
    public class InsertarUsuarioSistemaPerfilController : Controller
    {
        #region Propiedades
        private readonly InsertarUsuarioSistemaPerfilNegocio _negocio;
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public InsertarUsuarioSistemaPerfilController(IConfiguration configuration)
        {
            _negocio = new InsertarUsuarioSistemaPerfilNegocio();
            _configuration = configuration;
        }
        #endregion

        #region Métodos publicos
        [HttpPost("[action]")]
        public async Task<IActionResult> Post([FromBody] InsertarUsuarioSistemaRequest request)
        {
            InsertarUsuarioSistemaNoPassResponse resultadoSimple = new InsertarUsuarioSistemaNoPassResponse();
            try
            {
                var resultado = await _negocio.Operacion(request);
                resultadoSimple.id_usuario = resultado.Response[0].id_usuario;
                resultadoSimple.mensaje = resultado.Response[0].mensaje;
                resultadoSimple.proceso_exitoso = resultado.Response[0].proceso_exitoso;
                if (resultado.Status == ResponseStatus.Success)
                {
                    if (!string.IsNullOrEmpty(resultado.respuesta) || resultado.Response.Count > 0)
                    {
                        if (resultado.Response[0].proceso_exitoso == true)
                        {
                            string nombreUsuario = request.nombre;
                            string appUsuario = request.apellido_p;
                            string apmUsuario = request.apellido_m;

                            //EmailAddress emailAddress = new EmailAddress();
                            //emailAddress.Name = request.correo_electronico; ;
                            //emailAddress.Address = request.correo_electronico; ;
                            EmailMessage emailMessage = new EmailMessage();
                            String bodyCorreo = "<HTML><head><style>a:hover{ background-color: #828282!important; }</style></head><h1>" + nombreUsuario + " " + appUsuario + " " + apmUsuario + "</h1>" +
                                         "<br>Esta es tu contraseña de acceso:<strong>" + resultado.Response[0].contrasenia + "<strong></br>" +
                                         "<br>" +
                                         "<br><span class='es-button-border'><a href='" + request.url + "' style='background-color: #ccc; border:1px solid #98989A; padding:10px; border-radius: 5px; color: black; text-decoration-line:none;' class='es-button' target='_blank'>" +
                                         "INICIAR SESIÓN </a></span><br></HTML>";

                            //emailMessage.ToAddresses.Add(emailAddress);
                            //emailMessage.FromAddresses.Add(emailAddress);
                            String subjectCorreo = "Registro Usuario";

                            EnvioCorreoSMTP envioCorreo = new EnvioCorreoSMTP();
                            //envioCorreo.Send(request.correo_electronico, subjectCorreo, bodyCorreo, _configuration["Correo:email"], _configuration["Correo:contrasena"], _configuration["Correo:smtp"]);
                            envioCorreo.Send(request.correo_electronico.ToString(), subjectCorreo, bodyCorreo, _configuration["Correo:email"], _configuration["Correo:contrasena"], _configuration["Correo:smtp"], _configuration["Correo:puerto"], _configuration["Correo:usuario"]);
                        }
                    }

                    return Ok(resultadoSimple);

                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }
        #endregion
    }
}
