using Acceso_Datos.Base;
using Conexion;
using Microsoft.EntityFrameworkCore;
using Modelos.Modelos.Utilidades.Request;
using Modelos.Modelos.Utilidades.Response;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceso_Datos_Ejemplo.Operaciones
{
    public class ConsultaListaUsuariosSistemaAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_lista_usuarios_sistema = "religiosos.sp_consulta_lista_usuarios_sistema";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaListaUsuariosSistemaAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtenr los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ConsultaListaUsuariosSistemaRequest entidad)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "id_ca_perfiles", Tipo = "Int", Valor = entidad.id_ca_perfiles == null ? "NULL" : entidad.id_ca_perfiles.Value },
            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaListaUsuariosSistemaResponse>>> Consultar(ConsultaListaUsuariosSistemaRequest request)
        {
            List<ConsultaListaUsuariosSistemaResponse> respuesta = new List<ConsultaListaUsuariosSistemaResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_consulta_lista_usuarios_sistema);
                            respuesta = await conexion.ConsultaListaUsuariosSistemaResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_consulta_lista_usuarios_sistema, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaListaUsuariosSistemaResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaListaUsuariosSistemaResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("AccesoDatos ConsultaListaUsuariosSistemaAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
