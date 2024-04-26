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
    public class ActualizarTramitePasoCuatroAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_Actualizar_usuario_sistema = "religiosos.sp_actualizar_tramite_paso_Cuatro";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ActualizarTramitePasoCuatroAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para Actualizar usuario al sistema
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ActualizarTramitePasoCuatroRequest request)
        {
            return new List<EntidadParametro>
            {
                 new EntidadParametro { Nombre = "s_id", Tipo = "Int", Valor = request.s_id},
                 new EntidadParametro { Nombre = "s_superficie", Tipo = "String", Valor = request.s_superficie.ToString()},
                 new EntidadParametro { Nombre = "s_medidas", Tipo = "String", Valor = request.s_medidas.ToString()  },
                 new EntidadParametro { Nombre = "s_colindancia_text_1", Tipo = "String", Valor = request.s_colindancia_text_1 },
                 new EntidadParametro { Nombre = "s_colindancia_text_2", Tipo = "String", Valor = request.s_colindancia_text_2 },
                 new EntidadParametro { Nombre = "s_colindancia_text_3", Tipo = "String", Valor = request.s_colindancia_text_3 },
                 new EntidadParametro { Nombre = "s_colindancia_text_4", Tipo = "String", Valor = request.s_colindancia_text_4 },
                 new EntidadParametro { Nombre = "s_colindancia_usos", Tipo = "String", Valor = request.s_colindancia_usos },
                 new EntidadParametro { Nombre = "s_colindancia_num_1", Tipo = "Double", Valor = request.s_colindancia_num_1 },
                 new EntidadParametro { Nombre = "s_colindancia_num_2", Tipo = "Double", Valor = request.s_colindancia_num_2 },
                 new EntidadParametro { Nombre = "s_colindancia_num_3", Tipo = "Double", Valor = request.s_colindancia_num_3 },
                 new EntidadParametro { Nombre = "s_colindancia_num_4", Tipo = "Double", Valor = request.s_colindancia_num_4 },
                 new EntidadParametro { Nombre = "s_aviso_apertura", Tipo = "Int", Valor = request.s_aviso_apertura},

            };
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Método encargado de ejecutar el proceso completo del registro del tutor y del alumno
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ActualizarTramitePasoCuatroResponse>>> Operacion(ActualizarTramitePasoCuatroRequest request)
        {
            List<ActualizarTramitePasoCuatroResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_Actualizar_usuario_sistema);
                            respuesta = await conexion.ActualizarTramitePasoCuatroResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_Actualizar_usuario_sistema, tipo: "SELECT * FROM");
                            respuesta = await conexion.ActualizarTramitePasoCuatroResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ActualizarTramitePasoCuatroResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ActualizarTramitePasoCuatroAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
