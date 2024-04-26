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
    public class CatalogoMediosTransmisionAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_cat_medios_transmision = "religiosos.sp_crud_cat_medios_transmision";
        private const string sp_actualiza_cat_medios_transmision = "religiosos.sp_actualizar_catalogo_medios_transmision";
        private const string sp_elimina_cat_medios_transmision = "religiosos.sp_desactivar_catalogo_medios_transmision";

        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public CatalogoMediosTransmisionAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(CatalogoMediosTransmisionResponse request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "tipo_operacion", Tipo = "Int", Valor = request.tipo_operacion },
               new EntidadParametro { Nombre = "p_id", Tipo = "Int", Valor = request.id},
               new EntidadParametro { Nombre = "p_frecuencia_canal", Tipo = "String", Valor = request.frecuencia_canal == null ? "NULL" : request.frecuencia_canal},
               new EntidadParametro { Nombre = "p_proveedor", Tipo = "String", Valor = request.proveedor  == null ? "NULL" : request.proveedor},
               new EntidadParametro { Nombre = "p_televisora_radiodifusora", Tipo = "String", Valor = request.televisora_radiodifusora  == null ? "NULL" : request.televisora_radiodifusora},
               new EntidadParametro { Nombre = "p_lugar_transmision", Tipo = "Int", Valor = request.lugar_transmision},
               new EntidadParametro { Nombre = "p_lb_lugar_transmision", Tipo = "String", Valor = request.lb_lugar_transmision  == null ? "NULL" : request.lb_lugar_transmision},
               new EntidadParametro { Nombre = "p_televisora", Tipo = "Bit", Valor = request.televisora },
               new EntidadParametro { Nombre = "p_activo", Tipo = "Bit", Valor = request.activo }
            };
        }

        private List<EntidadParametro> ObtenerParametrosModificar(ActualizarMediosTransmisionRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "p_id", Tipo = "Int", Valor = request.id},
               new EntidadParametro { Nombre = "p_frecuencia_canal", Tipo = "String", Valor = request.frecuencia_canal == null ? "NULL" : request.frecuencia_canal},
               new EntidadParametro { Nombre = "p_proveedor", Tipo = "String", Valor = request.proveedor  == null ? "NULL" : request.proveedor},
               new EntidadParametro { Nombre = "p_televisora_radiodifusora", Tipo = "String", Valor = request.televisora_radiodifusora  == null ? "NULL" : request.televisora_radiodifusora},
               new EntidadParametro { Nombre = "p_lugar_transmision", Tipo = "Int", Valor = request.lugar_transmision},
               new EntidadParametro { Nombre = "p_televisora", Tipo = "Int", Valor = request.televisora == null ? "NULL" : request.televisora }
            };
        }

        private List<EntidadParametro> ObtenerParametrosEliminar(BorraMediosTransmisionRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "p_id", Tipo = "Int", Valor = request.id},
               new EntidadParametro { Nombre = "p_activo", Tipo = "Int", Valor = request.activo }
            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<CatalogoMediosTransmisionResponse>>> Consultar(CatalogoMediosTransmisionResponse model)
        {
            List<CatalogoMediosTransmisionResponse> respuesta = new List<CatalogoMediosTransmisionResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(null, sp_consulta_cat_medios_transmision);
                            respuesta = await conexion.CatalogoMediosTransmisionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(model), sp_consulta_cat_medios_transmision, tipo: "SELECT * FROM");
                            respuesta = await conexion.CatalogoMediosTransmisionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<CatalogoMediosTransmisionResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaLista CatalogoMediosTransmision", ex);
                throw;
            }
        }

        public async Task<ResponseGeneric<List<CatalogoMediosTransmisionResponse>>> Operacion(CatalogoMediosTransmisionResponse model)
        {
            List<CatalogoMediosTransmisionResponse> respuesta = new List<CatalogoMediosTransmisionResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(model), sp_consulta_cat_medios_transmision);
                            respuesta = await conexion.CatalogoMediosTransmisionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(model), sp_consulta_cat_medios_transmision, tipo: "SELECT * FROM");
                            respuesta = await conexion.CatalogoMediosTransmisionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<CatalogoMediosTransmisionResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("InsertarConvocatoriaAccesoDatos", ex);
                throw;
            }
        }

        public async Task<ResponseGeneric<List<ActualizarMediosTransmisionResponse>>> Modificar(ActualizarMediosTransmisionRequest model)
        {
            List<ActualizarMediosTransmisionResponse> respuesta = new List<ActualizarMediosTransmisionResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosModificar(model), sp_actualiza_cat_medios_transmision);
                            respuesta = await conexion.ActualizarMediosTransmisionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosModificar(model), sp_actualiza_cat_medios_transmision, tipo: "SELECT * FROM");
                            respuesta = await conexion.ActualizarMediosTransmisionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ActualizarMediosTransmisionResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("CatalogoMediosTransmisionAccesoDatos Modificar", ex);
                throw;
            }
        }

        public async Task<ResponseGeneric<List<BorraMediosTransmisionResponse>>> Eliminar(BorraMediosTransmisionRequest model)
        {
            List<BorraMediosTransmisionResponse> respuesta = new List<BorraMediosTransmisionResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosEliminar(model), sp_elimina_cat_medios_transmision);
                            respuesta = await conexion.BorraMediosTransmisionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosEliminar(model), sp_elimina_cat_medios_transmision, tipo: "SELECT * FROM");
                            respuesta = await conexion.BorraMediosTransmisionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<BorraMediosTransmisionResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("CatalogoMediosTransmisionAccesoDatos Eliminar", ex);
                throw;
            }
        }
        #endregion


    }
}
