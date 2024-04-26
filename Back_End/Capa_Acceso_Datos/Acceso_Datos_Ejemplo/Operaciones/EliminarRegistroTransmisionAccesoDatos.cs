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
   public class EliminarRegistroTransmisionAccesoDatos :BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_eliminar_registro_transmision = "religiosos.sp_elimina_registro_transmision";

        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public EliminarRegistroTransmisionAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL

        private List<EntidadParametro> ObtenerParametros(EliminarRegistroTransmisionRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "id_transmision", Tipo = "Int", Valor = request.id_transmision },


            };
        }
        #endregion

        #region Métodos Publicos      
        /// <summary>
        /// Método encargado de consumir el sp y eliminar el registro del transmision
        /// </summary>
        /// <param name="id_tramite">Parametro de entrada </param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<EliminarRegistroTransmisionResponse>>> EliminarRegistro(EliminarRegistroTransmisionRequest request)
        {
            List<EliminarRegistroTransmisionResponse> respuesta = new List<EliminarRegistroTransmisionResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_eliminar_registro_transmision);
                            respuesta = await conexion.EliminarRegistroTransmisionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_eliminar_registro_transmision, tipo: "SELECT * FROM");
                            respuesta = await conexion.EliminarRegistroTransmisionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<EliminarRegistroTransmisionResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("EliminarRegistroTransmisionAccesoDatos", ex);
                throw;
            }
        }
        #endregion

    }
}
