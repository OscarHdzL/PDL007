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
    public class InsertarTomaNotaApoderadoLegalAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_insertar_usuario_sistema = "religiosos.sp_insertar_apoderado_toma_nota";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public InsertarTomaNotaApoderadoLegalAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para insertar usuario al sistema
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(InsertarTomaNotaApoderadoLegalRequest request)
        {
            return new List<EntidadParametro>
            {

                new EntidadParametro { Nombre = "s_id", Tipo = "Int", Valor = request.s_id },
                 new EntidadParametro { Nombre = "p_id", Tipo = "Int", Valor = request.p_id },
                 new EntidadParametro { Nombre = "p_nombre", Tipo = "String", Valor = request.p_nombre },
                 new EntidadParametro { Nombre = "p_apellido_p", Tipo = "String", Valor = request.p_apaterno},
                 new EntidadParametro { Nombre = "p_apellido_m", Tipo = "String", Valor = request.p_amaterno },

                 new EntidadParametro { Nombre = "r_cat_poderes", Tipo = "Int", Valor = request.c_id_poder },
                 new EntidadParametro { Nombre = "r_cat_movimiento", Tipo = "Int", Valor = request.c_id_tipo_movimiento },
                 new EntidadParametro { Nombre = "p_nacionalidad", Tipo = "String", Valor = request.p_nacionalidad },
                 new EntidadParametro { Nombre = "p_edad", Tipo = "Int", Valor = request.p_edad },
            };
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Método encargado de ejecutar el proceso completo del registro del tutor y del alumno
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<InsertarTomaNotaApoderadoLegalResponse>>> Operacion(InsertarTomaNotaApoderadoLegalRequest request)
        {
            List<InsertarTomaNotaApoderadoLegalResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_insertar_usuario_sistema);
                            respuesta = await conexion.InsertarTomaNotaApoderadoLegalResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_insertar_usuario_sistema, tipo: "SELECT * FROM");
                            respuesta = await conexion.InsertarTomaNotaApoderadoLegalResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<InsertarTomaNotaApoderadoLegalResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("InsertarTomaNotaApoderadoLegalAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
