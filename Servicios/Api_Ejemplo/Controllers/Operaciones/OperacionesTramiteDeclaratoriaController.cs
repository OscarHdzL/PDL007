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
using Microsoft.Extensions.Configuration;
using Modelos.Modelos.Utilidades;
using Utilidades.EnvioCorreoElectronico;
using Utilidades.GestionCreacionDocumentos.Implementar.FabricaDocumento;
using Utilidades.GestionCreacionDocumentos.POCOs.Documentos;

namespace Asuntos_Religiosos_Api.Controllers.Operaciones
{
    [Area("Operaciones")]
    [Route("TRAMITESDGAR/Operaciones/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Authorize]
    
    public class OperacionesTramiteDeclaratoriaController : Controller
    {
        #region Propiedades
        private readonly OperacionesTramiteDeclaratoriaNegocio _negocio;
        private readonly IConfiguration _configuration;
        private Utilidades.Log4Net.LoggerManager log = new Utilidades.Log4Net.LoggerManager();
        private readonly ActualizarTramitePasoSextoNegocio _negocioGeneral;

        #endregion

        #region Constructor
        public OperacionesTramiteDeclaratoriaController(IConfiguration configuration)
        {
            _negocio = new OperacionesTramiteDeclaratoriaNegocio();
            _configuration = configuration;
            _negocioGeneral = new ActualizarTramitePasoSextoNegocio();

        }
        #endregion

        #region Métodos publicos

        [HttpPost("[action]")]
        public async Task<IActionResult> Activar([FromBody] ActivarTramiteDeclaratoria request)
        {
            try
            {
                var resultado = await _negocio.Activar(request);
                if (resultado.Status == ResponseStatus.Success)
                {
                    return Ok(resultado);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                log.LogError("OperacionesTramiteDeclaratoriaController - Activar", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            } 

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Asignar([FromBody] AsignarDeclaratoria request)
        {
            try
            {
                var resultado = await _negocio.Asignar(request);
                if (resultado.Status == ResponseStatus.Success)
                {
                    if (!string.IsNullOrEmpty(resultado.respuesta) || resultado.Response.Count > 0)
                    {
                        var usuario = await _negocioGeneral.Consulta(new ConsultaDetalleUsuarioSistemaRequest { id_usuario = request.p_id_dictaminador });


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
                                   "<div>Ha recibido una solicitud de trámite de Declaratoria de Procedencia.</div><br>" +
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

                    return Ok(resultado);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                log.LogError("OperacionesTramiteDeclaratoriaController - Asignar", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            } 

        }
        
        [HttpPost("[action]")]
        public async Task<IActionResult> PostComentarios([FromBody] ComentariosDeclaratoria request)
        {
            try
            {
                var resultado = await _negocio.PostComentarios(request);
                if (resultado.Status == ResponseStatus.Success)
                {
                    return Ok(resultado);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                log.LogError("OperacionesTramiteDeclaratoriaController - PostComentarios", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            } 

        }
        
        //[HttpPost("[action]")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GenerarOficio([FromQuery] ActualizarEstatusDeclaratoria request)
        {
            try
            {
                var resultado = await _negocio.GenerarOficio(request);
                if (resultado.Status == ResponseStatus.Success)
                {
                    var result = await _negocio.ObtenerInformacion(request.p_id_declaratoria);

                    if (result.Status == ResponseStatus.Success)
                    {
                        var miDocumento = new DocDeclaratoria(result.Response);
                        var docGenerado = await miDocumento.Documento;
                        return File(docGenerado, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", $"{Guid.NewGuid()}");
                    }
                    else
                    {
                        return NoContent();
                    }
                    //return Ok(resultado);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                log.LogError("OperacionesTramiteDeclaratoriaController - GenerarOficio", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            } 

        }
        
                
        [HttpPost("[action]")]
        public async Task<IActionResult> Finalizar([FromBody] ActualizarEstatusDeclaratoria request)
        {
            try
            {
                var resultado = await _negocio.Finalizar(request);
                if (resultado.Status == ResponseStatus.Success)
                {
                    if (!string.IsNullOrEmpty(resultado.respuesta) || resultado.Response.Count > 0)
                    {
                        var usuario = await _negocioGeneral.ConsultaUsuPerfil(new ConsultaDetalleUsuarioSistemaRequest { id_usuario = 11 });


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
                                   "<div>Ha recibido una solicitud de trámite de Declaratoria de Procedencia.</div><br>" +
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

           
                    return Ok(resultado);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                log.LogError("OperacionesTramiteDeclaratoriaController - FinalizarDeclaratoria", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            } 

        }
        
        [HttpPost("[action]")]
        public async Task<IActionResult> Autorizar([FromBody] AutorizarDeclaratoria request)
        {
            try
            {
                var resultado = await _negocio.Autorizar(request);
                if (resultado.Status == ResponseStatus.Success)
                {
                    var usuario = await _negocio.Consulta(new ConsultaDetalleUsuarioSistemaRequest
                        { id_usuario = resultado.Response[0].id_generico });

                    return Ok(resultado);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                log.LogError("OperacionesTramiteDeclaratoriaController - AutorizarDeclaratoria", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            }
        }
        
        [HttpPost("[action]")]
        public async Task<IActionResult> Concluir([FromBody] ConcluirDeclaratoria request)
        {
            try
            {
                var resultado = await _negocio.Concluir(request);
                if (resultado.Status == ResponseStatus.Success)
                {
                    var usuario = await _negocio.Consulta(new ConsultaDetalleUsuarioSistemaRequest
                        { id_usuario = resultado.Response[0].id_generico });

                    return Ok(resultado);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                log.LogError("OperacionesTramiteDeclaratoriaController - ConcluirDeclaratoria", ex);
                return BadRequest(new ResponseGeneric<string>("Error al realizar la acción, favor de revisar el log"));
            } 

        }

        #endregion
        
    }
}
