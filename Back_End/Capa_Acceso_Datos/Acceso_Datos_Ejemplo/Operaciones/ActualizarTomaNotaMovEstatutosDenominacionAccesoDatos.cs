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
    public class ActualizarTomaNotaMovEstatutosDenominacionAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_Actualizar_usuario_sistema = "religiosos.sp_actualizar_tomanota_mov_estatutos_denominacion";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ActualizarTomaNotaMovEstatutosDenominacionAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para Actualizar usuario al sistema
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ActualizarTomaNotaEstatutosDenominacionRequest request)
        {
            return new List<EntidadParametro>
            {
                 new EntidadParametro { Nombre = "c_id", Tipo = "Int", Valor = request.c_id},
                 new EntidadParametro { Nombre = "c_comentario", Tipo = "String", Valor = request.c_comentario == null ? "" : request.c_comentario.ToString() },
                 new EntidadParametro { Nombre = "c_denominacion", Tipo = "String", Valor = request.c_denominacion == null ? "" : request.c_denominacion.ToString() },
                 new EntidadParametro { Nombre = "c_comentario_n", Tipo = "String", Valor = request.c_comentario_n == null ? "" : request.c_comentario_n.ToString() },
            };
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Método encargado de ejecutar el proceso completo del registro del tutor y del alumno
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ActualizarTomaNotaEstatutosDenominacionResponse>>> Operacion(ActualizarTomaNotaEstatutosDenominacionRequest request)
        {
            List<ActualizarTomaNotaEstatutosDenominacionResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_Actualizar_usuario_sistema);
                            respuesta = await conexion.ActualizarTomaNotaEstatutosDenominacionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_Actualizar_usuario_sistema, tipo: "SELECT * FROM");
                            respuesta = await conexion.ActualizarTomaNotaEstatutosDenominacionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ActualizarTomaNotaEstatutosDenominacionResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ActualizarTomaNotaEstatutosDenominacionAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
