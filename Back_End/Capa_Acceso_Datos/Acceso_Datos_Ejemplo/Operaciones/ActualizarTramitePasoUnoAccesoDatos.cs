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
    public class ActualizarTramitePasoUnoAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_Actualizar_usuario_sistema = "religiosos.sp_Actualizar_tramite_paso_uno";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ActualizarTramitePasoUnoAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para Actualizar usuario al sistema
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ActualizarTramitePasoUnoRequest request)
        {
            return new List<EntidadParametro>
            {
                 new EntidadParametro { Nombre = "S_ID", Tipo = "Int", Valor = request.s_id },
                 new EntidadParametro { Nombre = "S_CAT_CREDO", Tipo = "Int", Valor = request.s_cat_credo },
                 new EntidadParametro { Nombre = "S_CAT_SOLICITUD_ESCRITO", Tipo = "Int", Valor = request.s_cat_solicitud_escrito },
                 new EntidadParametro { Nombre = "S_CAT_DENOMINACION", Tipo = "String", Valor = request.s_cat_denominacion},
                 new EntidadParametro { Nombre = "S_NUMERO_REGISTRO", Tipo = "String", Valor = request.s_numero_registro },
                 new EntidadParametro { Nombre = "S_PAIS_ORIGEN", Tipo = "Int", Valor = request.s_pais_origen },
                 new EntidadParametro { Nombre = "D_TIPO_DOMICILIO", Tipo = "Int", Valor = request.s_domicilio.d_tipo_domicilio},
                 new EntidadParametro { Nombre = "D_NUMEROE", Tipo = "String", Valor = request.s_domicilio.d_numeroe  },
                 new EntidadParametro { Nombre = "D_NUMEROI", Tipo = "String", Valor = request.s_domicilio.d_numeroi  },
                 new EntidadParametro { Nombre = "D_COLONIA", Tipo = "Int", Valor = request.s_domicilio.d_colonia  },
                 new EntidadParametro { Nombre = "D_CALLE", Tipo = "String", Valor = request.s_domicilio.d_calle  },
                 new EntidadParametro { Nombre = "P_MATRIZ", Tipo = "String", Valor = request.c_matriz ?? "NULL"  },
            };
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Método encargado de ejecutar el proceso completo del registro del tutor y del alumno
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ActualizarTramitePasoUnoResponse>>> Operacion(ActualizarTramitePasoUnoRequest request)
        {
            List<ActualizarTramitePasoUnoResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_Actualizar_usuario_sistema);
                            respuesta = await conexion.ActualizarTramitePasoUnoResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_Actualizar_usuario_sistema, tipo: "SELECT * FROM");
                            respuesta = await conexion.ActualizarTramitePasoUnoResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ActualizarTramitePasoUnoResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ActualizarTramitePasoUnoAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
