using Acceso_Datos.Base;
using Conexion;
using Microsoft.EntityFrameworkCore;
using Modelos.Modelos.Request;
using Modelos.Modelos.Response;
using Modelos.Modelos.Utilidades.Request;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceso_Datos.Operaciones
{
    public class RecuperarContrasenaAccesoDatos : BaseAccesoDatos
    {

        #region SP_Operaciones
        private const string SP_Consulta_correo = "religiosos.sp_consulta_correo";
        private const string SP_inserta_nueva_contrasena = "religiosos.sp_Insertar_Nueva_Contrasena";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public RecuperarContrasenaAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para el inicio de session
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametrosCorreo(ConsultarCorreoRequest entidad)
        {
            return new List<EntidadParametro>
                {
                    new EntidadParametro { Nombre = "Correo", Tipo = "String", Valor = entidad.Correo == null ? "NULL" : entidad.Correo.ToString() },
                };

        }
        private List<EntidadParametro> ObtenerParametrosContra(RecuperarContrasenaRequest entidad, int? UsuarioId)
        {

            return new List<EntidadParametro>
                {
                    //new EntidadParametro { Nombre = "UsuarioId", Tipo = "Int",  Valor =  UsuarioId == null ? "NULL" : UsuarioId.Value },
                    new EntidadParametro { Nombre = "UsuarioId", Tipo = "Int",  Valor =  UsuarioId == null ? "NULL" : UsuarioId.Value },
                    new EntidadParametro { Nombre = "ContraUsuario", Tipo = "String",  Valor =  entidad.ContraUsuario == null ? "NULL" : entidad.ContraUsuario.ToString() },
                };

        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado de validar las credenciales para el inicio de sesion
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<RecuperarContrasenaResponse>>> Consultar(ConsultarCorreoRequest request)
        {
            List<RecuperarContrasenaResponse> respuesta = new List<RecuperarContrasenaResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosCorreo(request), SP_Consulta_correo);
                            respuesta = await conexion.RecuperarContrasenaResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosCorreo(request), SP_Consulta_correo, tipo: "SELECT * FROM");
                            respuesta = await conexion.RecuperarContrasenaResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<RecuperarContrasenaResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("RecuperarContrasenaAccesoDatos", ex);
                throw;
            }
        }

        public async Task<ResponseGeneric<List<RecuperarContrasenaResponse>>> Operacion(RecuperarContrasenaRequest request, int? UsuarioId)
        {
            List<RecuperarContrasenaResponse> respuesta = new List<RecuperarContrasenaResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosContra(request, UsuarioId), SP_inserta_nueva_contrasena);
                            respuesta = await conexion.RecuperarContrasenaResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosContra(request, UsuarioId), SP_inserta_nueva_contrasena, tipo: "SELECT * FROM");
                            respuesta = await conexion.RecuperarContrasenaResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<RecuperarContrasenaResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("RecuperarContrasenaAccesoDatos", ex);
                throw;
            }
        }

        #endregion

    }
}
