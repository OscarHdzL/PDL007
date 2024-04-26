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
    public class FinalizarTramiteAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_Actualizar_usuario_sistema = "religiosos.sp_finalizar_tramite";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public FinalizarTramiteAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para Actualizar usuario al sistema
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(FinalizarTramiteRequest request)
        {
            return new List<EntidadParametro>
            {


                 new EntidadParametro { Nombre = "s_id_us", Tipo = "Int", Valor = request.s_id_us},
                 new EntidadParametro { Nombre = "s_id", Tipo = "Int", Valor = request.s_id},

            };
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Método encargado de ejecutar el proceso completo del registro del tutor y del alumno
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<FinalizarTramiteResponse>>> Operacion(FinalizarTramiteRequest request)
        {
            List<FinalizarTramiteResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_Actualizar_usuario_sistema);
                            respuesta = await conexion.FinalizarTramiteResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_Actualizar_usuario_sistema, tipo: "SELECT * FROM");
                            respuesta = await conexion.FinalizarTramiteResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<FinalizarTramiteResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("FinalizarTramiteAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
