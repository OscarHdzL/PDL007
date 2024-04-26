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
    public class ConsultaListaCatalogoCotejoAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_lista_convocatorias = "religiosos.sp_consulta_lista_catalogos_Cotejo";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaListaCatalogoCotejoAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(CatalogoCotejoListaRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "f_id", Tipo = "Int", Valor = request.f_id},
               new EntidadParametro { Nombre = "Activos", Tipo = "Boolean", Valor = request.Activos},
            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaListaCatalogoCotejoResponse>>> Consultar(CatalogoCotejoListaRequest request)
        {
            List<ConsultaListaCatalogoCotejoResponse> respuesta = new List<ConsultaListaCatalogoCotejoResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_consulta_lista_convocatorias);
                            respuesta = await conexion.ConsultaListaCatalogoCotejoResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_consulta_lista_convocatorias, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaListaCatalogoCotejoResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaListaCatalogoCotejoResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaListaCaalogoCotejo", ex);
                throw;
            }
        }
        #endregion
    }
}
