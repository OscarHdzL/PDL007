using Acceso_Datos.Base;
using Conexion;
using Microsoft.EntityFrameworkCore;
using Modelos.Modelos.Response;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acceso_Datos.Catalogos
{
    public class ConsultaListaUsuariosDictaminadorTransmisionAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_lista_usuarios_dictaminador_transmision = "religiosos.sp_consulta_lista_usuarios_dictaminador_transmision";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaListaUsuariosDictaminadorTransmisionAccesoDatos() : base() { }
        #endregion

        #region Parametros SQL

        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaListaUsuariosDictaminadorTransmisionResponse>>> Consultar()
        {
            List<ConsultaListaUsuariosDictaminadorTransmisionResponse> respuesta = new List<ConsultaListaUsuariosDictaminadorTransmisionResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(null, sp_consulta_lista_usuarios_dictaminador_transmision);
                            respuesta = await conexion.ConsultaListaUsuariosDictaminadorTransmisionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(null, sp_consulta_lista_usuarios_dictaminador_transmision, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaListaUsuariosDictaminadorTransmisionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaListaUsuariosDictaminadorTransmisionResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaListaUsuariosDictaminadorTransmisionAccesoDatos -  Consultar", ex);
                throw;
            }
        }
        #endregion
    }
}
