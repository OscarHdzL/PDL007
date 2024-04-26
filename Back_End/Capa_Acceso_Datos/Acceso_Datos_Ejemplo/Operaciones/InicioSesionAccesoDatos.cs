using Acceso_Datos.Base;
using Conexion;
using Microsoft.EntityFrameworkCore;
using Modelos.Modelos;
using Modelos.Modelos.Request;
using Modelos.Modelos.Response;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceso_Datos.Operaciones
{
    /// <summary>
    /// Clase encargado del acceso de datos para el inicio de sesion
    /// </summary>
    public class InicioSesionAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string SP_Consulta_InicioSesion = "religiosos.inicio_sesion";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public InicioSesionAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para el inicio de session
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(InicioSesionRequest entidad)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "usuariocorreo", Tipo = "String", Valor = entidad.usuarioCorreo == null ? "NULL" : entidad.usuarioCorreo.ToString() },
               new EntidadParametro { Nombre = "contrasenia", Tipo = "String",  Valor =  entidad.contrasenia == null ? "NULL" : entidad.contrasenia.ToString() },
            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado de validar las credenciales para el inicio de sesion
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<InicioSesionResponse>>> Consultar(InicioSesionRequest request)
        {
            List<InicioSesionResponse> respuesta = new List<InicioSesionResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), SP_Consulta_InicioSesion);
                            respuesta = await conexion.InicioSesionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), SP_Consulta_InicioSesion,"SELECT * FROM");
                            respuesta = await conexion.InicioSesionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<InicioSesionResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("InicioSesionAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
