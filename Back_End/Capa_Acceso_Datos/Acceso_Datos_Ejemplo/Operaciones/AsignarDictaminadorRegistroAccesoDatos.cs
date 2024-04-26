using Acceso_Datos.Base;
using Conexion;
using Microsoft.EntityFrameworkCore;
using Modelos.Modelos.Request;
using Modelos.Modelos.Response;
using Modelos.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceso_Datos.Operaciones
{
    /// <summary>
    /// Clase encargado del acceso de datos
    /// </summary>
    public class AsignarDictaminadorRegistroAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_insertar_usuario_sistema = "religiosos.sp_asignar_registro_dictaminador";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public AsignarDictaminadorRegistroAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para insertar usuario al sistema
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(AsignarDictaminadorRegistroRequest request)
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
        public async Task<ResponseGeneric<List<AsignarDictaminadorRegistroResponse>>> Operacion(AsignarDictaminadorRegistroRequest[] request)
        {
            List<AsignarDictaminadorRegistroResponse> respuesta = new();
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
                                respuesta.AddRange( await conexion.AsignarDictaminadorRegistroResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync());
                                break;

                            case 2:
                                var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(parameters), sp_insertar_usuario_sistema, tipo: "SELECT * FROM");
                                respuesta.AddRange( await conexion.AsignarDictaminadorRegistroResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync());
                                break;
                        }
                    }
                    
                }

                return new ResponseGeneric<List<AsignarDictaminadorRegistroResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("AsignarDictaminadorRegistroAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
