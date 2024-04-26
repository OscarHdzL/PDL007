using Acceso_Datos.Base;
using Conexion;
using Microsoft.EntityFrameworkCore;
using Modelos.Modelos.Request;
using Modelos.Modelos.Response;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acceso_Datos.Operaciones
{
    public class ConsultaListaTramitesDictaminadorAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_conteo_colonia_async = "religiosos.sp_consulta_registros_dictaminador_conteo";
        private const string sp_consulta_lista_colonias = "religiosos.sp_consulta_lista_registros_dictaminador";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaListaTramitesDictaminadorAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        private List<EntidadParametro> ObtenerParametrosDT(ConsultaListaTramitesDictaminadorgetRequest request)
        {
            return new List<EntidadParametro>
            {
                new EntidadParametro { Nombre = "c_activo", Tipo = "Boolean", Valor = request.Activos },
                new EntidadParametro { Nombre = "c_id_us", Tipo = "Int", Valor = request.id_usuario },
            };
        }
        private List<EntidadParametro> ObtenerParametrosConteo(ConsultaListaTramitesDictaminadorRequest request)
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
        public async Task<ResponseGeneric<List<ConsultaListaTramitesResponse>>> Consultar(ConsultaListaTramitesDictaminadorgetRequest request)
        {
            List<ConsultaListaTramitesResponse> respuesta = new List<ConsultaListaTramitesResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosDT(request), sp_consulta_lista_colonias);
                            respuesta = await conexion.ConsultaListaTramitesResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosDT(request), sp_consulta_lista_colonias, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaListaTramitesResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaListaTramitesResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaListaColoniaAccesoDatos", ex);
                throw;
            }
        }
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConteoTramitesResponse>>> Conteo(ConsultaListaTramitesDictaminadorRequest request)
        {
            List<ConteoTramitesResponse> respuesta = new List<ConteoTramitesResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosConteo(request), sp_consulta_conteo_colonia_async);
                            respuesta = await conexion.ConteoTramitesResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosConteo(request), sp_consulta_conteo_colonia_async, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConteoTramitesResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConteoTramitesResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaListaColoniaAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
