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
    public class AutorizarTransmisionAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_autorizar_transmision = "religiosos.sp_autorizar_transmision";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public AutorizarTransmisionAccesoDatos() : base() { }
        #endregion

        #region Parametros SQL
        private List<EntidadParametro> ObtenerParametros(AutorizarTransmisionRequest request)
        {
            return new List<EntidadParametro>
            {
                 new EntidadParametro { Nombre = "p_id_transmision", Tipo = "Int", Valor = request.id_transmision},
                 new EntidadParametro { Nombre = "p_fecha", Tipo = "String", Valor = request.fecha},
                 new EntidadParametro { Nombre = "p_hora", Tipo = "String", Valor = request.horario},
                 new EntidadParametro { Nombre = "p_direccion", Tipo = "String", Valor = request.direccion},
                 new EntidadParametro { Nombre = "p_id_usuario", Tipo = "Int", Valor = request.id_usuario},
            };
        }
        #endregion

        #region Métodos
        public async Task<ResponseGeneric<List<AutorizarTransmisionResponse>>> Operacion(AutorizarTransmisionRequest request)
        {
            List<AutorizarTransmisionResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_autorizar_transmision);
                            respuesta = await conexion.AutorizarTransmisionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_autorizar_transmision, tipo: "SELECT * FROM");
                            respuesta = await conexion.AutorizarTransmisionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<AutorizarTransmisionResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("AutorizarTransmisionAccesoDatos - Operacion", ex);
                throw;
            }
        }
        #endregion
    }
}
