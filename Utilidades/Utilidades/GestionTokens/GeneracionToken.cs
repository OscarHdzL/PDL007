using Microsoft.IdentityModel.Tokens;
using Modelos.Modelos.Utilidades;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Utilidades.GestionTokens
{
    public static class GeneracionToken
    {
        /// <summary>
        /// Método generico de la creacion del token
        /// </summary>
        /// <param name="genericToken">Entidad de la sesion</param>
        /// <returns></returns>
        public static string GenerarToken(GenericTokenModel genericToken)
        {
            var _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(genericToken.KeySecret));

            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            var _Header = new JwtHeader(_signingCredentials);

            var _Payload = new JwtPayload(
                    issuer: genericToken.Issuer,
                    audience: genericToken.Audience,
                    claims: genericToken.Claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddHours(8)
            );

            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                    );

            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }
    }
}
