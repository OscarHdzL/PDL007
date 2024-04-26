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
    public class OperacionesTramiteDeclaratoriaAccesoDatos : BaseAccesoDatos
    {
        
        #region SP_Operaciones
        private const string sp_eliminar_declaratoria = "religiosos.sp_eliminar_declaratoria";
        private const string sp_asignar_dictaminador_declaratoria = "religiosos.sp_asignar_dictaminador_declaratoria";
        private const string sp_insertar_observacion_declaratoria = "religiosos.sp_insertar_observacion_declaratoria";
        private const string sp_generar_oficio_declaratoria = "religiosos.sp_generar_oficio_declaratoria";
        private const string sp_estatus_finalizar_declaratoria = "religiosos.sp_estatus_finalizar_declaratoria";
        private const string sp_estatus_aprobar_declaratoria = "religiosos.sp_estatus_aprobar_declaratoria";
        private const string sp_estatus_autorizar_declaratoria = "religiosos.sp_estatus_autorizar_declaratoria";
        private const string sp_estatus_concluir_declaratoria =  "religiosos.sp_estatus_concluir_declaratoria";
        #endregion 
        
        #region Contructor
        
        public OperacionesTramiteDeclaratoriaAccesoDatos() : base() { }
        
        #endregion
        
        #region Métodos Publicos
        public async Task<ResponseGeneric<List<ResponseGenerico>>> Activar(ActivarTramiteDeclaratoria request)
        {
            List<ResponseGenerico> respuesta = new List<ResponseGenerico>();
            
            List<EntidadParametro> parametros = new List<EntidadParametro>();
            parametros.Add(new EntidadParametro { Nombre = "p_id_declaratoria", Tipo = "Int", Valor = request.p_id_declaratoria });
            parametros.Add(new EntidadParametro { Nombre = "p_activo", Tipo = "Bit", Valor = request.p_activo });
            
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(parametros, sp_eliminar_declaratoria);
                            respuesta = await conexion.ResponseGenerico.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(parametros, sp_eliminar_declaratoria, tipo: "SELECT * FROM");
                            respuesta = await conexion.ResponseGenerico.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ResponseGenerico>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("OperacionesTramiteDeclaratoriaAccesoDatos - Activar", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ResponseGenerico>>> Finalizar(ActualizarEstatusDeclaratoria request)
        {
            List<ResponseGenerico> respuesta = new List<ResponseGenerico>();
            
            List<EntidadParametro> parametros = new List<EntidadParametro>();
            parametros.Add(new EntidadParametro { Nombre = "p_id_declaratoria", Tipo = "Int", Valor = request.p_id_declaratoria });

            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(parametros, sp_estatus_finalizar_declaratoria);
                            respuesta = await conexion.ResponseGenerico.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(parametros, sp_estatus_finalizar_declaratoria, tipo: "SELECT * FROM");
                            respuesta = await conexion.ResponseGenerico.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ResponseGenerico>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("OperacionesTramiteDeclaratoriaAccesoDatos - Finalizar", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ResponseGenerico>>> Asignar(AsignarDeclaratoria request)
        {
            List<ResponseGenerico> respuesta = new List<ResponseGenerico>();
            
            List<EntidadParametro> parametros = new List<EntidadParametro>();
            parametros.Add(new EntidadParametro { Nombre = "p_id_declaratoria", Tipo = "Int", Valor = request.p_id_declaratoria });
            parametros.Add(new EntidadParametro { Nombre = "p_id_dictaminador", Tipo = "Int", Valor = request.p_id_dictaminador });
            parametros.Add(new EntidadParametro { Nombre = "p_id_asignador", Tipo = "Int", Valor = request.p_id_asignador });
            
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(parametros, sp_asignar_dictaminador_declaratoria);
                            respuesta = await conexion.ResponseGenerico.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(parametros, sp_asignar_dictaminador_declaratoria, tipo: "SELECT * FROM");
                            respuesta = await conexion.ResponseGenerico.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ResponseGenerico>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("OperacionesTramiteDeclaratoriaAccesoDatos - Asignar", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ResponseGenerico>>> PostComentarios(ComentariosDeclaratoria request)
        {
            List<ResponseGenerico> respuesta = new List<ResponseGenerico>();
            
            List<EntidadParametro> parametros = new List<EntidadParametro>();
            parametros.Add(new EntidadParametro { Nombre = "p_id_declaratoria", Tipo = "Int", Valor = request.p_id_declaratoria });
            parametros.Add(new EntidadParametro { Nombre = "p_comentarios", Tipo = "String", Valor = request.p_comentarios });
            
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(parametros, sp_insertar_observacion_declaratoria);
                            respuesta = await conexion.ResponseGenerico.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(parametros, sp_insertar_observacion_declaratoria, tipo: "SELECT * FROM");
                            respuesta = await conexion.ResponseGenerico.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ResponseGenerico>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("OperacionesTramiteDeclaratoriaAccesoDatos - PostComentarios", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ResponseGenerico>>> GenerarOficio(ActualizarEstatusDeclaratoria request)
        {
            List<ResponseGenerico> respuesta = new List<ResponseGenerico>();
            
            List<EntidadParametro> parametros = new List<EntidadParametro>();
            parametros.Add(new EntidadParametro { Nombre = "p_id_declaratoria", Tipo = "Int", Valor = request.p_id_declaratoria });

            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(parametros, sp_generar_oficio_declaratoria);
                            respuesta = await conexion.ResponseGenerico.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(parametros, sp_generar_oficio_declaratoria, tipo: "SELECT * FROM");
                            respuesta = await conexion.ResponseGenerico.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ResponseGenerico>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("OperacionesTramiteDeclaratoriaAccesoDatos - GenerarOficio", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ResponseGenerico>>> Aprobar(ActualizarEstatusDeclaratoria request)
        {
            List<ResponseGenerico> respuesta = new List<ResponseGenerico>();
            
            List<EntidadParametro> parametros = new List<EntidadParametro>();
            parametros.Add(new EntidadParametro { Nombre = "p_id_declaratoria", Tipo = "Int", Valor = request.p_id_declaratoria });
            
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(parametros, sp_estatus_aprobar_declaratoria);
                            respuesta = await conexion.ResponseGenerico.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(parametros, sp_estatus_aprobar_declaratoria, tipo: "SELECT * FROM");
                            respuesta = await conexion.ResponseGenerico.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ResponseGenerico>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("OperacionesTramiteDeclaratoriaAccesoDatos - Aprobar", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ResponseGenerico>>> Autorizar(AutorizarDeclaratoria request)
        {
            List<ResponseGenerico> respuesta = new List<ResponseGenerico>();
            
            List<EntidadParametro> parametros = new List<EntidadParametro>();
            parametros.Add(new EntidadParametro { Nombre = "p_id_declaratoria", Tipo = "Int", Valor = request.p_id_declaratoria });
            parametros.Add(new EntidadParametro { Nombre = "p_fecha", Tipo = "String", Valor = request.p_fecha });
            parametros.Add(new EntidadParametro { Nombre = "p_horario", Tipo = "String", Valor = request.p_horario });
            parametros.Add(new EntidadParametro { Nombre = "p_direccion", Tipo = "String", Valor = request.p_direccion });

            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(parametros, sp_estatus_autorizar_declaratoria);
                            respuesta = await conexion.ResponseGenerico.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(parametros, sp_estatus_autorizar_declaratoria, tipo: "SELECT * FROM");
                            respuesta = await conexion.ResponseGenerico.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ResponseGenerico>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("OperacionesTramiteDeclaratoriaAccesoDatos - Autorizar", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ResponseGenerico>>> Concluir(ConcluirDeclaratoria request)
        {
            List<ResponseGenerico> respuesta = new List<ResponseGenerico>();
            
            List<EntidadParametro> parametros = new List<EntidadParametro>();
            parametros.Add(new EntidadParametro { Nombre = "p_id_declaratoria", Tipo = "Int", Valor = request.p_id_declaratoria });
            parametros.Add(new EntidadParametro { Nombre = "p_estatus", Tipo = "Int", Valor = request.p_estatus });
            parametros.Add(new EntidadParametro { Nombre = "p_comentarios", Tipo = "String", Valor = request.p_comentarios });

            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(parametros, sp_estatus_concluir_declaratoria);
                            respuesta = await conexion.ResponseGenerico.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(parametros, sp_estatus_concluir_declaratoria, tipo: "SELECT * FROM");
                            respuesta = await conexion.ResponseGenerico.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ResponseGenerico>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("OperacionesTramiteDeclaratoriaAccesoDatos - Concluir", ex);
                throw;
            }
        }

        #endregion
    }
}
