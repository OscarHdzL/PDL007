using Acceso_Datos.Base;
using Conexion;
using Microsoft.EntityFrameworkCore;
using Modelos.Modelos;
using Modelos.Modelos.Request;
using Modelos.Modelos.Response;
using Modelos.Modelos.Utilidades.Request;
using Modelos.Modelos.Utilidades.Response;
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
    public class ConfirmarCorreoAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string SP_Consulta_InicioSesion = "religiosos.confirmar_email";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConfirmarCorreoAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para confirmar correo
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ConfirmarCorreoRequest entidad)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "id_user", Tipo = "Int", Valor = entidad.id_user_insert == null ? "NULL" : entidad.id_user_insert.Value}
            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado de validar la confirmación del correo.
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConfirmarCorreoResponse>>> Operacion(ConfirmarCorreoRequest request)
        {
            List<ConfirmarCorreoResponse> respuesta = new List<ConfirmarCorreoResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), SP_Consulta_InicioSesion);
                            respuesta = await conexion.ConfirmarCorreoResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), SP_Consulta_InicioSesion,"SELECT * FROM");
                            respuesta = await conexion.ConfirmarCorreoResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConfirmarCorreoResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConfirmarCorreoAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
