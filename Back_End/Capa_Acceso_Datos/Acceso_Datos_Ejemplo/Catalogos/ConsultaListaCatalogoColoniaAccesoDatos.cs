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

namespace Acceso_Datos.Catalogos
{
    public class ConsultaListaCatalogoColoniaAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_lista_convocatorias = "religiosos.sp_consulta_lista_catalogos_Colonia";
        private const string sp_consulta_conteo_colonia_async = "religiosos.sp_consulta_conteo_colonia_async";
        private const string sp_consulta_lista_colonias = "religiosos.sp_consulta_lista_catalogos_Colonia_async";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaListaCatalogoColoniaAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        private List<EntidadParametro> ObtenerParametros(ConsultaListaCatalogoColoniaRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "keyword", Tipo = "String", Valor = request.keyword },
            };
        }
        private List<EntidadParametro> ObtenerParametrosDT(ConsultaListaCatalogoColoniaRequest request, DtParametersrequest dt)
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
        private List<EntidadParametro> ObtenerParametrosConteo(ConsultaListaCatalogoColoniaRequest request, DtParametersrequest dt)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "keyword", Tipo = "String",  Valor = string.IsNullOrEmpty(dt.search.value) ? DBNull.Value : dt.search.value},
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
        public async Task<ResponseGeneric<List<ConsultaListaCatalogoColoniaResponse>>> Consultar(ConsultaListaCatalogoColoniaRequest request)
        {
            List<ConsultaListaCatalogoColoniaResponse> respuesta = new List<ConsultaListaCatalogoColoniaResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_consulta_lista_convocatorias);
                            respuesta = await conexion.ConsultaListaCatalogoColoniaResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_consulta_lista_convocatorias, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaListaCatalogoColoniaResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaListaCatalogoColoniaResponse>>(respuesta);
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
        public async Task<ResponseGeneric<List<ConsultaListaCatalogoColoniaResponse>>> Consultar(ConsultaListaCatalogoColoniaRequest request, DtParametersrequest dtparameters)
        {
            List<ConsultaListaCatalogoColoniaResponse> respuesta = new List<ConsultaListaCatalogoColoniaResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosDT(request,dtparameters), sp_consulta_lista_colonias);
                            respuesta = await conexion.ConsultaListaCatalogoColoniaResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosDT(request,dtparameters), sp_consulta_lista_colonias, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaListaCatalogoColoniaResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaListaCatalogoColoniaResponse>>(respuesta);
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
        public async Task<ResponseGeneric<List<ConteoColoniaResponse>>> Conteo(ConsultaListaCatalogoColoniaRequest request, DtParametersrequest parametersrequest)
        {
            List<ConteoColoniaResponse> respuesta = new List<ConteoColoniaResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosConteo(request, parametersrequest), sp_consulta_conteo_colonia_async);
                            respuesta = await conexion.ConteoColoniaResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosConteo(request,parametersrequest), sp_consulta_conteo_colonia_async, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConteoColoniaResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConteoColoniaResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaListaColoniaAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
    //public class tbl_dispositivos_acceso_datos
    //{
    //    private readonly BDParametros GeneracionParametros = new BDParametros();
    //    private readonly IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
    //    private static string StoreProcedure = "asp_crud_tbl_dispositivos ";
    //    private tbl_registro_errores _datos = new tbl_registro_errores();
    //    /// <summary>
    //    /// Obtener Parametros para enviar al procedimiento almacenado
    //    /// </summary>
    //    /// <param name="dispositivos_model_response"></param>
    //    /// <returns></returns>
    //    private List<EntidadParametro> ObtenerParametros(tbl_dispositivos_model_request entidad)
    //    {
    //        List<EntidadParametro> parametros = new List<EntidadParametro>();
    //        parametros.Add(new EntidadParametro { Nombre = "tipo", Tipo = "Int", Valor = entidad.tipoOperacion.ToString() });
    //        parametros.Add(new EntidadParametro { Nombre = "id", Tipo = "Int", Valor = entidad.tbl_dispositivos_id == null ? "NULL" : entidad.tbl_dispositivos_id.ToString() });
    //        parametros.Add(new EntidadParametro { Nombre = "nombre", Tipo = "String", Valor = entidad.tbl_dispositivos_nombre == null ? "NULL" : entidad.tbl_dispositivos_nombre.ToString() });
    //        parametros.Add(new EntidadParametro { Nombre = "direccion_mac", Tipo = "String", Valor = entidad.tbl_dispositivos_direccion_mac == null ? "NULL" : entidad.tbl_dispositivos_direccion_mac.ToString() });
    //        parametros.Add(new EntidadParametro { Nombre = "activo", Tipo = "Int", Valor = entidad.tbl_dispositivos_activo == null ? "NULL" : entidad.tbl_dispositivos_activo == false ? "0" : "1" });
    //        parametros.Add(new EntidadParametro { Nombre = "modelos_dispositivos_id", Tipo = "Int", Valor = entidad.tbl_dispositivos_modelos_dispositivos_id == null ? "NULL" : entidad.tbl_dispositivos_modelos_dispositivos_id.ToString() });
    //        parametros.Add(new EntidadParametro { Nombre = "id_organizacion", Tipo = "Int", Valor = entidad.tbl_dispositivos_id_organizacion == null ? "NULL" : entidad.tbl_dispositivos_id_organizacion.ToString() });
    //        parametros.Add(new EntidadParametro { Nombre = "firmware", Tipo = "String", Valor = entidad.tbl_dispositivos_firmware == null ? "NULL" : entidad.tbl_dispositivos_firmware.ToString() });
    //        parametros.Add(new EntidadParametro { Nombre = "ca_dispositivos_id", Tipo = "Int", Valor = entidad.tbl_dispositivos_ca_dispositivos_id == null ? "NULL" : entidad.tbl_dispositivos_ca_dispositivos_id.ToString() });
    //        parametros.Add(new EntidadParametro { Nombre = "uuid", Tipo = "String", Valor = entidad.tbl_dispositivos_uuid == null ? "NULL" : entidad.tbl_dispositivos_uuid.ToString() });
    //        parametros.Add(new EntidadParametro { Nombre = "start", Tipo = "Int", Valor = entidad.start == null ? "0" : entidad.start.ToString() });
    //        parametros.Add(new EntidadParametro { Nombre = "length", Tipo = "Int", Valor = entidad.length == null ? "10" : entidad.length.ToString() });
    //        parametros.Add(new EntidadParametro { Nombre = "order", Tipo = "String", Valor = entidad.order == null ? "NULL" : entidad.order.ToString() });
    //        parametros.Add(new EntidadParametro { Nombre = "column", Tipo = "String", Valor = entidad.column == null ? "NULL" : entidad.column.ToString() });

    //        return parametros;
    //    }
    //    /// <summary>
    //    /// Consultar informacion de tbl_dispositivos
    //    /// </summary>
    //    /// <param name="tbl_dispositivos_model_request"></param>
    //    /// <returns></returns>
    //    public ResponseGeneric<List<dispositivos_model_response>> Consultar(tbl_dispositivos_model_request model)
    //    {
    //        try
    //        {
    //            #region Parametros
    //            List<dispositivos_model_response> respuesta = new List<dispositivos_model_response>();
    //            List<EntidadParametro> parametros = this.ObtenerParametros(model);
    //            #endregion
    //            #region ConexionBD
    //            using (Contexto conexion = new Contexto())
    //            {
    //                switch (int.Parse(Configuration["TipoBase"].ToString()))
    //                {
    //                    case 1:
    //                        var resultSQL = GeneracionParametros.ParametrosSqlServer(parametros, StoreProcedure);
    //                        respuesta = conexion.Query<dispositivos_model_response>()
    //                            .FromSql<dispositivos_model_response>(resultSQL.Query, resultSQL.ListaParametros.ToArray())
    //                            .ToListAsync().Result;

    //                        break;

    //                    case 2:
    //                        var resulMySQL = GeneracionParametros.ParametrosMySQL(parametros, StoreProcedure);
    //                        respuesta = conexion.Query<dispositivos_model_response>()
    //                            .FromSql<dispositivos_model_response>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray())
    //                            .ToListAsync().Result;

    //                        break;
    //                }
    //            }
    //            #endregion

    //            return new ResponseGeneric<List<dispositivos_model_response>>(respuesta);
    //        }
    //        catch (Exception ex)
    //        {
    //            tbl_registro_errores_model_request reque = new tbl_registro_errores_model_request
    //            {
    //                tbl_registro_errores_id = null,
    //                tipoOperacion = 2,
    //                tbl_registro_errores_aplicacion = "Acceso Datos",
    //                tbl_registro_errores_clase = "tbl_dispositivos_acceso_datos",
    //                tbl_registro_errores_metodo = "Consulta",
    //                tbl_registro_errores_error = ex.Message.ToString(),
    //                tbl_registro_errores_ca_tipo_registro_id = 1
    //            };
    //            _datos.Operacion(reque);
    //            throw;
    //        }
    //    }

    //}
}
