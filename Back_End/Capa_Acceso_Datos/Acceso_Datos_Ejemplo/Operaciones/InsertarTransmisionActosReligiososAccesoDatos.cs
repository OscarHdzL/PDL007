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


namespace Acceso_Datos.Operaciones
{
    public class InsertarTransmisionActosReligiososAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_insertar_actos_religiosos = "religiosos.sp_insertar_actos_religiosos";
        private const string sp_insertar_actos_fechas = "religiosos.sp_insertar_actos_fechas";
        private const string sp_insertar_actos_emisoras = "religiosos.sp_insertar_actos_emisoras";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public InsertarTransmisionActosReligiososAccesoDatos() : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(InsertarTransmisionActosReligiososRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "i_id_tbl_transmision", Tipo = "Int", Valor = request.i_id_tbl_transmision },
               new EntidadParametro { Nombre = "i_id_acto", Tipo = "Int", Valor = request.i_id_acto },
               new EntidadParametro { Nombre = "c_nombre", Tipo = "String", Valor = request.c_nombre == null ? "NULL" : request.c_nombre }
            };
        }

        private List<EntidadParametro> ObtenerParametrosFechas(InsertarActosFechasRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "i_id_tbl_acto_religioso", Tipo = "Int", Valor = request.i_id_tbl_acto_religioso },
               new EntidadParametro { Nombre = "c_fecha_inicio", Tipo = "String", Valor = request.c_fecha_inicio  == null ? "NULL" : request.c_fecha_inicio},
               new EntidadParametro { Nombre = "c_fecha_fin", Tipo = "String", Valor = request.c_fecha_fin  == null ? "NULL" : request.c_fecha_fin },
               new EntidadParametro { Nombre = "c_hora_inicio", Tipo = "String", Valor = request.c_hora_inicio  == null ? "NULL" : request.c_hora_inicio },
               new EntidadParametro { Nombre = "c_hora_fin", Tipo = "String", Valor = request.c_hora_fin  == null ? "NULL" : request.c_hora_fin },
               new EntidadParametro { Nombre = "i_id_periodo", Tipo = "Int", Valor = request.i_id_cat_periodo },
               new EntidadParametro { Nombre = "cat_dia", Tipo = "String", Valor = request.cat_dia  == null ? "NULL" : request.cat_dia },
               new EntidadParametro { Nombre = "cat_mes", Tipo = "String", Valor = request.cat_mes  == null ? "NULL" : request.cat_mes },
               new EntidadParametro { Nombre = "cat_anio", Tipo = "String", Valor = request.cat_anio  == null ? "NULL" : request.cat_anio }
            };
        }

        private List<EntidadParametro> ObtenerParametrosEmisoras(InsertarActosEmisorasRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "frecuencia_canal", Tipo = "String", Valor = request.frecuencia_canal  == null ? "NULL" : request.frecuencia_canal},
               new EntidadParametro { Nombre = "proveedor", Tipo = "String", Valor = request.proveedor  == null ? "NULL" : request.proveedor},
               new EntidadParametro { Nombre = "televisora_radiodifusora", Tipo = "String", Valor = request.televisora_radiodifusora  == null ? "NULL" : request.televisora_radiodifusora},
               new EntidadParametro { Nombre = "i_id_estado", Tipo = "Int", Valor = request.i_id_estado},
               new EntidadParametro { Nombre = "televisora", Tipo = "Bit", Valor = request.televisora},
               new EntidadParametro { Nombre = "i_id_acto", Tipo = "Int", Valor = request.i_id_acto},
               new EntidadParametro { Nombre = "municipio", Tipo = "String", Valor = request.municipio ?? "NULL"}
              
            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<InsertarTransmisionActosReligiososResponse>>> Operacion(InsertarTransmisionActosReligiososRequest model)
        {
            List<InsertarTransmisionActosReligiososResponse> respuesta = new List<InsertarTransmisionActosReligiososResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(model), sp_insertar_actos_religiosos);
                            respuesta = await conexion.InsertarTransmisionActosReligiososResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(model), sp_insertar_actos_religiosos, tipo: "SELECT * FROM");
                            respuesta = await conexion.InsertarTransmisionActosReligiososResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<InsertarTransmisionActosReligiososResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("InsertarTransmisionActosReligiososAccesoDatos", ex);
                throw;
            }
        }

        public Response GuardarFechas(InsertarActosFechasRequest model)
        {
            Response response = new Response(ResponseStatus.Failed, null, null);
            List<InsertarActosFechasResponse> respuesta = new List<InsertarActosFechasResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosFechas(model), sp_insertar_actos_fechas);
                            respuesta = conexion.InsertarActosFechasResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToList();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosFechas(model), sp_insertar_actos_fechas, tipo: "SELECT * FROM");
                            respuesta = conexion.InsertarActosFechasResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToList();
                            break;
                    }
                }

                return new ResponseGeneric<List<InsertarActosFechasResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("InsertarTransmisionActosFechasAccesoDatos", ex);
                throw;
            }
        }

        public Response GuardarEmisoras(InsertarActosEmisorasRequest model)
        {
            Response response = new Response(ResponseStatus.Failed, null, null);
            List<InsertarActosEmisorasResponse> respuesta = new List<InsertarActosEmisorasResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosEmisoras(model), sp_insertar_actos_emisoras);
                            respuesta = conexion.InsertarActosEmisorasResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToList();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosEmisoras(model), sp_insertar_actos_emisoras, tipo: "SELECT * FROM");
                            respuesta = conexion.InsertarActosEmisorasResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToList();
                            break;
                    }
                }

                return new ResponseGeneric<List<InsertarActosEmisorasResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("InsertarTransmisionEmisorasAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
