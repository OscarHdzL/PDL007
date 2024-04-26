using Acceso_Datos.Base;
using Conexion;
using Microsoft.EntityFrameworkCore;
using Modelos.Modelos.Request;
using Modelos.Modelos.Response;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceso_Datos.Operaciones
{
    public class ActualizarEstatusTransmisionAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_actualizar_estatus_transmision = "religiosos.sp_actualizar_estatus_transmision";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ActualizarEstatusTransmisionAccesoDatos() : base() { }
        #endregion

        #region Parametros SQL
        private List<EntidadParametro> ObtenerParametros(ActualizarEstatusTransmisionRequest request)
        {
            return new List<EntidadParametro>
            {
                 new EntidadParametro { Nombre = "i_id_transmision", Tipo = "Int", Valor = request.i_id_transmision},
                 new EntidadParametro { Nombre = "i_id_estatus", Tipo = "Int", Valor = request.i_id_estatus}
            };
        }
        #endregion

        #region Métodos
        public async Task<ResponseGeneric<List<ActualizarEstatusTransmisionResponse>>> Operacion(ActualizarEstatusTransmisionRequest request)
        {
            List<ActualizarEstatusTransmisionResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_actualizar_estatus_transmision);
                            respuesta = await conexion.ActualizarEstatusTransmisionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_actualizar_estatus_transmision, tipo: "SELECT * FROM");
                            respuesta = await conexion.ActualizarEstatusTransmisionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ActualizarEstatusTransmisionResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ActualizarEstatusTransmisionAccesoDatos - Operacion", ex);
                throw;
            }
        }
        #endregion
    }
}
