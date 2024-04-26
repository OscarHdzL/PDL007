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
   public class EliminarRegistroTomaNotaAccesoDatos :BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_eliminar_registro_tnota = "religiosos.sp_elimina_registro_tnota";

        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public EliminarRegistroTomaNotaAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL

        private List<EntidadParametro> ObtenerParametros(EliminarRegistroTomaNotaRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "id_tnota", Tipo = "Int", Valor = request.id_tnota },


            };
        }
        #endregion

        #region Métodos Publicos      
        /// <summary>
        /// Método encargado de consumir el sp y eliminar el registro de la toma de nota
        /// </summary>
        /// <param name="id_tnota">Parametro de entrada </param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<EliminarRegistroTomaNotaResponse>>> EliminarRegistro(EliminarRegistroTomaNotaRequest request)
        {
            List<EliminarRegistroTomaNotaResponse> respuesta = new List<EliminarRegistroTomaNotaResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_eliminar_registro_tnota);
                            respuesta = await conexion.EliminarRegistroTomaNotaResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_eliminar_registro_tnota, tipo: "SELECT * FROM");
                            respuesta = await conexion.EliminarRegistroTomaNotaResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<EliminarRegistroTomaNotaResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("EliminarRegistroTomaNotaAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}

