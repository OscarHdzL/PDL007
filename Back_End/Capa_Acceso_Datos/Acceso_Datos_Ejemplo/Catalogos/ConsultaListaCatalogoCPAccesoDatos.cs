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
    public class ConsultaListaCatalogoCPAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_lista_CP = "religiosos.sp_consulta_lista_catalogos_CP";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaListaCatalogoCPAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        private List<EntidadParametro> ObtenerParametros(ConsultaListaCatalogoCPRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "keyword", Tipo = "String", Valor = request.keyword },
            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaListaCatalogoCPResponse>>> Consultar(ConsultaListaCatalogoCPRequest request)
        {
            List<ConsultaListaCatalogoCPResponse> respuesta = new List<ConsultaListaCatalogoCPResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_consulta_lista_CP);
                            respuesta = await conexion.ConsultaListaCatalogoCPResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_consulta_lista_CP, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaListaCatalogoCPResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaListaCatalogoCPResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaListaCPAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
