using Acceso_Datos.Base;
using Conexion;
using Microsoft.EntityFrameworkCore;
using Modelos.Modelos.Response;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Modelos.Modelos.Request;

namespace Acceso_Datos.Operaciones
{
    public class ReporteTramitesAccesoDatos  : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_reporte_transmision = "religiosos.sp_reporte_transmision";
        private const string sp_reporte_declaratoria_procedencia = "religiosos.sp_reporte_declaratoria_procedencia";
        private const string sp_reporte_nota = "religiosos.sp_reporte_tnota";
        private const string sp_reporte_registro = "religiosos.sp_reporte_tregistro";
        #endregion 
        
        #region Contructor
        public ReporteTramitesAccesoDatos() : base() { }
        #endregion
        
        #region Parametros SQL
        private List<EntidadParametro> ObtenerParametros(ReporteTramitesRequest request)
        {
            return new List<EntidadParametro>
            {
                new EntidadParametro { Nombre = "p_fecha_inicio", Tipo = "String", Valor = string.IsNullOrEmpty(request.p_fecha_inicio) ? "" : request.p_fecha_inicio },
                new EntidadParametro { Nombre = "p_fecha_fin", Tipo = "String", Valor = string.IsNullOrEmpty(request.p_fecha_fin) ? "": request.p_fecha_fin },
                new EntidadParametro { Nombre = "p_id_estatus", Tipo = "Int", Valor = (request.p_id_estatus == null) ? 0: request.p_id_estatus}
            };
        }
        #endregion
        
        #region Métodos Publicos
        public async Task<ResponseGeneric<List<ReporteTramitesResponse>>> GetTransmisiones(ReporteTramitesRequest request)
        {
            List<ReporteTramitesResponse> respuesta = new List<ReporteTramitesResponse>();

            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_reporte_transmision);
                            respuesta = await conexion.ReporteTramitesResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_reporte_transmision, tipo: "SELECT * FROM");
                            respuesta = await conexion.ReporteTramitesResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ReporteTramitesResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ReporteTramitesAccesoDatos - GetTransmisiones", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ReporteTramitesResponse>>> GetDeclaratorias(ReporteTramitesRequest request)
        {
            List<ReporteTramitesResponse> respuesta = new List<ReporteTramitesResponse>();

            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_reporte_declaratoria_procedencia);
                            respuesta = await conexion.ReporteTramitesResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_reporte_declaratoria_procedencia, tipo: "SELECT * FROM");
                            respuesta = await conexion.ReporteTramitesResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ReporteTramitesResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ReporteTramitesAccesoDatos - GetDeclaratorias", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ReporteTramitesResponse>>> GetNota(ReporteTramitesRequest request)
        {
            List<ReporteTramitesResponse> respuesta = new List<ReporteTramitesResponse>();

            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_reporte_nota);
                            respuesta = await conexion.ReporteTramitesResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_reporte_nota, tipo: "SELECT * FROM");
                            respuesta = await conexion.ReporteTramitesResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ReporteTramitesResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ReporteTramitesAccesoDatos - GetNota", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ReporteTramitesResponse>>> GetRegistro(ReporteTramitesRequest request)
        {
            List<ReporteTramitesResponse> respuesta = new List<ReporteTramitesResponse>();

            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_reporte_registro);
                            respuesta = await conexion.ReporteTramitesResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_reporte_registro, tipo: "SELECT * FROM");
                            respuesta = await conexion.ReporteTramitesResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ReporteTramitesResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ReporteTramitesAccesoDatos - GetRegistro", ex);
                throw;
            }
        }
        #endregion
        
    }
}
