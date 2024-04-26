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
    public class BorraCatalogoCnotarioarrAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_borra_convocatoria = "religiosos.sp_desactivar_catalogo_cnotarioarr";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public BorraCatalogoCnotarioarrAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(BorraCatalagoCnotarioarrRequest entidad)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "c_id", Tipo = "Int", Valor = entidad.c_id },
               new EntidadParametro { Nombre = "c_activo", Tipo = "Boolean", Valor = entidad.c_activo},
            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<BorraCatalogoCnotarioarrResponse>>> Operacion(BorraCatalagoCnotarioarrRequest request)
        {
            List<BorraCatalogoCnotarioarrResponse> respuesta = new List<BorraCatalogoCnotarioarrResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_borra_convocatoria);
                            respuesta = await conexion.BorraCatalogoCnotarioarrResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_borra_convocatoria, tipo: "SELECT * FROM");
                            respuesta = await conexion.BorraCatalogoCnotarioarrResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<BorraCatalogoCnotarioarrResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("BorraConvocatoriaAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
