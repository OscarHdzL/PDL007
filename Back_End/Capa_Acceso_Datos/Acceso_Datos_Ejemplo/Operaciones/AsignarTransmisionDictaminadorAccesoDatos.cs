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
    public class AsignarTransmisionDictaminadorAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_asignar_transmision_dictaminador = "religiosos.sp_asignar_transmision_dictaminador";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public AsignarTransmisionDictaminadorAccesoDatos(): base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para insertar usuario al sistema
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(AsignarTransmisionDictaminadorRequest request)
        {
            return new List<EntidadParametro>
            {
                 new EntidadParametro { Nombre = "id_transmision", Tipo = "Int", Valor = request.id_transmision},
                 new EntidadParametro { Nombre = "id_usuario_dictaminador", Tipo = "Int", Valor = request.id_usuario_dictaminador},
                 new EntidadParametro { Nombre = "id_usuario_asignador", Tipo = "Int", Valor = request.id_usuario_asignador},
            };
        }
        #endregion

        #region Métodos
        public async Task<ResponseGeneric<List<AsignarTransmisionDictaminadorResponse>>> Operacion(AsignarTransmisionDictaminadorRequest request)
        {
            List<AsignarTransmisionDictaminadorResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                   switch (int.Parse(Configuration["TipoBase"].ToString()))
                        {
                            case 1:
                                var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_asignar_transmision_dictaminador);
                                respuesta.AddRange(await conexion.AsignarTransmisionDictaminadorResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync());
                                break;

                            case 2:
                                var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_asignar_transmision_dictaminador, tipo: "SELECT * FROM");
                                respuesta = await conexion.AsignarTransmisionDictaminadorResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                                //respuesta.AddRange(await conexion.AsignarTransmisionDictaminadorResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync());
                                break;
                        }

                }

                return new ResponseGeneric<List<AsignarTransmisionDictaminadorResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("AsignarTransmisionDictaminadorAccesoDatos - Operacion", ex);
                throw;
            }
        }
        #endregion
    }
}
