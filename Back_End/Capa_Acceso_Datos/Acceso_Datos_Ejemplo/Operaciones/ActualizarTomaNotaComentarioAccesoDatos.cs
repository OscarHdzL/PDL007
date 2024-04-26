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
    public class ActualizarTomaNotaComentarioAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_Actualizar_toma_nota_comentario = "religiosos.sp_actualizar_tomanota_comentario";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ActualizarTomaNotaComentarioAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para Actualizar usuario al sistema
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ActualizarTomaNotaComentarioRequest request)
        {
            return new List<EntidadParametro>
            {
                 new EntidadParametro { Nombre = "c_id", Tipo = "Int", Valor = request.c_id},
                 new EntidadParametro { Nombre = "c_comentario", Tipo = "String", Valor = request.c_comentario == null ? "" : request.c_comentario.ToString() },
                 new EntidadParametro { Nombre = "p_numero_sgar", Tipo = "String", Valor = request.c_numero_sgar == null ? "" : request.c_numero_sgar.ToString() },
                 new EntidadParametro { Nombre = "p_denominacion", Tipo = "String", Valor = request.c_denominacion == null ? "" : request.c_denominacion.ToString() },
            };
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Método encargado de ejecutar el proceso completo del registro del tutor y del alumno
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ActualizarTomaNotaComentarioResponse>>> Operacion(ActualizarTomaNotaComentarioRequest request)
        {
            List<ActualizarTomaNotaComentarioResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_Actualizar_toma_nota_comentario);
                            respuesta = await conexion.ActualizarTomaNotaComentarioResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_Actualizar_toma_nota_comentario, tipo: "SELECT * FROM");
                            respuesta = await conexion.ActualizarTomaNotaComentarioResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ActualizarTomaNotaComentarioResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ActualizarTomaNotaComentarioAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
