using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Modelos.Modelos.Request;
using Modelos.Modelos.Utilidades;
using Modelos.Modelos.Utilidades.Request;
using Modelos.Response;
using Negocio.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilidades.CifradoMd5;
using Utilidades.EnvioCorreoElectronico;

namespace Api.Controllers.Operaciones
{
    [Area("Operaciones")]
    [Route("TRAMITESDGAR/Operaciones/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    public class RecuperarContrasenaController : Controller
    {

        #region Propiedades
        private readonly RecuperarContrasenaNegocio _negocio;
        private readonly IConfiguration _configuration;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public RecuperarContrasenaController(IConfiguration configuration)
        {
            _negocio = new RecuperarContrasenaNegocio();
            _configuration = configuration;
        }
        #endregion

        [HttpPost("[action]")]
        public async Task<IActionResult> ComprobarCorreo([FromBody] ConsultarCorreoRequest entidad)
        {
            try
            {

                var response = await _negocio.Consultar(entidad);
                if (response.Status == ResponseStatus.Success)
                {
                    if (!string.IsNullOrEmpty(response.respuesta) || response.Response.Count > 0)
                    {

                        CifradoMd5 cifradoMd5 = new CifradoMd5();
                        DateTime fechaActual = DateTime.Now;
                        string token = cifradoMd5.cifrar(response.Response[0].IdUsuario.ToString() + "_" + fechaActual);
                        string url = entidad.Url + "/" + token;
                        string nombreUsuario =  !string.IsNullOrEmpty(response.Response[0].Nombre.ToString()) ? response.Response[0].Nombre.ToString() + " " : " ";
                        string appUsuario = !string.IsNullOrEmpty(response.Response[0].ApPaterno.ToString()) ? response.Response[0].ApPaterno.ToString() + " " : " ";
                        string apmUsuario = !string.IsNullOrEmpty(response.Response[0].ApMaterno) ? response.Response[0].ApMaterno.ToString() : " ";
                        string Usuario = !string.IsNullOrEmpty(response.Response[0].Usuario.ToString()) ? response.Response[0].Usuario.ToString() : " ";



                        EmailMessage emailMessage = new EmailMessage();
                        String bodyCorreo = "<HTML><head><style>a:hover{ background-color: #828282!important; }</style></head><h1>" + nombreUsuario + appUsuario + apmUsuario + "</h1 >" +
                                     "<br>Por favor ingrese al siguiente enlace para recuperar su contraseña.<br>" +
                                     "<br>Solo estará disponble por 1 hora.<br>" +
                                     "<br>Usuario: "+ Usuario + "<br><br><br>" +
                                     "<div><a href='" + url + "' style='background-color: #ccc; border:1px solid #98989A; padding:10px; border-radius: 5px; color: black; text-decoration-line:none;' target='_blank'>Recuperar Contraseña</a></div>" +
                                     "<br><br>Si no lo has hecho tú, por favor ignora este mensaje.<br>";

                        String subjectCorreo = "Cambio de contraseña";

                        EnvioCorreoSMTP envioCorreo = new EnvioCorreoSMTP();
                        envioCorreo.Send(response.Response[0].CorreoUsuario.ToString(), subjectCorreo, bodyCorreo, _configuration["Correo:email"], _configuration["Correo:contrasena"], _configuration["Correo:smtp"], _configuration["Correo:puerto"], _configuration["Correo:usuario"]);

                        return Ok(response);
                    }
                    else
                    {
                        response.mensaje = "El correo ingresado es incorrecto, favor de validar su información";
                        return Ok(response);
                    }
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {

                log.LogError("RecuperarContrasenaController - ComprobarCorreo", ex);
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CambiarContrasena([FromBody] RecuperarContrasenaRequest entidad)
        {
            try
            {
                CifradoMd5 cifradoMd5 = new CifradoMd5();
                DateTime fechaActual = DateTime.Now;
                DateTime fechaVencimiento = DateTime.Now;
                int UsuarioId = 0;

                if (entidad.TokenContrasena != null)
                {
                    string tokenContra = cifradoMd5.descifrar(entidad.TokenContrasena);

                    //string tokenContra = Encoding.UTF8.GetString(Convert.FromBase64String(entidad.TokenContrasena));
                    string[] idFecha = tokenContra.Split('_');

                    UsuarioId = Int32.Parse(idFecha[0]);
                    DateTime fechaSolicitud = Convert.ToDateTime(idFecha[1]);

                    fechaVencimiento = fechaSolicitud.AddMinutes(60);
                    fechaActual = DateTime.Now;

                }
                if (entidad.TokenContrasena != null)
                {
                    if (fechaActual <= fechaVencimiento)
                    {

                        var response = await _negocio.Operacion(entidad, UsuarioId);
                        if (response.Status == ResponseStatus.Success)
                        {
                            if (!string.IsNullOrEmpty(response.respuesta) || response.Response.Count > 0)
                            {
                                string nombreUsuario = response.Response[0].Nombre.ToString() != null ? response.Response[0].Nombre.ToString() + " " : " ";
                                string appUsuario = response.Response[0].ApPaterno.ToString() != null ? response.Response[0].ApPaterno.ToString() + " " : " ";
                                string apmUsuario = response.Response[0].ApMaterno.ToString() != null ? response.Response[0].ApMaterno.ToString() : " ";


                                String bodyCorreo = "<HTML><head><style>a:hover{ background-color: #828282!important; }</style></head> <h1>" + nombreUsuario + appUsuario + apmUsuario + "</h1 >" +
                                                      "<br>Tu contraseña ha sido restablecida.<br></HTML><br>" +
                                                      "<div><a href='" + entidad.url + "' style='background-color: #ccc; border:1px solid #98989A; padding:10px; border-radius: 5px; color: black; text-decoration-line:none;' target='_blank'>" +
                                                        "Iniciar Sesión </a></div><br></HTML>";
                                String subjectCorreo = "Cambio de contraseña";

                                EnvioCorreoSMTP envioCorreo = new EnvioCorreoSMTP();
                                envioCorreo.Send(response.Response[0].CorreoUsuario.ToString(), subjectCorreo, bodyCorreo, _configuration["Correo:email"], _configuration["Correo:contrasena"], _configuration["Correo:smtp"], _configuration["Correo:puerto"], _configuration["Correo:usuario"]);

                                return Ok(response);
                            }
                            else
                            {
                                return NoContent();
                            }
                        }
                        else
                        {
                            return BadRequest(response);
                        }
                    }
                    else
                    {
                        string mensaje = "El tiempo para cambiar la contraseña a caducado, vuelva a restablecer su contraseña.";
                        return BadRequest(mensaje);
                    }
                }
                else
                {

                    var response = await _negocio.Operacion(entidad, entidad.idUsuario);
                    if (response.Status == ResponseStatus.Success)
                    {
                        if (!string.IsNullOrEmpty(response.respuesta) || response.Response.Count > 0)
                        {
                            string nombreUsuario = response.Response[0]?.Nombre?.ToString() != null ? response.Response[0]?.Nombre?.ToString() + " " : " ";
                            string appUsuario = response.Response[0]?.ApPaterno?.ToString() != null ? response.Response[0]?.ApPaterno?.ToString() + " " : " ";
                            string apmUsuario = response.Response[0]?.ApMaterno?.ToString() != null ? response.Response[0]?.ApMaterno?.ToString() : " ";

                            EmailAddress emailAddress = new EmailAddress();
                            emailAddress.Name = response.Response[0].CorreoUsuario.ToString();
                            emailAddress.Address = response.Response[0].CorreoUsuario.ToString();

                            EmailMessage emailMessage = new EmailMessage();

                            String bodyCorreo = "<HTML> <head><style>a:hover{ background-color: #828282!important; }</style></head><h1>" + nombreUsuario + appUsuario + apmUsuario + "</h1 >" +
                                                  "<br>Tu contraseña ha sido restablecida.<br></HTML><br>" +
                                                  "<div><a href='" + entidad.url + "' style='background-color: #ccc; border:1px solid #98989A; padding:10px; border-radius: 5px; color: black; text-decoration-line:none;' target='_blank'>" +
                                                    "Iniciar Sesión </a></div><br></HTML>";

                            emailMessage.ToAddresses.Add(emailAddress);
                            emailMessage.FromAddresses.Add(emailAddress);
                            String subjectCorreo = "Cambio de contraseña";

                            EnvioCorreoSMTP envioCorreo = new EnvioCorreoSMTP();
                            envioCorreo.Send(response.Response[0].CorreoUsuario.ToString(), subjectCorreo, bodyCorreo, _configuration["Correo:email"], _configuration["Correo:contrasena"], _configuration["Correo:smtp"], _configuration["Correo:puerto"], _configuration["Correo:usuario"]);

                            return Ok(response);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                    else
                    {
                        return BadRequest(response);
                    }


                }
            }
            catch (Exception ex)
            {
                log.LogError("RecuperarContrasenaController - CambiarContrasena", ex);
                //return BadRequest(ex.Message);
                return BadRequest("El token para restablecer de contraseña no es correcto o ya expiró, por favor vuelve a solicitarlo.");
            }

        }

    }
}
