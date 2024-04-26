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
    public class ConsultaDetalleTomaNotaRepresentanteLegalAccesoDatos : BaseAccesoDatos 
    {
        #region SP_Operaciones
        private const string sp_consulta_detalle_usuarios_sistema = "religiosos.sp_consulta_detalle_toma_nota_representante_legal";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaDetalleTomaNotaRepresentanteLegalAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ConsultaDetalleTomaNotaRepresentantesRequest entidad)
        {
            return new List<EntidadParametro>
            {
               //new EntidadParametro { Nombre = "s_id_us", Tipo = "Int", Valor = entidad.s_id_us },
               new EntidadParametro { Nombre = "i_id_c", Tipo = "Int", Valor = entidad.i_id_c },
               //new EntidadParametro { Nombre = "is_dictaminador", Tipo = "Boolean", Valor = entidad.dictaminador },

            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaDetalleTomaNotaRepresentanteLegalResponse>>> Consultar(ConsultaDetalleTomaNotaRepresentantesRequest request)
        {
            List<ConsultaDetalleTomaNotaRepresentanteLegalResponse> respuesta = new List<ConsultaDetalleTomaNotaRepresentanteLegalResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_consulta_detalle_usuarios_sistema);
                            respuesta = await conexion.ConsultaDetalleTomaNotaRepresentanteLegalResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_consulta_detalle_usuarios_sistema, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaDetalleTomaNotaRepresentanteLegalResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaDetalleTomaNotaRepresentanteLegalResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("AccesoDatos ConsultaDetalleTomaNotaRepresentanteLegalAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
