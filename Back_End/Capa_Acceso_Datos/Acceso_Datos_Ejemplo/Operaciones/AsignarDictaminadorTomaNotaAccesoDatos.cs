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
    public class AsignarDictaminadorTomaNotaAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_insertar_usuario_sistema = "religiosos.sp_asignar_toma_nota_dictaminador";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public AsignarDictaminadorTomaNotaAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para insertar usuario al sistema
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(AsignarDictaminadorTomaNotaRequest request)
        {
            return new List<EntidadParametro>
            {


                 new EntidadParametro { Nombre = "s_id", Tipo = "Int", Valor = request.s_id},
                 new EntidadParametro { Nombre = "us_dictaminador_id", Tipo = "Int", Valor = request.us_dictaminador_id},
                 new EntidadParametro { Nombre = "us_asigna_id", Tipo = "Int", Valor = request.us_asigna_id},
            };
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Método encargado de ejecutar el proceso completo del registro del tutor y del alumno
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<AsignarDictaminadorTomaNotaResponse>>> Operacion(AsignarDictaminadorTomaNotaRequest[] request)
        {
            List<AsignarDictaminadorTomaNotaResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    foreach (var parameters in request)
                    {
                        switch (int.Parse(Configuration["TipoBase"].ToString()))
                        {
                            case 1:
                                var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(parameters), sp_insertar_usuario_sistema);
                                respuesta.AddRange(await conexion.AsignarDictaminadorTomaNotaResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync());
                                break;

                            case 2:
                                var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(parameters), sp_insertar_usuario_sistema, tipo: "SELECT * FROM");
                                respuesta.AddRange(await conexion.AsignarDictaminadorTomaNotaResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync());
                                break;
                        }
                    }

                }

                return new ResponseGeneric<List<AsignarDictaminadorTomaNotaResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("AsignarDictaminadorTomaNotaAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
