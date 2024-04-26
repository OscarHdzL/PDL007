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
    public class InsertarTramiteDeclaratoriaProcedenciaAccesoDatos  : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_insertar_declaratoria_paso1 = "religiosos.sp_insertar_declaratoria_paso1";
        private const string sp_insertar_declaratoria_paso2 = "religiosos.sp_insertar_declaratoria_paso2";
        private const string sp_insertar_declaratoria_paso4 = "religiosos.sp_insertar_declaratoria_paso4";
        private const string sp_insertar_declaratoria_paso5 = "religiosos.sp_insertar_declaratoria_paso5";
        #endregion 
        
        #region Contructor
        
        public InsertarTramiteDeclaratoriaProcedenciaAccesoDatos() : base() { }
        
        #endregion 
        
        #region Parametros SQL
        
        private List<EntidadParametro> ObtenerParametrosPaso1(InsertarTramiteDeclaratoriaPaso1 request)
        {
            return new List<EntidadParametro>
            {
                new EntidadParametro { Nombre = "p_id_declaratoria", Tipo = "Int", Valor = request.p_id_declaratoria },
                new EntidadParametro { Nombre = "p_nombre_completo", Tipo = "String", Valor = request.p_nombre_completo },
                new EntidadParametro { Nombre = "p_denominacion_religiosa", Tipo = "String", Valor = request.p_denominacion_religiosa },
                new EntidadParametro { Nombre = "p_numero_sgar", Tipo = "String", Valor = request.p_numero_sgar},
                new EntidadParametro { Nombre = "p_i_id_tbl_cargo", Tipo = "Int", Valor = request.p_i_id_tbl_cargo },
                new EntidadParametro { Nombre = "p_i_id_tbl_usuario", Tipo = "Int", Valor = request.p_i_id_tbl_usuario }
            };
        }
        
        private List<EntidadParametro> ObtenerParametrosPaso2(InsertarTramiteDeclaratoriaPaso2 request)
        {
            return new List<EntidadParametro>
            {
                new EntidadParametro { Nombre = "p_id_declaratoria", Tipo = "Int", Valor = request.p_id_declaratoria },
                new EntidadParametro { Nombre = "p_calle", Tipo = "String", Valor = request.p_calle },
                new EntidadParametro { Nombre = "p_numeroe", Tipo = "String", Valor = request.p_numeroe},
                new EntidadParametro { Nombre = "p_numeroi", Tipo = "String", Valor = request.p_numeroi },
                new EntidadParametro { Nombre = "p_i_id_tbl_colonia", Tipo = "Int", Valor = request.p_i_id_tbl_colonia },
                new EntidadParametro { Nombre = "p_tipo_domicilio", Tipo = "Int", Valor = request.p_tipo_domicilio },
                new EntidadParametro { Nombre = "p_lote", Tipo = "String", Valor = request.p_lote },
                new EntidadParametro { Nombre = "p_manzana", Tipo = "String", Valor = request.p_manzana },
                new EntidadParametro { Nombre = "p_super_manzana", Tipo = "String", Valor = request.p_super_manzana },
                new EntidadParametro { Nombre = "p_delegacion", Tipo = "String", Valor = request.p_delegacion },
                new EntidadParametro { Nombre = "p_sector", Tipo = "String", Valor = request.p_sector },
                new EntidadParametro { Nombre = "p_zona", Tipo = "String", Valor = request.p_zona },
                new EntidadParametro { Nombre = "p_region", Tipo = "String", Valor = request.p_region },
                new EntidadParametro { Nombre = "p_personas_aut", Tipo = "String", Valor = request.p_personas_aut },
                //new EntidadParametro { Nombre = "p_numero_telefono", Tipo = "String", Valor = request.p_numero_telefono },
                //new EntidadParametro { Nombre = "p_correo_electronico", Tipo = "String", Valor = request.p_correo_electronico }
            };
        }
        
        private List<EntidadParametro> ObtenerParametrosPaso4(InsertarTramiteDeclaratoriaPaso4 request)
        {
            return new List<EntidadParametro>
            {
                new EntidadParametro { Nombre = "p_id_declaratoria", Tipo = "Int", Valor = request.p_id_declaratoria },
                new EntidadParametro { Nombre = "p_superficie", Tipo = "String", Valor = request.p_superficie },
                new EntidadParametro { Nombre = "p_unidad", Tipo = "Int", Valor = request.p_unidad},
                new EntidadParametro { Nombre = "p_ubicacion", Tipo = "String", Valor = request.p_ubicacion },
                new EntidadParametro { Nombre = "p_culto_publico", Tipo = "Bit", Valor = request.p_culto_publico },
                new EntidadParametro { Nombre = "p_inicio_actividades", Tipo = "String", Valor = request.p_inicio_actividades },
                new EntidadParametro { Nombre = "p_uso", Tipo = "Int", Valor = request.p_uso },
                new EntidadParametro { Nombre = "p_norte", Tipo = "Double", Valor = request.p_norte },
                new EntidadParametro { Nombre = "p_noreste", Tipo = "Double", Valor = request.p_noreste },
                new EntidadParametro { Nombre = "p_noroeste", Tipo = "Double", Valor = request.p_noroeste },
                new EntidadParametro { Nombre = "p_sur", Tipo = "Double", Valor = request.p_sur },
                new EntidadParametro { Nombre = "p_sureste", Tipo = "Double", Valor = request.p_sureste },
                new EntidadParametro { Nombre = "p_suroeste", Tipo = "Double", Valor = request.p_suroeste },
                new EntidadParametro { Nombre = "p_oriente", Tipo = "Double", Valor = request.p_oriente },
                new EntidadParametro { Nombre = "p_poniente", Tipo = "Double", Valor = request.p_poniente },
                new EntidadParametro { Nombre = "p_otro", Tipo = "String", Valor = request.p_otro },
                new EntidadParametro { Nombre = "p_colindancia", Tipo = "String", Valor = request.p_colindancia },
                new EntidadParametro { Nombre = "p_descripcion_salida", Tipo = "String", Valor = request.p_descripcion_salida },
                new EntidadParametro { Nombre = "p_regular", Tipo = "Bit", Valor = request.p_regular }
            };
        }
        
        private List<EntidadParametro> ObtenerParametrosPaso5(InsertarTramiteDeclaratoriaPaso5 request)
        {
            return new List<EntidadParametro>
            {
                new EntidadParametro { Nombre = "p_id_declaratoria", Tipo = "Int", Valor = request.p_id_declaratoria },
                new EntidadParametro { Nombre = "p_manifiesto_verdad", Tipo = "Bit", Valor = request.p_manifiesto_verdad }
            };
        }
        
        #endregion
        
        #region Métodos Publicos
        
        public async Task<ResponseGeneric<List<ResponseGenerico>>> InsertarPaso1(InsertarTramiteDeclaratoriaPaso1 model)
        {
            List<ResponseGenerico> respuesta = new List<ResponseGenerico>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosPaso1(model), sp_insertar_declaratoria_paso1);
                            respuesta = await conexion.ResponseGenerico.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosPaso1(model), sp_insertar_declaratoria_paso1, tipo: "SELECT * FROM");
                            respuesta = await conexion.ResponseGenerico.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ResponseGenerico>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("InsertarTramitesDeclaratoriaProcedencia - Paso1", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ResponseGenerico>>> InsertarPaso2(InsertarTramiteDeclaratoriaPaso2 model)
        {
            List<ResponseGenerico> respuesta = new List<ResponseGenerico>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosPaso2(model), sp_insertar_declaratoria_paso2);
                            respuesta = await conexion.ResponseGenerico.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosPaso2(model), sp_insertar_declaratoria_paso2, tipo: "SELECT * FROM");
                            respuesta = await conexion.ResponseGenerico.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ResponseGenerico>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("InsertarTramitesDeclaratoriaProcedencia - Paso2", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ResponseGenerico>>> InsertarPaso4(InsertarTramiteDeclaratoriaPaso4 model)
        {
            List<ResponseGenerico> respuesta = new List<ResponseGenerico>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosPaso4(model), sp_insertar_declaratoria_paso4);
                            respuesta = await conexion.ResponseGenerico.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosPaso4(model), sp_insertar_declaratoria_paso4, tipo: "SELECT * FROM");
                            respuesta = await conexion.ResponseGenerico.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ResponseGenerico>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("InsertarTramitesDeclaratoriaProcedencia - Paso4", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ResponseGenerico>>> InsertarPaso5(InsertarTramiteDeclaratoriaPaso5 model)
        {
            List<ResponseGenerico> respuesta = new List<ResponseGenerico>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosPaso5(model), sp_insertar_declaratoria_paso5);
                            respuesta = await conexion.ResponseGenerico.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosPaso5(model), sp_insertar_declaratoria_paso5, tipo: "SELECT * FROM");
                            respuesta = await conexion.ResponseGenerico.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ResponseGenerico>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("InsertarTramitesDeclaratoriaProcedencia - Paso1", ex);
                throw;
            }
        }
        
        #endregion
        
        
        
        
    }
}