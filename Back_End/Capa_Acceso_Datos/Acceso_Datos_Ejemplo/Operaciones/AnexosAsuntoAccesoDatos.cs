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
    public class AnexosAsuntoAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones

        private const string sp_consulta_lista_anexo_asunto = "religiosos.sp_consulta_lista_anexo_asunto";
        private const string sp_consulta_detalle_anexo_asunto = "religiosos.sp_consulta_detalle_anexo_asunto";
        private const string sp_insertar_anexo_asunto = "religiosos.sp_insert_anexo_asunto";
        private const string sp_borrar_anexo_asunto = "religiosos.sp_borrar_anexo_asunto";
        private const string sp_borrar_anexo_bd = "religiosos.sp_borrar_anexo_bd";

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor inicial para el Acceso a Datos
        /// </summary>
        public AnexosAsuntoAccesoDatos() : base() { }

        #endregion

        #region Parámetros SQL

        /// <summary>
        /// Método encargado de obtener los parámetros para consultar un registro del catálogo cartografía mapas
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametrosConsultaLista(ConsultaListaAnexoAsuntoRequest request)
        {
            return new List<EntidadParametro>
            {
                new EntidadParametro { Nombre = "id_toma_nota", Tipo = "Int", Valor = request.id_toma_nota },
                new EntidadParametro { Nombre = "id_tramite", Tipo = "Int", Valor = request.id_tramite },
            };
        }

        /// <summary>
        /// Método encargado de obtener los parámetros para consultar un registro del catálogo cartografía mapas
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametrosConsultaDetalle(ConsultaDetalleAnexoAsuntoRequest request)
        {
            return new List<EntidadParametro>
            {
                new EntidadParametro { Nombre = "id_anexo", Tipo = "Int", Valor = request.id_anexo },
            };
        }

        /// <summary>
        /// Método encargado de obtener los parámetros para insertar un registro en el catálogo cartografía mapas
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametrosInsertar(InsertarAnexoRequest request)
        {
            return new List<EntidadParametro>
            {
                new EntidadParametro { Nombre = "id_toma_nota", Tipo = "Int", Valor = request.id_toma_nota },
                new EntidadParametro { Nombre = "nombre_anexo", Tipo = "String", Valor = request.nombre_anexo == null ? "NULL" : request.nombre_anexo },
                new EntidadParametro { Nombre = "url_anexo", Tipo = "String", Valor = request.url_anexo },
                new EntidadParametro { Nombre = "p_id_tramite", Tipo = "Int", Valor = request.id_tramite},
            };
        }

        /// <summary>
        /// Método encargado de obtener los parámetros para actualizar un registro del catálogo cartografía mapas
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametrosBorrar(BorrarAnexoRequest request)
        {
            return new List<EntidadParametro>
            {
                new EntidadParametro { Nombre = "id_anexo", Tipo = "Int", Valor = request.id_anexo },
            };
        }

        #endregion

        #region Métodos Públicos

        /// <summary>
        ///  Método encargado de ejecutar el proceso completo de consultar un registro del catálogo cartografía mapas
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaListaAnexoAsuntoResponse>>> ConsultarListaAnexo(ConsultaListaAnexoAsuntoRequest request)
        {
            List<ConsultaListaAnexoAsuntoResponse> respuesta = new List<ConsultaListaAnexoAsuntoResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var queryMySql = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosConsultaLista(request), sp_consulta_lista_anexo_asunto);
                            respuesta = await conexion.ConsultaListaAnexoAsuntoResponse.FromSqlRaw(queryMySql.Query, queryMySql.ListaParametros.ToArray()).ToListAsync();
                            break;
                        case 2:
                            var queryPostgrSql = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosConsultaLista(request), sp_consulta_lista_anexo_asunto, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaListaAnexoAsuntoResponse.FromSqlRaw(queryPostgrSql.Query, queryPostgrSql.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaListaAnexoAsuntoResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("AccesoDatos ConsultaListaAnexoAsuntoAccesoDatos-Consultar", ex);
                throw;
            }
        }

        /// <summary>
        ///  Método encargado de ejecutar el proceso completo de consultar un registro del catálogo cartografía mapas
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaDetalleAnexoAsuntoResponse>>> ConsultarDetalleAnexo(ConsultaDetalleAnexoAsuntoRequest request)
        {
            List<ConsultaDetalleAnexoAsuntoResponse> respuesta = new List<ConsultaDetalleAnexoAsuntoResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var queryMySql = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosConsultaDetalle(request), sp_consulta_detalle_anexo_asunto);
                            respuesta = await conexion.ConsultaDetalleAnexoAsuntoResponse.FromSqlRaw(queryMySql.Query, queryMySql.ListaParametros.ToArray()).ToListAsync();
                            break;
                        case 2:
                            var queryPostgrSql = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosConsultaDetalle(request), sp_consulta_detalle_anexo_asunto, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaDetalleAnexoAsuntoResponse.FromSqlRaw(queryPostgrSql.Query, queryPostgrSql.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaDetalleAnexoAsuntoResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("AccesoDatos ConsultaDetalleAnexoAsuntoAccesoDatos-Consultar", ex);
                throw;
            }
        }

        /// <summary>
        ///  Método encargado de ejecutar el proceso completo para insertar un registro en el catálogo cartografía mapas
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<InsertarAnexoAsuntoResponse>>> InsertarAnexoAsunto(InsertarAnexoRequest request)
        {
            List<InsertarAnexoAsuntoResponse> respuesta = new List<InsertarAnexoAsuntoResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var queryMySql = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosInsertar(request), sp_insertar_anexo_asunto);
                            respuesta = await conexion.InsertarAnexoAsuntoResponse.FromSqlRaw(queryMySql.Query, queryMySql.ListaParametros.ToArray()).ToListAsync();
                            break;
                        case 2:
                            var queryPostgrSql = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosInsertar(request), sp_insertar_anexo_asunto, tipo: "SELECT * FROM");
                            respuesta = await conexion.InsertarAnexoAsuntoResponse.FromSqlRaw(queryPostgrSql.Query, queryPostgrSql.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<InsertarAnexoAsuntoResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("AccesoDatos InsertarAnexoAsuntoAccesoDatos-Operacion", ex);
                throw;
            }
        }

        /// <summary>
        ///  Método encargado de ejecutar el proceso completo para actualizar un registro del catálogo cartografía mapas
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<BorrarAnexoAsuntoResponse>>> BorrarAnexoAsunto(BorrarAnexoRequest request)
        {
            List<BorrarAnexoAsuntoResponse> respuesta = new List<BorrarAnexoAsuntoResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var queryMySql = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosBorrar(request), sp_borrar_anexo_asunto);
                            respuesta = await conexion.BorrarAnexoAsuntoResponse.FromSqlRaw(queryMySql.Query, queryMySql.ListaParametros.ToArray()).ToListAsync();
                            break;
                        case 2:
                            var queryPostgrSql = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosBorrar(request), sp_borrar_anexo_asunto, tipo: "SELECT * FROM");
                            respuesta = await conexion.BorrarAnexoAsuntoResponse.FromSqlRaw(queryPostgrSql.Query, queryPostgrSql.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<BorrarAnexoAsuntoResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("AccesoDatos BorrarAnexoAsuntoAccesoDatos-Operacion", ex);
                throw;
            }
        }

        /// <summary>
        ///  Método encargado de ejecutar el proceso completo para actualizar un registro del catálogo cartografía mapas
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<BorrarAnexoBDResponse>>> BorrarAnexoBD(BorrarAnexoRequest request)
        {
            List<BorrarAnexoBDResponse> respuesta = new List<BorrarAnexoBDResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var queryMySql = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosBorrar(request), sp_borrar_anexo_bd);
                            respuesta = await conexion.BorrarAnexoBDResponse.FromSqlRaw(queryMySql.Query, queryMySql.ListaParametros.ToArray()).ToListAsync();
                            break;
                        case 2:
                            var queryPostgrSql = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosBorrar(request), sp_borrar_anexo_bd, tipo: "SELECT * FROM");
                            respuesta = await conexion.BorrarAnexoBDResponse.FromSqlRaw(queryPostgrSql.Query, queryPostgrSql.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<BorrarAnexoBDResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("AccesoDatos BorrarAnexoAsuntoAccesoDatos-Operacion", ex);
                throw;
            }
        }

        #endregion
    }
}
