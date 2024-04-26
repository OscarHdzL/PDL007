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

    /// <summary>
    /// Clase encargada del accesode de datos para borrar una plantilla de documento de transmisión
    /// </summary>
    public class BorrarPlantillaDocTransmisionAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_borrar_catalogo_plantilla = "religiosos.sp_borrar_catalogo_plantilla";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos 
        /// </summary>
        public BorrarPlantillaDocTransmisionAccesoDatos()
            : base() { }
        #endregion


        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(BorrarPlantillaDocTransmisionRequest entidad)
        {
            return new List<EntidadParametro>
            {
                new EntidadParametro { Nombre = "p_id", Tipo = "Int", Valor = entidad.i_id == null ?  "NULL" : entidad.i_id}
            };
        }
        #endregion


        #region Métodos Publicos
        /// <summary>
        /// Método encargado de insertar una plantilla
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<BorrarPlantillaDocTransmisionResponse>>> Operacion(BorrarPlantillaDocTransmisionRequest request)
        {
            List<BorrarPlantillaDocTransmisionResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_borrar_catalogo_plantilla);
                            respuesta = await conexion.BorrarPlantillaDocTransmisionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_borrar_catalogo_plantilla, tipo: "SELECT * FROM");
                            respuesta = await conexion.BorrarPlantillaDocTransmisionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<BorrarPlantillaDocTransmisionResponse>>(respuesta);
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
