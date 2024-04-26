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
using Utilidades.EnvioCorreoElectronico;

namespace Religiosos_Api.Controllers.Operaciones
{
    [Area("Operaciones")]
    [Route("TRAMITESDGAR/Operaciones/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Authorize]
    public class AsignarDictaminadorRegistroController : Controller
    {
        #region Propiedades
        private readonly AsignarDictaminadorRegistroNegocio _negocio;
        private readonly IConfiguration _configuration;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        private readonly ActualizarTramitePasoSextoNegocio _negocioGeneral;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Inicial de controllador
        /// </summary>
        public AsignarDictaminadorRegistroController(IConfiguration configuration)
        {
            _negocio = new AsignarDictaminadorRegistroNegocio();
            _configuration = configuration;
            _negocioGeneral = new ActualizarTramitePasoSextoNegocio();

        }
        #endregion

        #region Métodos publicos
        [HttpPost("[action]")]
        public async Task<IActionResult> Post([FromBody] AsignarDictaminadorRegistroRequest[] request)
        {
            try
            {
                var resultado = await _negocio.Operacion(request);
                if (resultado.Status == ResponseStatus.Success)
                {
                    if (!string.IsNullOrEmpty(resultado.respuesta) || resultado.Response.Count > 0)
                    {
                        foreach (var itms in request)
                        {

                            var usuario = await _negocioGeneral.Consulta(new ConsultaDetalleUsuarioSistemaRequest { id_usuario = itms.us_dictaminador_id });


                            if (usuario.Response.Count > 0)
                            {
                                foreach (var item in usuario.Response)
                                {
                                    var currentUser = item;
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
                                    String bodyCorreo =
                                       "<HTML style='padding:20px;'><head><style>a:hover{ background-color: #828282!important; }</style></head>" +
                                       "<h1> Estimado(a):  " + ToemailAddress.Name + "</h1><br>" +
                                       "<div>Ha recibido una solicitud de registro.</div><br>" +
                                       "</HTML>";
                                    emailMessage.ToAddresses.Add(ToemailAddress);
                                    emailMessage.FromAddresses.Add(FromemailAddress);
                                    emailMessage.Body = bodyCorreo;
                                    emailMessage.Subject = "Registro Trámite - Dirección General de Asuntos Religiosos";

                                    EnvioCorreoSMTP envioCorreo = new EnvioCorreoSMTP();
                                    envioCorreo.Send(currentUser.usuario, emailMessage.Subject, bodyCorreo, _configuration["Correo:email"], _configuration["Correo:contrasena"], _configuration["Correo:smtp"], _configuration["Correo:puerto"], _configuration["Correo:usuario"]);

                                }

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
                log.LogError("ActualizarTramitePasoTresController - Post", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }

        }
        #endregion
    }
}
