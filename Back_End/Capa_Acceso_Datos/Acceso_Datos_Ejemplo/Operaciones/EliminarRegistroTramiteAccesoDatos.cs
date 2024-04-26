using Acceso_Datos.Base;
using Conexion;
using Microsoft.EntityFrameworkCore;
using Modelos.Modelos.Request;
using Modelos.Modelos.Response;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acceso_Datos.Operaciones
{
   public class EliminarRegistroTramiteAccesoDatos :BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_eliminar_registro_tramite = "religiosos.sp_elimina_registro_tramite";

        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public EliminarRegistroTramiteAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL

        private List<EntidadParametro> ObtenerParametrosConteo(EliminarRegistroTramiteRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "id_tramite", Tipo = "Int", Valor = request.id_tramite },
     

            };
        }
        #endregion

        #region Métodos Publicos      
        /// <summary>
        /// Método encargado de consumir el sp y eliminar el registro del tramite
        /// </summary>
        /// <param name="id_tramite">Parametro de entrada </param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<EliminarRegistroTramiteResponse>>> EliminarRegistro(EliminarRegistroTramiteRequest request)
        {
            List<EliminarRegistroTramiteResponse> respuesta = new List<EliminarRegistroTramiteResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosConteo(request), sp_eliminar_registro_tramite);
                            respuesta = await conexion.EliminarRegistroTramiteResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosConteo(request), sp_eliminar_registro_tramite, tipo: "SELECT * FROM");
                            respuesta = await conexion.EliminarRegistroTramiteResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<EliminarRegistroTramiteResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("EliminarRegistroTramiteAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
