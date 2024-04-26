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

namespace Acceso_Datos.Catalogos
{
    /// <summary>
    /// Clase encargada del accesode de datos la actuliza una plantilla de documento de transmisión
    /// </summary>
    public class ActulizarPlantillaDocTransmisionAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_actuliza_catalogo_plantilla = "religiosos.sp_actuliza_catalogo_plantilla";
        private const string sp_actuliza_catalogo_selecciona_plantilla = "religiosos.sp_actuliza_catalogo_selecciona_plantilla";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos 
        /// </summary>
        public ActulizarPlantillaDocTransmisionAccesoDatos()
            : base() { }
        #endregion


        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ActulizarPlantillaRequest entidad)
        {
            return new List<EntidadParametro>
            {
                new EntidadParametro { Nombre = "p_id", Tipo = "Int", Valor = entidad.i_id == null ? "NULL" : entidad.i_id },
                new EntidadParametro { Nombre = "p_nombre", Tipo = "String", Valor = entidad.c_nombre ?? "NULL" },
                new EntidadParametro { Nombre = "p_ruta", Tipo = "String", Valor = entidad.c_ruta ?? "NULL" }
            };
        }

        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametrosSeleccionarUnaPlantilla(ActulizarPlantillaRequest entidad)
        {
            return new List<EntidadParametro>
            {
                new EntidadParametro { Nombre = "p_id", Tipo = "Int", Valor = entidad.i_id == null ? "NULL" : entidad.i_id },
                new EntidadParametro { Nombre = "i_estatus",  Tipo = "Int", Valor = entidad.i_estatus == null ? "NULL" : entidad.i_estatus}
            };
        }
        #endregion


        #region Métodos Publicos
        /// <summary>
        /// Método encargado de insertar una plantilla
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ActulizarPlantillaDocTransmisionResponse>>> Operacion(ActulizarPlantillaRequest request)
        {
            List<ActulizarPlantillaDocTransmisionResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_actuliza_catalogo_plantilla);
                            respuesta = await conexion.ActulizarPlantillaDocTransmisionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_actuliza_catalogo_plantilla, tipo: "SELECT * FROM");
                            respuesta = await conexion.ActulizarPlantillaDocTransmisionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ActulizarPlantillaDocTransmisionResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("BorraConvocatoriaAccesoDatos", ex);
                throw;
            }
        }

        /// <summary>
        /// Método encargado de insertar una plantilla
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ActulizarPlantillaDocTransmisionResponse>>> OperacionSeleccionaPlantilla(ActulizarPlantillaRequest request)
        {
            List<ActulizarPlantillaDocTransmisionResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosSeleccionarUnaPlantilla(request), sp_actuliza_catalogo_selecciona_plantilla);
                            respuesta = await conexion.ActulizarPlantillaDocTransmisionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosSeleccionarUnaPlantilla(request), sp_actuliza_catalogo_selecciona_plantilla, tipo: "SELECT * FROM");
                            respuesta = await conexion.ActulizarPlantillaDocTransmisionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ActulizarPlantillaDocTransmisionResponse>>(respuesta);
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
