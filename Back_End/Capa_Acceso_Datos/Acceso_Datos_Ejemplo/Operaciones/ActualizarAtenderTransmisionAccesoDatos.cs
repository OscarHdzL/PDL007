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
    public class ActualizarAtenderTransmisionAccesoDatos : BaseAccesoDatos 
    {
        #region SP_Operaciones
        private const string sp_actualizar_atender_transmision = "religiosos.sp_actualizar_atender_transmision";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ActualizarAtenderTransmisionAccesoDatos() : base() { }
        #endregion

        #region Parametros SQL

        private List<EntidadParametro> ObtenerParametros(ActualizarAtenderTransmisionRequest request)
        {
            return new List<EntidadParametro>
            {
                 new EntidadParametro { Nombre = "p_id_transmision", Tipo = "Int", Valor = request.id_transmision},
                 new EntidadParametro { Nombre = "p_referencia", Tipo = "String", Valor = request.referencia},
                 new EntidadParametro { Nombre = "p_expediente", Tipo = "String", Valor = request.expediente},
                 new EntidadParametro { Nombre = "p_oficio", Tipo = "String", Valor = request.oficio},
                 new EntidadParametro { Nombre = "p_id_firmante", Tipo = "Int", Valor = request.id_firmante},
                 new EntidadParametro { Nombre = "p_puesto_firmante", Tipo = "String", Valor = request.puesto_firmante},
                 new EntidadParametro { Nombre = "p_id_ccp", Tipo = "Int", Valor = request.id_ccp},
            };
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Método encargado de ejecutar el proceso completo del registro del tutor y del alumno
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ActualizarAtenderTransmisionResponse>>> Operacion(ActualizarAtenderTransmisionRequest request)
        {
            List<ActualizarAtenderTransmisionResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_actualizar_atender_transmision);
                            respuesta = await conexion.ActualizarAtenderTransmisionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_actualizar_atender_transmision, tipo: "SELECT * FROM");
                            respuesta = await conexion.ActualizarAtenderTransmisionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ActualizarAtenderTransmisionResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ActualizarAtenderTransmisionAccesoDatos - Operacion", ex);
                throw;
            }
        }
        #endregion
    }
}
