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
    public class ConsultaDetalleTramitePasoCuatroAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_detalle_usuarios_sistema = "religiosos.sp_consulta_detalle_tramite_paso_Cuatro";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaDetalleTramitePasoCuatroAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ConsultaDetalleTramitePasoCuatroRequest entidad)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "s_us_id", Tipo = "Int", Valor = entidad.s_id_us },
               new EntidadParametro { Nombre = "i_id_c", Tipo = "Int", Valor = entidad.i_id_c },
               new EntidadParametro { Nombre = "id_dictaminador", Tipo = "Boolean", Valor = entidad.dictaminador },


            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaDetalleTramitePasoCuatroResponse>>> Consultar(ConsultaDetalleTramitePasoCuatroRequest request)
        {
            List<ConsultaDetalleTramitePasoCuatroResponse> respuesta = new List<ConsultaDetalleTramitePasoCuatroResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_consulta_detalle_usuarios_sistema);
                            respuesta = await conexion.ConsultaDetalleTramitePasoCuatroResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_consulta_detalle_usuarios_sistema, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaDetalleTramitePasoCuatroResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaDetalleTramitePasoCuatroResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("AccesoDatos ConsultaDetalleTramitePasoCuatroAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
