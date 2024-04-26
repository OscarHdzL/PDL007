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
    public class ActualizarTomaNotaDomicilioLegalAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_Actualizar_usuario_sistema = "religiosos.sp_actualizar_domicilio_toma_nota";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ActualizarTomaNotaDomicilioLegalAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para Actualizar usuario al sistema
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ActualizarDomicilioRequest request)
        {
            return new List<EntidadParametro>
            {
                 new EntidadParametro { Nombre = "D_ID_DOMICILIO", Tipo = "Int", Valor = request.d_id_domicilio},
                 new EntidadParametro { Nombre = "D_TIPO_DOMICILIO", Tipo = "Int", Valor = request.d_tipo_domicilio},
                 new EntidadParametro { Nombre = "D_NUMEROE", Tipo = "String", Valor = request.d_numeroe  },
                 new EntidadParametro { Nombre = "D_NUMEROI", Tipo = "String", Valor = request.d_numeroi  },
                 new EntidadParametro { Nombre = "D_COLONIA", Tipo = "Int", Valor = request.d_colonia  },
                 new EntidadParametro { Nombre = "D_CALLE", Tipo = "String", Valor = request.d_calle  },
            };
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Método encargado de ejecutar el proceso completo del registro del tutor y del alumno
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ActualizarTomaNotaDomicilioResponse>>> Operacion(ActualizarDomicilioRequest request)
        {
            List<ActualizarTomaNotaDomicilioResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_Actualizar_usuario_sistema);
                            respuesta = await conexion.ActualizarTomaNotaDomicilioResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_Actualizar_usuario_sistema, tipo: "SELECT * FROM");
                            respuesta = await conexion.ActualizarTomaNotaDomicilioResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ActualizarTomaNotaDomicilioResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ActualizarTomaNotaDomicilioAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
