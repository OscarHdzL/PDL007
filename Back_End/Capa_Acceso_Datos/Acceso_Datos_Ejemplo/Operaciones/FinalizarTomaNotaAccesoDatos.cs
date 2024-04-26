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
    public class FinalizarTomaNotaAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_Actualizar_usuario_sistema = "religiosos.sp_finalizar_toma_nota";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public FinalizarTomaNotaAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para Actualizar usuario al sistema
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(FinalizarTomaNotaRequest request)
        {
            return new List<EntidadParametro>
            {


                 new EntidadParametro { Nombre = "i_id_trtn", Tipo = "Int", Valor = request.i_id_trtn},

                 new EntidadParametro { Nombre = "b_estatutos", Tipo = "Boolean", Valor = request.b_estatutos},
                 new EntidadParametro { Nombre = "b_denominacion", Tipo = "Boolean", Valor = request.b_denominacion},
                 new EntidadParametro { Nombre = "b_miembros", Tipo = "Boolean", Valor = request.b_miembros},
                 new EntidadParametro { Nombre = "b_representante", Tipo = "Boolean", Valor = request.b_representante},
                 new EntidadParametro { Nombre = "b_apoderado", Tipo = "Boolean", Valor = request.b_apoderado},
                 new EntidadParametro { Nombre = "b_dom_legal", Tipo = "Boolean", Valor = request.b_dom_legal},
                 new EntidadParametro { Nombre = "b_dom_notificacion", Tipo = "Boolean", Valor = request.b_dom_notificacion},
                 //new EntidadParametro { Nombre = "s_id", Tipo = "Int", Valor = request.s_id},

            };
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Método encargado de ejecutar el proceso completo del registro del tutor y del alumno
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<FinalizarTomaNotaResponse>>> Operacion(FinalizarTomaNotaRequest request)
        {
            List<FinalizarTomaNotaResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_Actualizar_usuario_sistema);
                            respuesta = await conexion.FinalizarTomaNotaResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_Actualizar_usuario_sistema, tipo: "SELECT * FROM");
                            respuesta = await conexion.FinalizarTomaNotaResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<FinalizarTomaNotaResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("FinalizarTomaNotaAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
