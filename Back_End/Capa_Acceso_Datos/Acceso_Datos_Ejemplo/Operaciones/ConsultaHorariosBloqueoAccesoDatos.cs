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
    public class ConsultaHorariosBloqueoAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_horarios_bloq = "religiosos.sp_consulta_horarios_bloq";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ConsultaHorariosBloqueoAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ConsultaHorariosBloqueoRequest entidad)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "id_usuario_dictaminador", Tipo = "Int", Valor = entidad.id_usuario_dictaminador },
               new EntidadParametro { Nombre = "fecha_cotejo_inicio", Tipo = "String", Valor = entidad.fecha_cotejo_inicio },
               new EntidadParametro { Nombre = "fecha_cotejo_fin", Tipo = "String", Valor = entidad.fecha_cotejo_fin },
            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaHorariosBloqueoResponse>>> Consultar(ConsultaHorariosBloqueoRequest request)
        {
            List<ConsultaHorariosBloqueoResponse> respuesta = new List<ConsultaHorariosBloqueoResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_consulta_horarios_bloq);
                            respuesta = await conexion.ConsultaHorariosBloqueoResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_consulta_horarios_bloq, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaHorariosBloqueoResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaHorariosBloqueoResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("AccesoDatos ConsultaHorariosBloqueoAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
