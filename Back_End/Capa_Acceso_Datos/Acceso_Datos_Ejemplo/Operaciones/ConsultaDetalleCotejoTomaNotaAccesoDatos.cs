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
    public class ConsultaDetalleCotejoTomaNotaAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_detalle_toma_nota_cotejo = "religiosos.sp_consulta_detalle_toma_nota_cotejo";
        private const string sp_consulta_detalle_toma_nota_cotejo_publico = "religiosos.sp_consulta_detalle_toma_nota_cotejo_publico_new";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaDetalleCotejoTomaNotaAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ConsultaDetalleCotejoRequest entidad)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "cotejo_tipo", Tipo = "Int", Valor = entidad.cotejo_tipo },
               new EntidadParametro { Nombre = "s_id_us", Tipo = "Int", Valor = entidad.s_id_us },
               new EntidadParametro { Nombre = "c_id", Tipo = "Int", Valor = entidad.c_id },
            };
        }
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametrosPublico(ConsultaDetalleCotejoPublicoRequest entidad)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "s_id_us", Tipo = "Int", Valor = entidad.s_id_us },
               new EntidadParametro { Nombre = "id_tramite", Tipo = "Int", Valor = entidad.id_tramite }
            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaDetalleCotejoResponse>>> Consultar(ConsultaDetalleCotejoRequest request)
        {
            List<ConsultaDetalleCotejoResponse> respuesta = new List<ConsultaDetalleCotejoResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_consulta_detalle_toma_nota_cotejo);
                            respuesta = await conexion.ConsultaDetalleCotejoResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_consulta_detalle_toma_nota_cotejo, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaDetalleCotejoResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaDetalleCotejoResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("AccesoDatos ConsultaDetalleCotejoTomaNotaAccesoDatos", ex);
                throw;
            }
        }
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaDetalleCotejoPublicoResponse>>> Consultar(ConsultaDetalleCotejoPublicoRequest request)
        {
            List<ConsultaDetalleCotejoPublicoResponse> respuesta = new List<ConsultaDetalleCotejoPublicoResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosPublico(request), sp_consulta_detalle_toma_nota_cotejo_publico);
                            respuesta = await conexion.ConsultaDetalleCotejoPublicoResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosPublico(request), sp_consulta_detalle_toma_nota_cotejo_publico, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaDetalleCotejoPublicoResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaDetalleCotejoPublicoResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("AccesoDatos ConsultaDetalleCotejoTomaNotaAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
