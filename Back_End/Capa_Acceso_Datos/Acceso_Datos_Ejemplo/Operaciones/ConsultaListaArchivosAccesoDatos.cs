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
    /// <summary>
    /// Clase del acceso a datos para consultar la lista de archivos.
    /// </summary>
    public class ConsultaListaArchivosAccesoDatos : BaseAccesoDatos
    {
        #region Constantes SP
        private string SP_Consulta_Lista_Archivos = "religiosos.sp_consulta_lista_archivos_transmision";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial
        /// </summary>
        public ConsultaListaArchivosAccesoDatos() : base()
        {

        }
        #endregion

        #region Parámetros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para consultar 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametrosConsultaLista(ArchivosRequest request)
        {
            return new List<EntidadParametro>
            {
                new EntidadParametro { Nombre = "id_transmision", Tipo = "Int", Valor = request.transmisionId == null ? "NULL" : request.transmisionId  }
            };
        }
        #endregion

        #region Métodos publicos
        /// <summary>
        /// Método encargado consultar la lista de archivos
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaArchivosListaResponse>>> Consultar(ArchivosRequest request)
        {
            List<ConsultaArchivosListaResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosConsultaLista(request), SP_Consulta_Lista_Archivos);
                            respuesta = await conexion.ConsultaArchivosListaResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosConsultaLista(request), SP_Consulta_Lista_Archivos, "SELECT * FROM");
                            respuesta = await conexion.ConsultaArchivosListaResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaArchivosListaResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("InicioSesionAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
