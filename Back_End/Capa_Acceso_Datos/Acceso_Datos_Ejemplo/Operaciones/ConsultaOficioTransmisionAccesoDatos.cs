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
    public class ConsultaOficioTransmisionAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_oficio_transmision = "religiosos.sp_consulta_oficio_transmision";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaOficioTransmisionAccesoDatos() : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ConsultaOficioTransmisionRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "i_id_transmision", Tipo = "Int", Valor = request.i_id_transmision }
            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargada de consultar sp_consulta_oficio_transmision
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaOficioTransmisionResponse>>> Consultar(ConsultaOficioTransmisionRequest model)
        {
            List<ConsultaOficioTransmisionResponse> respuesta = new List<ConsultaOficioTransmisionResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(null, sp_consulta_oficio_transmision);
                            respuesta = await conexion.ConsultaOficioTransmisionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(model), sp_consulta_oficio_transmision, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaOficioTransmisionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaOficioTransmisionResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("Consultar - ConsultaOficioTransmisionAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
