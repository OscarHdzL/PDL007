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
    public class ConsultaListaTomaNotaDictaminadorAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_conteo_colonia_async = "religiosos.sp_consulta_toma_nota_dictaminador_conteo";
        private const string sp_consulta_lista_colonias = "religiosos.sp_consulta_lista_toma_nota_dictaminador";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaListaTomaNotaDictaminadorAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        private List<EntidadParametro> ObtenerParametrosDT(ConsultaListaTomaNotaDictaminadorRequest request, DtParametersrequest dt)
        {
            return new List<EntidadParametro>
            {

                 new EntidadParametro { Nombre = "keyword", Tipo = "String", Valor = string.IsNullOrEmpty(dt.search.value) ? DBNull.Value : dt.search.value},
               new EntidadParametro { Nombre = "c_id_n", Tipo = "Int", Valor = request.c_id },
               new EntidadParametro { Nombre = "c_activo", Tipo = "Boolean", Valor = request.c_activo },
               new EntidadParametro { Nombre = "d_column", Tipo = "String", Valor = dt.columns[(int)dt.order[0].column].data },
               new EntidadParametro { Nombre = "d_order", Tipo = "String", Valor =  dt.order[0].dir },
               new EntidadParametro { Nombre = "d_start", Tipo = "Int", Valor = dt.start },
               new EntidadParametro { Nombre = "d_length", Tipo = "Int", Valor = dt.length },
            };
        }
        private List<EntidadParametro> ObtenerParametrosConteo(ConsultaListaTomaNotaDictaminadorRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "keyword", Tipo = "String", Valor = request.keyword },
               new EntidadParametro { Nombre = "c_id_n", Tipo = "Int", Valor = request.c_id },
               new EntidadParametro { Nombre = "c_activo", Tipo = "Boolean", Valor = request.c_activo },
            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaListaTomaNotaResponse>>> Consultar(ConsultaListaTomaNotaDictaminadorRequest request, DtParametersrequest dtparameters)
        {
            List<ConsultaListaTomaNotaResponse> respuesta = new List<ConsultaListaTomaNotaResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosDT(request, dtparameters), sp_consulta_lista_colonias);
                            respuesta = await conexion.ConsultaListaTomaNotaResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosDT(request, dtparameters), sp_consulta_lista_colonias, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaListaTomaNotaResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaListaTomaNotaResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaListaTomaNotaDictaminadorAccesoDatos", ex);
                throw;
            }
        }
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConteoTomaNotaResponse>>> Conteo(ConsultaListaTomaNotaDictaminadorRequest request)
        {
            List<ConteoTomaNotaResponse> respuesta = new List<ConteoTomaNotaResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosConteo(request), sp_consulta_conteo_colonia_async);
                            respuesta = await conexion.ConteoTomaNotaResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosConteo(request), sp_consulta_conteo_colonia_async, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConteoTomaNotaResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConteoTomaNotaResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaListaTomaNotaDictaminadorAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
