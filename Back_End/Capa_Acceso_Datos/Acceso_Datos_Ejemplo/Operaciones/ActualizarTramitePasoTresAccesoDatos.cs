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
    public class ActualizarTramitePasoTresAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_Actualizar_usuario_sistema = "religiosos.sp_actualizar_tramite_paso_tres";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ActualizarTramitePasoTresAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para Actualizar usuario al sistema
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ActualizarTramitePasoTresRequest request)
        {
            return new List<EntidadParametro>
            {
                 new EntidadParametro { Nombre = "s_id_tramite", Tipo = "Int", Valor = request.s_id_tramite},
                 new EntidadParametro { Nombre = "s_cat_sjuridica", Tipo = "Int", Valor = request.s_cat_sjuridica},
                 new EntidadParametro { Nombre = "s_f_apertura", Tipo = "Date", Valor = DateTime.ParseExact(request.s_f_apertura,"yyyy-MM-dd",CultureInfo.InvariantCulture)},
                 //new EntidadParametro { Nombre = "D_ID_DOMICILIO", Tipo = "Int", Valor = request.s_domicilio.d_id_domicilio},
                 new EntidadParametro { Nombre = "d_tipo_domicilio", Tipo = "Int", Valor = request.s_domicilio.d_tipo_domicilio},
                 new EntidadParametro { Nombre = "d_numeroe", Tipo = "String", Valor = request.s_domicilio.d_numeroe  },
                 new EntidadParametro { Nombre = "d_numeroi", Tipo = "String", Valor = request.s_domicilio.d_numeroi  },
                 new EntidadParametro { Nombre = "d_colonia", Tipo = "Int", Valor = request.s_domicilio.d_colonia  },
                 new EntidadParametro { Nombre = "d_calle", Tipo = "String", Valor = request.s_domicilio.d_calle  },
            };
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Método encargado de ejecutar el proceso completo del registro del tutor y del alumno
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ActualizarTramitePasoTresResponse>>> Operacion(ActualizarTramitePasoTresRequest request)
        {
            List<ActualizarTramitePasoTresResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_Actualizar_usuario_sistema);
                            respuesta = await conexion.ActualizarTramitePasoTresResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_Actualizar_usuario_sistema, tipo: "SELECT * FROM");
                            respuesta = await conexion.ActualizarTramitePasoTresResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ActualizarTramitePasoTresResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ActualizarTramitePasoTresAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
