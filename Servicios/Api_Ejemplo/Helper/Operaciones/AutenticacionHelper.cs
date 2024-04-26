using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Modelos.Modelos.Enum;
using Modelos.Modelos.Response;
using Modelos.Modelos.Utilidades;
using Modelos.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Utilidades.GestionTokens;

namespace Religiosos_Api.Helper.Operaciones
{
    /// <summary>
    /// Helper encargado de la creacion de los tokens
    /// </summary>
    public static class AutenticacionHelper
    {

        /// <summary>
        /// Método encargado procesar la información de las respuesta del SP
        /// </summary>
        /// <param name="inicioSesion"></param>
        /// <returns></returns>
        public static ResponseGeneric<AutenticacionResponse> ProcesarRespuestaSesion(List<InicioSesionResponse> inicioSesion, IConfiguration configuration)
        {
            var sesionActual = inicioSesion.FirstOrDefault();
            if (sesionActual != null && sesionActual.CodigoError == (int)EnumSesion.Exitoso)
            {
                return new ResponseGeneric<AutenticacionResponse>(new AutenticacionResponse
                {
                    Token = GeneracionToken.GenerarToken(ObtenerDatosJwt(configuration, sesionActual))
                });
            }
            else
            {
                return new ResponseGeneric<AutenticacionResponse>(new AutenticacionResponse
                {
                    Mensaje = sesionActual?.CodigoError == (int)EnumSesion.CorreoNoExiste ?
                              "El correo ingresado es incorrecto, favor de validar su información" :
                             sesionActual?.CodigoError == (int)EnumSesion.Contrasenia ?
                                "La contraseña ingresada es incorrecta, favor de validar su información" :
                             sesionActual?.CodigoError == (int)EnumSesion.EstadoUsuarioNoPermitido ?
                                "El Registro no ha sido confirmado o está suspendido. " : "Aún no se encuentra registrado"
                });
            }
        }

        /// <summary>
        /// Método encargado de crear el jwt para la API
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="inicioSesion"></param>
        /// <returns></returns>
        public static GenericTokenModel ObtenerDatosJwt(IConfiguration configuration, InicioSesionResponse inicioSesion)
        {
            int rolUsuario = inicioSesion.IdPerfil ?? 0;
            return new GenericTokenModel
            {
                Issuer = configuration["JWT:Issuer"],
                Audience = configuration["JWT:Audience"],
                KeySecret = configuration["JWT:ClaveSecreta"],
                TimeExpire = int.Parse(configuration["TiempoExpiracionToken"]),
                Claims = new List<Claim>
                 {
                     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                     new Claim("UserData", JsonConvert.SerializeObject(inicioSesion)),
                     new Claim(ClaimTypes.Role, rolUsuario.ToString())
                 }
            };
        }

        /// <summary>
        /// Método encargado de obtener el token de la  web api
        /// </summary>
        /// <param name="currentUser">Instancia actual del HTTP</param>
        /// <returns></returns>
        public static string ObtenerTokenWebAPISegob(HttpContext currentUser)
        {
            string token = "";
            try
            {
                if (currentUser.User.HasClaim(claim => claim.Type == "TokenSEGOB"))
                {
                    var tokenAPI = currentUser.User.Claims.FirstOrDefault(claim => claim.Type == "TokenSEGOB").Value;
                    if (!string.IsNullOrEmpty(tokenAPI))
                        token = tokenAPI;
                }
            }
            catch (Exception)
            {
                return token;
            }

            return token;
        }

        /// <summary>
        /// Método encargado de obtener el token de la  web api
        /// </summary>
        /// <param name="currentUser">Instancia actual del HTTP</param>
        /// <returns></returns>
        public static UsuarioViewModel ObtenerInformacionUsuario(string token)
        {
            UsuarioViewModel usuario = null;
            try
            {
                var jsonTokenData = new JwtSecurityTokenHandler().ReadJwtToken(token) as JwtSecurityToken;

                var jsonUsuario = jsonTokenData.Claims.FirstOrDefault(claim => claim.Type == "UserData").Value;

                if (!string.IsNullOrEmpty(jsonUsuario))
                {
                    usuario = JsonConvert.DeserializeObject<UsuarioViewModel>(jsonUsuario);
                }
            }
            catch (Exception)
            {
                return usuario;
            }

            return usuario;
        }

        /// <summary>
        /// Método encargado de obtener el usuario actual en sesion
        /// </summary>
        /// <param name="currentUser">Instancia actual del HTTP</param>
        /// <returns></returns>
        public static UsuarioViewModel UsuarioActualEnSesion(HttpContext currentUser)
        {
            UsuarioViewModel usuario = null;
            try
            {
                var jsonTokenData = new JwtSecurityTokenHandler().ReadJwtToken(ObtenerTokenWebAPISegob(currentUser)) as JwtSecurityToken;

                var jsonUsuario = jsonTokenData.Claims.FirstOrDefault(claim => claim.Type == "UserData").Value;

                if (!string.IsNullOrEmpty(jsonUsuario))
                {
                    usuario = JsonConvert.DeserializeObject<UsuarioViewModel>(jsonUsuario);
                }
            }
            catch (Exception)
            {
                return usuario;
            }

            return usuario;
        }
    }
}
