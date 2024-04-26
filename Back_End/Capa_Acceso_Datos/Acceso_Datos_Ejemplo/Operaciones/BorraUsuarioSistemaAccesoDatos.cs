using Acceso_Datos.Base;
using Conexion;
using Microsoft.EntityFrameworkCore;
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
    public class BorraUsuarioSistemaAccesoDatos : BaseAccesoDatos
    {

        #region SP_Operaciones
        private const string sp_borra_usuario_sistema = "religiosos.sp_borra_usuario_sistema";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public BorraUsuarioSistemaAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(BorraUsuarioSistemaRequest entidad)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "id_usuario", Tipo = "Int", Valor = entidad.id_usuario == null ? "NULL" : entidad.id_usuario.Value },
               new EntidadParametro { Nombre = "estatus", Tipo = "Int", Valor = entidad.estatus == null ? "NULL" : entidad.estatus.Value },
            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<BorraUsuarioSistemaResponse>>> Consultar(BorraUsuarioSistemaRequest request)
        {
            List<BorraUsuarioSistemaResponse> respuesta = new List<BorraUsuarioSistemaResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_borra_usuario_sistema);
                            respuesta = await conexion.BorraUsuarioSistemaResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_borra_usuario_sistema, tipo: "SELECT * FROM");
                            respuesta = await conexion.BorraUsuarioSistemaResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<BorraUsuarioSistemaResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("AccesoDatos BorraUsuarioSistemaAccesoDatos", ex);
                throw;
            }
        }
        #endregion

    }
}
