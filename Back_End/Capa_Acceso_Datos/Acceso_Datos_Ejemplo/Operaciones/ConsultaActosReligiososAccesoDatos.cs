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
    public class ConsultaActosReligiososAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_actos_religiosos = "religiosos.sp_consulta_actos_religiosos";
        private const string sp_consulta_actos_medio_transmision = "religiosos.sp_consulta_actos_medio_transmision";
        private const string sp_consulta_actos_fechas = "religiosos.sp_consulta_actos_fechas";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaActosReligiososAccesoDatos() : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ConsultaActosReligiososRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "i_id_transmision", Tipo = "Int", Valor = request.i_id_transmision }
            };
        }

        private List<EntidadParametro> ObtenerParametrosActo(ConsultaActosReligiososRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "i_id_acto_religioso", Tipo = "Int", Valor = request.i_id_acto_religioso }
            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado religiosos.sp_consulta_actos_religiosos
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaActosReligiososResponse>>> ConsultarActos(ConsultaActosReligiososRequest model)
        {
            List<ConsultaActosReligiososResponse> respuesta = new List<ConsultaActosReligiososResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(null, sp_consulta_actos_religiosos);
                            respuesta = await conexion.ConsultaActosReligiososResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(model), sp_consulta_actos_religiosos, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaActosReligiososResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaActosReligiososResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultarActos - AccesoDatos", ex);
                throw;
            }
        }

        /// <summary>
        /// Método encargado de religiosos.sp_consulta_actos_medio_transmision
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaActosMediosTrasmisionResponse>>> ConsultarActosMediosTransmision(ConsultaActosReligiososRequest model)
        {
            List<ConsultaActosMediosTrasmisionResponse> respuesta = new List<ConsultaActosMediosTrasmisionResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(null, sp_consulta_actos_medio_transmision);
                            respuesta = await conexion.ConsultaActosMediosTrasmisionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosActo(model), sp_consulta_actos_medio_transmision, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaActosMediosTrasmisionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaActosMediosTrasmisionResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultarActosMediosTransmision - AccesoDatos", ex);
                throw;
            }
        }

        /// <summary>
        /// Método encargado de religiosos.sp_consulta_actos_fechas
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaActosFechasResponse>>> ConsultarActosFechas(ConsultaActosReligiososRequest model)
        {
            List<ConsultaActosFechasResponse> respuesta = new List<ConsultaActosFechasResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(null, sp_consulta_actos_fechas);
                            respuesta = await conexion.ConsultaActosFechasResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosActo(model), sp_consulta_actos_fechas, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaActosFechasResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaActosFechasResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultarActosFechas - AccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
