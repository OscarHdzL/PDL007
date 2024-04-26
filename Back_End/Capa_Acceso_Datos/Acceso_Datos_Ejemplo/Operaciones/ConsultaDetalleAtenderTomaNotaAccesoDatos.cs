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
    public class ConsultaDetalleAtenderTomaNotaAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_detalle_usuarios_sistema = "religiosos.sp_consulta_detalle_atender_toma_nota";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaDetalleAtenderTomaNotaAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ConsultaDetalleTomaNotaRequest entidad)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "s_id_us", Tipo = "Int", Valor = entidad.s_id_us },
               new EntidadParametro { Nombre = "i_id_c", Tipo = "Int", Valor = entidad.i_id_c },
            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaDetalleTomaNotaResponse>>> Consultar(ConsultaDetalleTomaNotaRequest request)
        {
            List<ConsultaDetalleTomaNotaResponse> respuesta = new List<ConsultaDetalleTomaNotaResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_consulta_detalle_usuarios_sistema);
                            respuesta = await conexion.ConsultaDetalleTomaNotaResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_consulta_detalle_usuarios_sistema, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaDetalleTomaNotaResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaDetalleTomaNotaResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("AccesoDatos ConsultaDetalleAtenderTomaNotaAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
