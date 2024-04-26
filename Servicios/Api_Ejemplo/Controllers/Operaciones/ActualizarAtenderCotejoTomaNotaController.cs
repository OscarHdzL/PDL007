using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Modelos.Modelos.Request;
using Modelos.Modelos.Utilidades;
using Modelos.Response;
using Negocio.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilidades.EnvioCorreoElectronico;

namespace Asuntos_Religiosos_Api.Controllers.Operaciones
{
    [Area("Operaciones")]
    [Route("TRAMITESDGAR/Operaciones/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Authorize]
    public class ActualizarAtenderCotejoTomaNotaController : Controller
    {
        #region Propiedades
        private readonly ActualizarAtenderCotejoTomaNotaNegocio _negocio;
        private readonly IConfiguration _configuration;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public ActualizarAtenderCotejoTomaNotaController(IConfiguration configuration)
        {
            _negocio = new ActualizarAtenderCotejoTomaNotaNegocio();
            _configuration = configuration;
        }
        #endregion

        #region Métodos publicos
        [HttpPost("[action]")]
        public async Task<IActionResult> Post([FromBody] ActualizarCotejoRequest request)
        {
            try
            {
                var resultado = await _negocio.Operacion(request);
                if (resultado.Status == ResponseStatus.Success)
                {
                    if (!string.IsNullOrEmpty(resultado.respuesta) || resultado.Response.Count > 0)
                    {
                        if ((bool)resultado.Response[0]?.proceso_existoso)
                        {
                            var usuario = await _negocio.Consulta(new ConsultaDetalleUsuarioSistemaRequest { id_usuario = resultado.Response[0].id_gen });

                            if (usuario.Response.Count > 0)
                            {
                                var currentUser = usuario.Response[0];
                                string nombreUsuario = currentUser.nombre;
                                string appUsuario = currentUser.apellido_paterno;
                                string apmUsuario = currentUser.apellido_materno;
                                EmailAddress FromemailAddress = new EmailAddress
                                {
                                    Name = $"SEGOB",
                                    Address = "noreply@segob.com.mx"
                                };
                                EmailAddress ToemailAddress = new EmailAddress
                                {
                                    Name = $"{nombreUsuario} {appUsuario} {apmUsuario}",
                                    Address = currentUser.usuario
                                };


                                EmailMessage emailMessage = new EmailMessage();
                                String bodyCorreo = "<HTML style='padding:20px;'><head><style>a:hover{ background-color: #828282!important; }</style></head><h1> Estimado(a):  " + ToemailAddress.Name + "</h1><br>" +
                                                    "<div>Por favor, ingrese a su cuenta para dar seguimiento a su trámite de toma de nota.</div><br>" +
                                                    "<div>Ingrese a la liga siguiente: (<a href=\"https://tramitesdgar.segob.gob.mx/\">https://tramitesdgar.segob.gob.mx/</a>)</div></HTML>";

                                emailMessage.ToAddresses.Add(ToemailAddress);
                                emailMessage.FromAddresses.Add(FromemailAddress);
                                emailMessage.Body = bodyCorreo;
                                emailMessage.Subject = "Registro Trámite - Dirección General de Asuntos Religiosos";

                                EnvioCorreoSMTP envioCorreo = new EnvioCorreoSMTP();
                                envioCorreo.Send(currentUser.usuario, emailMessage.Subject, bodyCorreo, _configuration["Correo:email"], _configuration["Correo:contrasena"], _configuration["Correo:smtp"], _configuration["Correo:puerto"], _configuration["Correo:usuario"]);

                            }
                        }
                    }
                    return Ok(resultado);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                log.LogError("ActualizarAtenderCotejoTomaNotaController - Post", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }
        #endregion
    }
}
