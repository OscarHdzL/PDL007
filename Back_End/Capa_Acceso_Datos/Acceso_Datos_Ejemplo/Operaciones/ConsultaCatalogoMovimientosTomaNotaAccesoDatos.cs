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
    public class ConsultaCatalogoMovimientosTomaNotaAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_catalogo_movimientos_toma_nota = "religiosos.sp_consulta_catalogo_movimientos_toma_nota";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaCatalogoMovimientosTomaNotaAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(bool p_activos)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "p_activos", Tipo = "Boolean", Valor = p_activos},
            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaCatalogoMovimientosTomaNotaResponse>>> Consultar(bool p_activos)
        {
            List<ConsultaCatalogoMovimientosTomaNotaResponse> respuesta = new List<ConsultaCatalogoMovimientosTomaNotaResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(p_activos), sp_consulta_catalogo_movimientos_toma_nota);
                            respuesta = await conexion.ConsultaCatalogoMovimientosTomaNotaResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(p_activos), sp_consulta_catalogo_movimientos_toma_nota, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaCatalogoMovimientosTomaNotaResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaCatalogoMovimientosTomaNotaResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaCatalogoMovimientosTomaNotaAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
