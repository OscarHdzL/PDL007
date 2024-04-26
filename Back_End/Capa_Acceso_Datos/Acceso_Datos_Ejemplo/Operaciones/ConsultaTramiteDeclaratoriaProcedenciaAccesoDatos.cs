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
    public class ConsultaTramiteDeclaratoriaProcedenciaAccesoDatos  : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_declaratoria_paso1 = "religiosos.sp_consulta_declaratoria_paso1";
        private const string sp_consulta_declaratoria_paso2 = "religiosos.sp_consulta_declaratoria_paso2";
        private const string sp_consulta_declaratoria_paso4 = "religiosos.sp_consulta_declaratoria_paso4";
        private const string sp_consulta_declaratoria_paso5 = "religiosos.sp_consulta_declaratoria_paso5";
        private const string sp_consulta_declaratoria_avance = "religiosos.sp_consulta_declaratoria_avance";
        private const string sp_consulta_declaratoria_lista = "religiosos.sp_consulta_declaratoria_lista";

        private const string sp_consulta_declaratoria_lista_filtrado = "religiosos.sp_consulta_declaratoria_lista_busqueda";

        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaTramiteDeclaratoriaProcedenciaAccesoDatos() : base() { }
        
        #endregion
        
        #region Métodos Publicos
 
        public async Task<ResponseGeneric<List<ConsultarTramiteDeclaratoriaPaso1>>> ConsultaPaso1(int id_declaratoria)
        {
            List<ConsultarTramiteDeclaratoriaPaso1> respuesta = new List<ConsultarTramiteDeclaratoriaPaso1>();
            
            List<EntidadParametro> parametros = new List<EntidadParametro>();
            parametros.Add(new EntidadParametro { Nombre = "p_id_declaratoria", Tipo = "Int", Valor = id_declaratoria });

            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(parametros, sp_consulta_declaratoria_paso1);
                            respuesta = await conexion.ConsultarTramiteDeclaratoriaPaso1.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(parametros, sp_consulta_declaratoria_paso1, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultarTramiteDeclaratoriaPaso1.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultarTramiteDeclaratoriaPaso1>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaTramiteDeclaratoriaProcedenciaAccesoDatos - Paso1", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ConsultarTramiteDeclaratoriaPaso2>>> ConsultaPaso2(int id_declaratoria, int tipo_domicilio)
        {
            List<ConsultarTramiteDeclaratoriaPaso2> respuesta = new List<ConsultarTramiteDeclaratoriaPaso2>();
            
            List<EntidadParametro> parametros = new List<EntidadParametro>();
            parametros.Add(new EntidadParametro { Nombre = "p_id_declaratoria", Tipo = "Int", Valor = id_declaratoria });
            parametros.Add(new EntidadParametro { Nombre = "p_tipo_domicilio", Tipo = "Int", Valor = tipo_domicilio });

            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(parametros, sp_consulta_declaratoria_paso2);
                            respuesta = await conexion.ConsultarTramiteDeclaratoriaPaso2.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(parametros, sp_consulta_declaratoria_paso2, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultarTramiteDeclaratoriaPaso2.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultarTramiteDeclaratoriaPaso2>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaTramiteDeclaratoriaProcedenciaAccesoDatos - Paso2", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ConsultarTramiteDeclaratoriaPaso4>>> ConsultaPaso4(int id_declaratoria)
        {
            List<ConsultarTramiteDeclaratoriaPaso4> respuesta = new List<ConsultarTramiteDeclaratoriaPaso4>();
            
            List<EntidadParametro> parametros = new List<EntidadParametro>();
            parametros.Add(new EntidadParametro { Nombre = "p_id_declaratoria", Tipo = "Int", Valor = id_declaratoria });

            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(parametros, sp_consulta_declaratoria_paso4);
                            respuesta = await conexion.ConsultarTramiteDeclaratoriaPaso4.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(parametros, sp_consulta_declaratoria_paso4, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultarTramiteDeclaratoriaPaso4.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultarTramiteDeclaratoriaPaso4>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaTramiteDeclaratoriaProcedenciaAccesoDatos - Paso4", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ConsultarTramiteDeclaratoriaPaso5>>> ConsultaPaso5(int id_declaratoria)
        {
            List<ConsultarTramiteDeclaratoriaPaso5> respuesta = new List<ConsultarTramiteDeclaratoriaPaso5>();
            
            List<EntidadParametro> parametros = new List<EntidadParametro>();
            parametros.Add(new EntidadParametro { Nombre = "p_id_declaratoria", Tipo = "Int", Valor = id_declaratoria });

            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(parametros, sp_consulta_declaratoria_paso5);
                            respuesta = await conexion.ConsultarTramiteDeclaratoriaPaso5.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(parametros, sp_consulta_declaratoria_paso5, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultarTramiteDeclaratoriaPaso5.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultarTramiteDeclaratoriaPaso5>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaTramiteDeclaratoriaProcedenciaAccesoDatos - Paso5", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ConsultarTramiteDeclaratoriaAvance>>> ConsultaAvance(int id_declaratoria)
        {
            List<ConsultarTramiteDeclaratoriaAvance> respuesta = new List<ConsultarTramiteDeclaratoriaAvance>();
            
            List<EntidadParametro> parametros = new List<EntidadParametro>();
            parametros.Add(new EntidadParametro { Nombre = "p_id_declaratoria", Tipo = "Int", Valor = id_declaratoria });

            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(parametros, sp_consulta_declaratoria_avance);
                            respuesta = await conexion.ConsultarTramiteDeclaratoriaAvance.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(parametros, sp_consulta_declaratoria_avance, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultarTramiteDeclaratoriaAvance.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultarTramiteDeclaratoriaAvance>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaTramiteDeclaratoriaProcedenciaAccesoDatos - ConsultaAvance", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ConsultarTramiteDeclaratoriaLista>>> ConsultaLista(int id_usuario, int id_rol)
        {
            List<ConsultarTramiteDeclaratoriaLista> respuesta = new List<ConsultarTramiteDeclaratoriaLista>();
            
            List<EntidadParametro> parametros = new List<EntidadParametro>();
            parametros.Add(new EntidadParametro { Nombre = "p_id_usuario", Tipo = "Int", Valor = id_usuario });
            parametros.Add(new EntidadParametro { Nombre = "p_id_rol", Tipo = "Int", Valor = id_rol });

            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(parametros, sp_consulta_declaratoria_lista);
                            respuesta = await conexion.ConsultarTramiteDeclaratoriaLista.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(parametros, sp_consulta_declaratoria_lista, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultarTramiteDeclaratoriaLista.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultarTramiteDeclaratoriaLista>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaTramiteDeclaratoriaProcedenciaAccesoDatos - ConsultaAvance", ex);
                throw;
            }
        }

        public async Task<ResponseGeneric<List<ConsultarTramiteDeclaratoriaLista>>> ConsultaLista(ConsultaTramiteDeclaratoriaListaFiltrosRequest request)
        {
            List<ConsultarTramiteDeclaratoriaLista> respuesta = new List<ConsultarTramiteDeclaratoriaLista>();

            List<EntidadParametro> parametros = new List<EntidadParametro>();
            parametros.Add(new EntidadParametro { Nombre = "p_id_usuario", Tipo = "Int", Valor = request.id_usuario });
            parametros.Add(new EntidadParametro { Nombre = "p_id_rol", Tipo = "Int", Valor = request.id_rol });
            parametros.Add(new EntidadParametro { Nombre = "p_denominacion", Tipo = "String", Valor = request.denominacion!=null? request.denominacion:null });
            parametros.Add(new EntidadParametro { Nombre = "p_folio", Tipo = "String", Valor = request.folio != null ? request.folio : null });
            parametros.Add(new EntidadParametro { Nombre = "p_estatus", Tipo = "Int", Valor = request.estatus != null ? request.estatus : null });
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(parametros, sp_consulta_declaratoria_lista_filtrado);
                            respuesta = await conexion.ConsultarTramiteDeclaratoriaLista.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(parametros, sp_consulta_declaratoria_lista_filtrado, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultarTramiteDeclaratoriaLista.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultarTramiteDeclaratoriaLista>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaTramiteDeclaratoriaProcedenciaAccesoDatos - ConsultaAvance", ex);
                throw;
            }
        }
        #endregion
    }
}