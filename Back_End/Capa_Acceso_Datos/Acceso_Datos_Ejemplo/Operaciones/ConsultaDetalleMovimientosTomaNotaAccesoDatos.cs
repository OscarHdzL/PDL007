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
    public class ConsultaDetalleMovimientosTomaNotaAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_lista_convocatorias = "religiosos.sp_consulta_detalle_movimientos_toma_nota";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaDetalleMovimientosTomaNotaAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ConsultaDetalleTomaNotaRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "i_id_c", Tipo = "Int", Valor = request.i_id_c},
            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaDetalleMovimientosTomaNotaResponse>>> Consultar(ConsultaDetalleTomaNotaRequest request)
        {
            List<ConsultaDetalleMovimientosTomaNotaResponse> respuesta = new List<ConsultaDetalleMovimientosTomaNotaResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_consulta_lista_convocatorias);
                            respuesta = await conexion.ConsultaDetalleMovimientosTomaNotaResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_consulta_lista_convocatorias, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaDetalleMovimientosTomaNotaResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaDetalleMovimientosTomaNotaResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaDetalleMovimientosTomaNotaAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
