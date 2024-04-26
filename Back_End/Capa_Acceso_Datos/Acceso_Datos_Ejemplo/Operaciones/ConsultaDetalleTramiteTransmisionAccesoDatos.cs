using Acceso_Datos.Base;
using Conexion;
using Microsoft.EntityFrameworkCore;
using Modelos.Modelos;
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
    public class ConsultaDetalleTramiteTransmisionAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_detalle_tramite_transmision = "religiosos.sp_consulta_detalle_tramite_transmision";
        private const string sp_consulta_detalle_transmision_cotejo_publico = "religiosos.sp_consulta_detalle_transmision_cotejo_publico_new";

        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaDetalleTramiteTransmisionAccesoDatos() : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ConsultaTramiteTransmisionRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "s_id_us", Tipo = "Int", Valor = request.s_id_us },
               new EntidadParametro { Nombre = "i_id_transmision", Tipo = "Int", Valor = request.i_id_transmision },
               
            };
        }

        private List<EntidadParametro> ObtenerParametrosCotejoPublico(ConsultaTramiteTransmisionRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "s_id_us", Tipo = "Int", Valor = request.s_id_us },
               new EntidadParametro { Nombre = "id_trans", Tipo = "Int", Valor = request.id_trans }
            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado sp_consulta_detalle_tramite_transmision
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaDetalleTramiteTransmisionResponse>>> Consultar(ConsultaTramiteTransmisionRequest model)
        {
            List<ConsultaDetalleTramiteTransmisionResponse> respuesta = new List<ConsultaDetalleTramiteTransmisionResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(null, sp_consulta_detalle_tramite_transmision);
                            respuesta = await conexion.ConsultaDetalleTramiteTransmisionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(model), sp_consulta_detalle_tramite_transmision, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaDetalleTramiteTransmisionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaDetalleTramiteTransmisionResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("Consultar - ConsultaTramiteTransmisionAccesoDatos", ex);
                throw;
            }
        }

        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaDetalleTransmisionCotejoPublicoResponse>>> ConsultarTrans(ConsultaTramiteTransmisionRequest request)
        {
            List<ConsultaDetalleTransmisionCotejoPublicoResponse> respuesta = new List<ConsultaDetalleTransmisionCotejoPublicoResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosCotejoPublico(request), sp_consulta_detalle_transmision_cotejo_publico);
                            respuesta = await conexion.ConsultaDetalleTransmisionCotejoPublicoResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosCotejoPublico(request), sp_consulta_detalle_transmision_cotejo_publico, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaDetalleTransmisionCotejoPublicoResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaDetalleTransmisionCotejoPublicoResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("AccesoDatos ConsultaDetalleTransmisionCotejoPublicoAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
