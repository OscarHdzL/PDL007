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
    public class ConsultaDetalleTomaNotaAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_detalle_toma_nota = "religiosos.sp_consulta_detalle_toma_nota_new";
        private const string sp_actualizar_movimientos = "religiosos.sp_actualizar_movimientos";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaDetalleTomaNotaAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ConsultaDetalleTomaNotaRequest entidad)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "s_id_us", Tipo = "Int", Valor = entidad.s_id_us },
               new EntidadParametro { Nombre = "id_tramite", Tipo = "Int", Valor = entidad.id_tramite },
               new EntidadParametro { Nombre = "i_id_c", Tipo = "Int", Valor = entidad.i_id_c },
            };
        }

        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametrosMovimientos(RequestParamMovimientos entidad)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = nameof(entidad.p_id_tramite), Tipo = "Int", Valor = entidad.p_id_tramite ?? 0 },
               new EntidadParametro { Nombre = nameof(entidad.estatutos), Tipo = "Bit", Valor = entidad.estatutos },
               new EntidadParametro { Nombre = nameof(entidad.denominacion), Tipo = "Bit", Valor = entidad.denominacion },
               new EntidadParametro { Nombre = nameof(entidad.rep_legal), Tipo = "Bit", Valor = entidad.rep_legal },
               new EntidadParametro { Nombre = nameof(entidad.apoderado), Tipo = "Bit", Valor = entidad.apoderado },
               new EntidadParametro { Nombre = nameof(entidad.dom_notificaciones), Tipo = "Bit", Valor = entidad.dom_notificaciones },
               new EntidadParametro { Nombre = nameof(entidad.dom_legal), Tipo = "Bit", Valor = entidad.dom_legal },
            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaDetalleTomaNotaInfoResponse>>> Consultar(ConsultaDetalleTomaNotaRequest request)
        {
            List<ConsultaDetalleTomaNotaInfoResponse> respuesta = new List<ConsultaDetalleTomaNotaInfoResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_consulta_detalle_toma_nota);
                            respuesta = await conexion.ConsultaDetalleTomaNotaInfoResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_consulta_detalle_toma_nota, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaDetalleTomaNotaInfoResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaDetalleTomaNotaInfoResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("AccesoDatos ConsultaDetalleTomaNotaAccesoDatos", ex);
                throw;
            }
        }


        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ResponseGenerico>>> OperacionMovimientos(RequestParamMovimientos request)
        {
            List<ResponseGenerico> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosMovimientos(request), sp_actualizar_movimientos);
                            respuesta = await conexion.ResponseGenerico.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosMovimientos(request), sp_actualizar_movimientos, tipo: "SELECT * FROM");
                            respuesta = await conexion.ResponseGenerico.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ResponseGenerico>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("AccesoDatos ConsultaDetalleTomaNotaAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
