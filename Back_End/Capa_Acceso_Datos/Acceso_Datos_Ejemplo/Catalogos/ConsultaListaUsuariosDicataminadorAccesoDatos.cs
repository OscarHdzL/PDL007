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

namespace Acceso_Datos.Catalogos
{
    public class ConsultaListaUsuariosDicataminadorAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_lista_convocatorias = "religiosos.sp_consulta_lista_usuarios_dictaminador";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaListaUsuariosDicataminadorAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaListaUsuariosDicataminadorResponse>>> Consultar()
        {
            List<ConsultaListaUsuariosDicataminadorResponse> respuesta = new List<ConsultaListaUsuariosDicataminadorResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(null, sp_consulta_lista_convocatorias);
                            respuesta = await conexion.ConsultaListaUsuariosDicataminadorResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(null, sp_consulta_lista_convocatorias, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaListaUsuariosDicataminadorResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaListaUsuariosDicataminadorResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaListaUsuariosDicataminador", ex);
                throw;
            }
        }
        #endregion
    }
}
