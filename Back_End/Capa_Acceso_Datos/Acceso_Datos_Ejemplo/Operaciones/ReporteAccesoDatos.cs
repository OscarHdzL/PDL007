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
    public class ReporteAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string Sp_Consulta_Reporte_Contactos = "religiosos.sp_consulta_reporte_registro";
        private const string sp_consulta_reporte_toma_nota = "religiosos.sp_consulta_reporte_toma_nota";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ReporteAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para el inicio de session
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ReporteRequest entidad)
        {
            entidad.EntidadRegistro = entidad.EntidadRegistro == null ? 0 : entidad.EntidadRegistro;
            entidad.CredoRegistro = entidad.CredoRegistro == null ? 0 : entidad.CredoRegistro;
            entidad.MunicipioRegistro = entidad.MunicipioRegistro == null ? 0 : entidad.MunicipioRegistro;
            entidad.EstatusRegistro = entidad.EstatusRegistro == null ? 0 : entidad.EstatusRegistro;
            entidad.FechaI = entidad.FechaI == null ? null : entidad.FechaI;
            entidad.FechaF = entidad.FechaF == null ? null : entidad.FechaF;
            entidad.Ttramite = entidad.Ttramite == null ? 0 : entidad.Ttramite;

            return new List<EntidadParametro>
                {
                    new EntidadParametro { Nombre = "EntidadRegistro", Tipo = "Int",  Valor =  entidad.EntidadRegistro == null ? "NULL" : entidad.EntidadRegistro.Value },
                    new EntidadParametro { Nombre = "CredoRegistro", Tipo = "Int",  Valor =  entidad.CredoRegistro == null ? "NULL" : entidad.CredoRegistro.Value },
                    new EntidadParametro { Nombre = "movRealizado", Tipo = "Int",  Valor =  entidad.movRealizado == null ? "NULL" : entidad.movRealizado.Value },
                    new EntidadParametro { Nombre = "MunicipioRegistro", Tipo = "Int",  Valor =  entidad.MunicipioRegistro == null ? "NULL" : entidad.MunicipioRegistro.Value },
                    new EntidadParametro { Nombre = "EstatusRegistro", Tipo = "Int",  Valor =  entidad.EstatusRegistro == null ? "NULL" : entidad.EstatusRegistro.Value },
                    new EntidadParametro { Nombre = "FechaI", Tipo = "Date",  Valor =  entidad.FechaI == null ? "NULL" : entidad.FechaI.Value },
                    new EntidadParametro { Nombre = "FechaF", Tipo = "Date",  Valor =  entidad.FechaF == null ? "NULL" : entidad.FechaF.Value },
                    new EntidadParametro { Nombre = "Ttramite", Tipo = "Int",  Valor =  entidad.Ttramite == null ? "NULL" : entidad.Ttramite.Value }
                };
        }
        #endregion
        #region Métodos Publicos
        /// <summary>
        /// Método encargado de 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ReporteResponse>>> Consultar(ReporteRequest request)
        {
            List<ReporteResponse> respuesta = new List<ReporteResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), Sp_Consulta_Reporte_Contactos);
                            respuesta = await conexion.ReporteResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), Sp_Consulta_Reporte_Contactos, tipo: "SELECT * FROM");
                            respuesta = await conexion.ReporteResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ReporteResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("AccesoDatos ReporteContactosAccesoDatos", ex);
                throw;
            }
        }

        public async Task<ResponseGeneric<List<ReporteResponseTnota>>> ConsultarTnota(ReporteRequest request)
        {
            List<ReporteResponseTnota> respuesta = new List<ReporteResponseTnota>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_consulta_reporte_toma_nota);
                            respuesta = await conexion.ReporteResponseTnota.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_consulta_reporte_toma_nota, tipo: "SELECT * FROM");
                            respuesta = await conexion.ReporteResponseTnota.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ReporteResponseTnota>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("AccesoDatos ReporteContactosAccesoDatos", ex);
                throw;
            }
        }

        #endregion
    }
}
