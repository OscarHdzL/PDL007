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
    public class InsertarTramitePasoCincoAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_insertar_usuario_sistema = "religiosos.sp_insertar_representante";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public InsertarTramitePasoCincoAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para insertar usuario al sistema
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(InsertarTramitePasoCincoRequest request)
        {
            return new List<EntidadParametro>
            {
                
                new EntidadParametro { Nombre = "s_id", Tipo = "Int", Valor = request.s_id },
                 new EntidadParametro { Nombre = "p_id", Tipo = "Int", Valor = request.p_id },
                 new EntidadParametro { Nombre = "p_nombre", Tipo = "String", Valor = request.p_nombre },
                 new EntidadParametro { Nombre = "p_apellido_p", Tipo = "String", Valor = request.p_apaterno},
                 new EntidadParametro { Nombre = "p_apellido_m", Tipo = "String", Valor = request.p_amaterno },
                 new EntidadParametro { Nombre = "p_telefono", Tipo = "String", Valor = request.p_telefono },
                 new EntidadParametro { Nombre = "p_correo", Tipo = "String", Valor = request.p_correo},
                 new EntidadParametro { Nombre = "r_cargo", Tipo = "String", Valor = request.p_cargo  },
                 new EntidadParametro { Nombre = "r_cargo_g", Tipo = "String", Valor = request.c_organo_g  },
                 new EntidadParametro { Nombre = "t_rep_legal", Tipo = "Boolean", Valor = request.t_rep_legal  },
                 new EntidadParametro { Nombre = "t_rep_asociado", Tipo = "Boolean", Valor = request.t_rep_asociado  },
                 new EntidadParametro { Nombre = "t_ministro_culto", Tipo = "Boolean", Valor = request.t_ministro_culto  },
                 new EntidadParametro { Nombre = "t_organo_gob", Tipo = "Boolean", Valor = request.t_organo_gob  },
            };
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Método encargado de ejecutar el proceso completo del registro del tutor y del alumno
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<InsertarTramitePasoCincoResponse>>> Operacion(InsertarTramitePasoCincoRequest request)
        {
            List<InsertarTramitePasoCincoResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_insertar_usuario_sistema);
                            respuesta = await conexion.InsertarTramitePasoCincoResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_insertar_usuario_sistema, tipo: "SELECT * FROM");
                            respuesta = await conexion.InsertarTramitePasoCincoResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<InsertarTramitePasoCincoResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("InsertarTramitePasoCincoAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
