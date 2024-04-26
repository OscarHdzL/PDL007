using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Modelos.Modelos.Request;
using Modelos.Modelos.Response;
using Modelos.Modelos.Utilidades;
using Modelos.Response;
using Negocio.Operaciones;
using Religiosos_Api.Helper.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilidades.CifradoMd5;
using Utilidades.EnvioCorreoElectronico;

namespace Religiosos_Api.Controllers.Operaciones
{
    [Area("Operaciones")]
    [Route("TRAMITESDGAR/Operaciones/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    public class InsertarUsuarioSistemaController : Controller
    {
        #region Propiedades
        private readonly InsertarUsuarioSistemaNegocio _negocio;
        private readonly IConfiguration _configuration;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public InsertarUsuarioSistemaController(IConfiguration configuration)
        {
            _negocio = new InsertarUsuarioSistemaNegocio();
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
                            CifradoMd5 cifradoMd5 = new CifradoMd5();

                            var appUrl = request.url;

                            var urlconfirmacion = appUrl + "/confirmar-correo/" + cifradoMd5.cifrar(resultado.Response[0].id_usuario.ToString());
                            //var urlconfirmacion = appUrl + "/confirmar-correo/" + HerramientasHelper.Encrypt(resultado.Response[0].id_usuario.ToString());
                            EmailAddress FromemailAddress = new EmailAddress
                            {
                                Name = $"SEGOB",
                                Address = "noreply@segob.com.mx"
                            };
                            EmailAddress ToemailAddress = new EmailAddress
                            {
                                Name = $"{nombreUsuario} {appUsuario} {apmUsuario}",
                                Address = request.correo_electronico
                            };
                            EmailMessage emailMessage = new EmailMessage();
                            String bodyCorreo = "<HTML style='padding:20px;'><head><style>a:hover{ background-color: #828282!important; }</style></head><h1> Estimado(a)  " + ToemailAddress.Name + "</h1><br>" +
                                                "<div>Por favor, ingrese al siguiente enlace para confirmar su cuenta y tener acceso a los trámites" +
                                                " de la Dirección General de Asuntos Religiosos.</div></br><br>" +
                                                "<div>Usuario: <strong>"+ request.usuario +"</strong></div>" +
                                                "<br>" +
                                                "<div>Contraseña: <strong>"+ resultado.Response[0].contrasenia + "</strong></div>" +
                                                "<br><br>" +
                                                "<div><a href='" + urlconfirmacion + "' style='background-color: #ccc; border:1px solid #98989A; padding:10px; border-radius: 5px; color: black; text-decoration-line:none;' target='_blank'>" +
                                                "Confirmar cuenta </a></div><br></HTML>";

                            emailMessage.ToAddresses.Add(ToemailAddress);
                            emailMessage.FromAddresses.Add(FromemailAddress);
                            emailMessage.Body=bodyCorreo;
                            emailMessage.Subject= "Confirmar cuenta - Dirección General de Asuntos Religiosos";

                            EnvioCorreoSMTP envioCorreo = new EnvioCorreoSMTP();
                            envioCorreo.Send(request.correo_electronico, emailMessage.Subject, bodyCorreo, _configuration["Correo:email"],_configuration["Correo:contrasena"], _configuration["Correo:smtp"], _configuration["Correo:puerto"], _configuration["Correo:usuario"]);
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
                log.LogError("InsertarUsuarioSistemaController - Post", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }
        #endregion
    }
}
