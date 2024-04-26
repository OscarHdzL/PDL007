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
    public class ConsultaEstatusRegistroAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string SP_Consulta_Datos = "religiosos.sp_consulta_estatus_new";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaEstatusRegistroAccesoDatos()
            : base() { }
        #endregion

        private List<EntidadParametro> ObtenerParametros(ConsultaEstatusNewRequest entidad )
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "tipotramite", Tipo = "Int", Valor = entidad.tipotramite == null ? 0 : entidad.tipotramite.Value},
            };
        }

        #region Métodos Publicos
        /// <summary>
        /// Método encargado de consultar los estados
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaEstatusResponse>>> Consultar(ConsultaEstatusNewRequest entidad)
        {
            List<ConsultaEstatusResponse> respuesta = new List<ConsultaEstatusResponse>();
            List<EntidadParametro> parametros = new List<EntidadParametro>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(entidad), SP_Consulta_Datos);
                            respuesta = await conexion.ConsultaEstatusResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(entidad), SP_Consulta_Datos, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaEstatusResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaEstatusResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("AccesoDatos ConsultaDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
