using Acceso_Datos.Base;
using Conexion;
using Microsoft.EntityFrameworkCore;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Modelos.Modelos.Response;

namespace Acceso_Datos.Catalogos
{
    public class CatalogosTramiteDeclaratoriaAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_cat_uso_inmueble = "religiosos.sp_consulta_cat_uso_inmueble";
        private const string sp_consulta_cat_estatus_declaratoria = "religiosos.sp_consulta_cat_estatus_declaratoria";
        private const string sp_consulta_lista_usuarios_dictaminador_declaratoria = "religiosos.sp_consulta_lista_usuarios_dictaminador_declaratoria";
        private const string sp_consulta_estatus_reporte = "religiosos.sp_consulta_estatus_reporte";
        #endregion
        
        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public CatalogosTramiteDeclaratoriaAccesoDatos()
            : base() { }
        #endregion
        
        #region Métodos Publicos

        public async Task<ResponseGeneric<List<CatalogoGenericoResponse>>> GetUsoInmueble()
        {
            List<CatalogoGenericoResponse> respuesta = new List<CatalogoGenericoResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(null, sp_consulta_cat_uso_inmueble);
                            respuesta = await conexion.CatalogoGenericoResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(null, sp_consulta_cat_uso_inmueble, tipo: "SELECT * FROM");
                            respuesta = await conexion.CatalogoGenericoResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<CatalogoGenericoResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("CatalogosTramiteDeclaratoriaAccesoDatos - GetUsoInmueble", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<CatalogoGenericoResponse>>> GetEstatus()
        {
            List<CatalogoGenericoResponse> respuesta = new List<CatalogoGenericoResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(null, sp_consulta_cat_estatus_declaratoria);
                            respuesta = await conexion.CatalogoGenericoResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(null, sp_consulta_cat_estatus_declaratoria, tipo: "SELECT * FROM");
                            respuesta = await conexion.CatalogoGenericoResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<CatalogoGenericoResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("CatalogosTramiteDeclaratoriaAccesoDatos - GetEstatus", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ConsultaListaUsuariosDictaminadorTransmisionResponse>>> GetDictaminadores()
        {
            List<ConsultaListaUsuariosDictaminadorTransmisionResponse> respuesta = new List<ConsultaListaUsuariosDictaminadorTransmisionResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(null, sp_consulta_lista_usuarios_dictaminador_declaratoria);
                            respuesta = await conexion.ConsultaListaUsuariosDictaminadorTransmisionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(null, sp_consulta_lista_usuarios_dictaminador_declaratoria, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaListaUsuariosDictaminadorTransmisionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaListaUsuariosDictaminadorTransmisionResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("CatalogosTramiteDeclaratoriaAccesoDatos - GetDictaminadores", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<CatalogoGenericoResponse>>> GetEstatusReporte(int p_tipo_tramite)
        {
            List<CatalogoGenericoResponse> respuesta = new List<CatalogoGenericoResponse>();
            
            List<EntidadParametro> parametros = new List<EntidadParametro>();
            parametros.Add(new EntidadParametro { Nombre = "p_tipo_tramite", Tipo = "Int", Valor = p_tipo_tramite });
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(parametros, sp_consulta_estatus_reporte);
                            respuesta = await conexion.CatalogoGenericoResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(parametros, sp_consulta_estatus_reporte, tipo: "SELECT * FROM");
                            respuesta = await conexion.CatalogoGenericoResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<CatalogoGenericoResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("CatalogosTramiteDeclaratoriaAccesoDatos - GetEstatusReporte", ex);
                throw;
            }
        }
        
        #endregion
    }
}
