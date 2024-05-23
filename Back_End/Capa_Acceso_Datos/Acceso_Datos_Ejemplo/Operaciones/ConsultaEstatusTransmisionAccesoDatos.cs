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
    public class ConsultaEstatusTransmisionAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_estatus_transmision = "religiosos.sp_consulta_estatus_transmision";
        private const string sp_consulta_estatus_transmision_filtrado = "religiosos.sp_consulta_estatus_transmision_filtrado";

        
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaEstatusTransmisionAccesoDatos() : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtenr los municipios
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ConsultaEstatusTransmisionRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "id_estatus", Tipo = "Int", Valor = request.id_estatus},
               new EntidadParametro { Nombre = "id_dictaminador", Tipo = "Int", Valor = request.id_dictaminador}
            };
        }

        private List<EntidadParametro> ObtenerParametrosFiltrado(ConsultaEstatusTransmisionFiltradoRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "id_estatus", Tipo = "Int", Valor = request.id_estatus},
               new EntidadParametro { Nombre = "id_asignador", Tipo = "Int", Valor = request.id_asignador},
               new EntidadParametro { Nombre = "id_dictaminador", Tipo = "Int", Valor = request.id_dictaminador},
               new EntidadParametro { Nombre = "busqueda", Tipo = "String", Valor = request.busqueda!=null?request.busqueda:null}

            };
        }
        #endregion

        #region Métodos Publicos

        public async Task<ResponseGeneric<List<ConsultaEstatusTransmisionResponse>>> Consultar(ConsultaEstatusTransmisionRequest request)
        {
            List<ConsultaEstatusTransmisionResponse> respuesta = new ();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(null, sp_consulta_estatus_transmision);
                            respuesta = await conexion.ConsultaEstatusTransmisionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_consulta_estatus_transmision, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaEstatusTransmisionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaEstatusTransmisionResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaEstatusTransmisionAccesoDatos - Consultar", ex);
                throw;
            }
        }

        public async Task<ResponseGeneric<List<ConsultaEstatusTransmisionResponse>>> ConsultarFiltrado(ConsultaEstatusTransmisionFiltradoRequest request)
        {
            List<ConsultaEstatusTransmisionResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(null, sp_consulta_estatus_transmision_filtrado);
                            respuesta = await conexion.ConsultaEstatusTransmisionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosFiltrado(request), sp_consulta_estatus_transmision_filtrado, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaEstatusTransmisionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaEstatusTransmisionResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaEstatusTransmisionAccesoDatos - Consultar", ex);
                throw;
            }
        }

        #endregion
    }
}
