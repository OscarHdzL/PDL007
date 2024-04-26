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
    public class InsertarObservacionTransmisionAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_insertar_observacion_transmision = "religiosos.sp_insertar_observacion_transmision";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public InsertarObservacionTransmisionAccesoDatos() : base() { }
        #endregion

        #region Parametros SQL
        private List<EntidadParametro> ObtenerParametros(InsertarObservacionTransmisionRequest request)
        {
            return new List<EntidadParametro>
            {
                 new EntidadParametro { Nombre = "p_id_transmision", Tipo = "Int", Valor = request.id_transmision},
                 new EntidadParametro { Nombre = "p_observacion", Tipo = "String", Valor = request.observacion},
                 new EntidadParametro { Nombre = "p_id_estatus", Tipo = "Int", Valor = request.id_estatus},
                 new EntidadParametro { Nombre = "P_id_usuario", Tipo = "Int", Valor = request.id_usuario},
            };
        }
        #endregion

        #region Métodos
        public async Task<ResponseGeneric<List<InsertarObservacionTransmisionResponse>>> Operacion(InsertarObservacionTransmisionRequest request)
        {
            List<InsertarObservacionTransmisionResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_insertar_observacion_transmision);
                            respuesta = await conexion.InsertarObservacionTransmisionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_insertar_observacion_transmision, tipo: "SELECT * FROM");
                            respuesta = await conexion.InsertarObservacionTransmisionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<InsertarObservacionTransmisionResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("InsertarObservacionTransmisionAccesoDatos - Operacion", ex);
                throw;
            }
        }
        #endregion
    }
}
