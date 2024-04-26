using Acceso_Datos.Base;
using Conexion;
using Microsoft.EntityFrameworkCore;
using Modelos.Modelos.Request;
using Modelos.Modelos.Response;
using Modelos.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceso_Datos.Operaciones
{
    /// <summary>
    /// Clase encargado del acceso de datos
    /// </summary>
    public class ActualizarTramitePasoSextoAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_Actualizar_usuario_sistema = "religiosos.sp_actualizar_tramite_paso_Sexto";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ActualizarTramitePasoSextoAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para Actualizar usuario al sistema
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ActualizarTramitePasoSextoRequest request)
        {
            return new List<EntidadParametro>
            {


                 new EntidadParametro { Nombre = "s_id", Tipo = "Int", Valor = request.s_id},
                 new EntidadParametro { Nombre = "s_cat_cnotario", Tipo = "Int", Valor = request.s_cat_notarioarr},
                 new EntidadParametro { Nombre = "s_cat_modalidad", Tipo = "Int", Valor = request.s_cat_modalidad  },

            };
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Método encargado de ejecutar el proceso completo del registro del tutor y del alumno
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ActualizarTramitePasoSextoResponse>>> Operacion(ActualizarTramitePasoSextoRequest request)
        {
            List<ActualizarTramitePasoSextoResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_Actualizar_usuario_sistema);
                            respuesta = await conexion.ActualizarTramitePasoSextoResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_Actualizar_usuario_sistema, tipo: "SELECT * FROM");
                            respuesta = await conexion.ActualizarTramitePasoSextoResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ActualizarTramitePasoSextoResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ActualizarTramitePasoSextoAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
