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
    public class ConsultaListaCatalogoCnotarioarrAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_lista_convocatorias = "religiosos.sp_consulta_lista_catalogos_cnotarioarr";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaListaCatalogoCnotarioarrAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para Actualizar usuario al sistema
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(int? tipoSolicitud)
        {
            return new List<EntidadParametro>
            {
                 new EntidadParametro { Nombre = "p_tipoSolicitud", Tipo = "Int", Valor = tipoSolicitud == null ? "NULL" : tipoSolicitud }
            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaListaCatalogoCnotarioarrResponse>>> Consultar(int? tipoSolicitud)
        {
            List<ConsultaListaCatalogoCnotarioarrResponse> respuesta = new List<ConsultaListaCatalogoCnotarioarrResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(tipoSolicitud), sp_consulta_lista_convocatorias);
                            respuesta = await conexion.ConsultaListaCatalogoCnotarioarrResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(tipoSolicitud), sp_consulta_lista_convocatorias, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaListaCatalogoCnotarioarrResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaListaCatalogoCnotarioarrResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaListaConvocatoriasAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
