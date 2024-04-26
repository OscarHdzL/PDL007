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
    public class ReporteTransmisionAccesoDatos: BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_lista_estatus_transmision = "religiosos.sp_consulta_lista_estatus_transmision";
        private const string sp_consulta_reporte_transmisiones = "religiosos.sp_consulta_reporte_transmisiones";
        private const string sp_consulta_reporte_transmisiones2 = "religiosos.sp_consulta_reporte_transmisiones2";
        private const string sp_consulta_reporte_transmisiones3 = "religiosos.sp_consulta_reporte_transmisiones3";

        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ReporteTransmisionAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para el inicio de session
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametrosEstatusTransmision(ReporteTransmisionListaEstatusRequest entidad)
        {
           return new List<EntidadParametro>
                {
                    new EntidadParametro { Nombre = "p_estatus_id", Tipo = "Int",  Valor =  entidad.estatus_id == null ? 1 : entidad.estatus_id },
                };
        }

        private List<EntidadParametro> ObtenerParametrosReporteTransmision(ReporteTransmisionRequest entidad)
        {
            return new List<EntidadParametro>
                {
                    new EntidadParametro { Nombre = "p_medio_comunicacion", Tipo = "String",  Valor =  entidad.medio_comunicacion == null ? "NULL" : entidad.medio_comunicacion },
                    new EntidadParametro { Nombre = "p_estatus_transmision", Tipo = "Int",  Valor =  entidad.estatus_transmision == null ? "NULL" : entidad.estatus_transmision },
                    new EntidadParametro { Nombre = "p_denominacion", Tipo = "String",  Valor =  entidad.denominacion == null ? "NULL" : entidad.denominacion },
                    new EntidadParametro { Nombre = "p_acto_religioso", Tipo = "String",  Valor =  entidad.acto_religioso == null ? "NULL" : entidad.acto_religioso },
                    new EntidadParametro { Nombre = "p_fecha_inicio", Tipo = "Date",  Valor =  entidad.fecha_inicio == null ? "NULL" : entidad.fecha_inicio.Value },
                    new EntidadParametro { Nombre = "p_fecha_fin", Tipo = "Date",  Valor =  entidad.fecha_fin == null ? "NULL" : entidad.fecha_fin.Value },
                };
        }
        #endregion
        #region Métodos Publicos
        /// <summary>
        /// Método encargado de 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ReporteTransmisionListaEstatusResponse>>> ConsultarEstatusTransmision(ReporteTransmisionListaEstatusRequest request)
        {
            List<ReporteTransmisionListaEstatusResponse> respuesta = new List<ReporteTransmisionListaEstatusResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosEstatusTransmision(request), sp_consulta_lista_estatus_transmision);
                            respuesta = await conexion.ReporteTransmisionListaEstatusResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosEstatusTransmision(request), sp_consulta_lista_estatus_transmision, tipo: "SELECT * FROM");
                            respuesta = await conexion.ReporteTransmisionListaEstatusResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ReporteTransmisionListaEstatusResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("AccesoDatos ReporteTransmisionAccesoDatos ConsultarEstatusTransmision", ex);
                throw;
            }
        }

        public async Task<ResponseGeneric<List<ReporteTransmisionResponse>>> ConsultarReporteTransmision(ReporteTransmisionRequest request)
        {
            List<ReporteTransmisionResponse> respuesta = new List<ReporteTransmisionResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosReporteTransmision(request), sp_consulta_reporte_transmisiones3);
                            respuesta = await conexion.ReporteTransmisionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosReporteTransmision(request), sp_consulta_reporte_transmisiones3, tipo: "SELECT * FROM");
                            respuesta = await conexion.ReporteTransmisionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ReporteTransmisionResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("AccesoDatos ReporteTransmisionAccesoDatos ConsultarReporteTransmision", ex);
                throw;
            }
        }

        #endregion

    }
}
