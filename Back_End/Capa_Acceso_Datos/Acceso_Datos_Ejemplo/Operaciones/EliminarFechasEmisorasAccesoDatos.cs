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
    public class EliminarFechasEmisorasAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_eliminar_fechas_emisoras = "religiosos.sp_eliminar_fechas_emisoras";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public EliminarFechasEmisorasAccesoDatos() : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(EliminarFechasEmisorasRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "id_tipo", Tipo = "Int", Valor = request.id_tipo },
               new EntidadParametro { Nombre = "i_id_entidad", Tipo = "Int", Valor = request.i_id_entidad }
            };
        }
        #endregion

        #region Métodos Publicos
        public async Task<ResponseGeneric<List<EliminarFechasEmisorasResponse>>> Operacion(EliminarFechasEmisorasRequest model)
        {
            List<EliminarFechasEmisorasResponse> respuesta = new List<EliminarFechasEmisorasResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(model), sp_eliminar_fechas_emisoras);
                            respuesta = await conexion.EliminarFechasEmisorasResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(model), sp_eliminar_fechas_emisoras, tipo: "SELECT * FROM");
                            respuesta = await conexion.EliminarFechasEmisorasResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<EliminarFechasEmisorasResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("EliminarFechasEmisorasAccesoDatos - Operacion", ex);
                throw;
            }
        }
        #endregion

    }
}
