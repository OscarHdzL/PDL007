using Acceso_Datos.Base;
using Conexion;
using Microsoft.EntityFrameworkCore;
using Modelos.Modelos.Request;
using Modelos.Modelos.Response;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceso_Datos.Operaciones
{
    public class ActualizarAtenderCotejoTomaNotaAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_Actualizar_usuario_sistema = "religiosos.sp_actualizar_atender_cotejo_toma_nota";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ActualizarAtenderCotejoTomaNotaAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para Actualizar usuario al sistema
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ActualizarCotejoRequest request)
        {
            return new List<EntidadParametro>
            {
                 new EntidadParametro { Nombre = "cotejo_tipo", Tipo = "Int", Valor = request.cotejo_tipo},
                 new EntidadParametro { Nombre = "s_us_id", Tipo = "Int", Valor = request.s_us_id},
                 new EntidadParametro { Nombre = "s_id", Tipo = "Int", Valor = request.s_id},
                 new EntidadParametro { Nombre = "s_estatus", Tipo = "Int", Valor = request.s_estatus  },
                 new EntidadParametro { Nombre = "s_direccion", Tipo = "String", Valor = request.s_direccion  },
                 new EntidadParametro { Nombre = "s_fecha", Tipo = "DateTime", Valor =  string.IsNullOrEmpty(request.s_fecha) || string.IsNullOrEmpty(request.s_horario) ? DBNull.Value:DateTime.ParseExact(request.s_fecha+' '+request.s_horario,"yyyy-MM-dd HH:mm",CultureInfo.InvariantCulture)  },
                 new EntidadParametro { Nombre = "s_comentarios", Tipo = "String", Valor = request.s_comentarios  },
                 new EntidadParametro { Nombre = "s_noficio_entrada", Tipo = "String", Valor = request.s_noficio_entrada  },
                 new EntidadParametro { Nombre = "s_noficio_salida", Tipo = "String", Valor = request.s_noficio_salida  }
            };
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Método encargado de ejecutar el proceso completo del registro del tutor y del alumno
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ActualizarCotejoResponse>>> Operacion(ActualizarCotejoRequest request)
        {
            List<ActualizarCotejoResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_Actualizar_usuario_sistema);
                            respuesta = await conexion.ActualizarCotejoResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_Actualizar_usuario_sistema, tipo: "SELECT * FROM");
                            respuesta = await conexion.ActualizarCotejoResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ActualizarCotejoResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ActualizarAtenderCotejoTomaNotaAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
