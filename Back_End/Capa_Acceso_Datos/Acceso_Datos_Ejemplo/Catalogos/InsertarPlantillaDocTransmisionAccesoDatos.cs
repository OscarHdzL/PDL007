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
    /// Clase encargada del accesode de datos la Insertar una plantilla de documento de transmisión
    /// </summary>
    public class InsertarPlantillaDocTransmisionAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_insertar_catalogo_plantilla = "religiosos.sp_insertar_catalogo_plantilla";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos 
        /// </summary>
        public InsertarPlantillaDocTransmisionAccesoDatos()
            : base() { }
        #endregion


        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(InsertarPlantillaRequest entidad)
        {
            return new List<EntidadParametro>
            {
                new EntidadParametro { Nombre = "p_nombre", Tipo = "String", Valor = entidad.c_nombre ?? "NULL" },
                new EntidadParametro { Nombre = "p_ruta", Tipo = "String", Valor = entidad.c_ruta ?? "NULL" }
            };
        }
        #endregion


        #region Métodos Publicos
        /// <summary>
        /// Método encargado de insertar una plantilla
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<InsertarPlantillaDocTransmisionResponse>>> Operacion(InsertarPlantillaRequest request)
        {
            List<InsertarPlantillaDocTransmisionResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_insertar_catalogo_plantilla);
                            respuesta = await conexion.InsertarPlantillaDocTransmisionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_insertar_catalogo_plantilla, tipo: "SELECT * FROM");
                            respuesta = await conexion.InsertarPlantillaDocTransmisionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<InsertarPlantillaDocTransmisionResponse>>(respuesta);
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
