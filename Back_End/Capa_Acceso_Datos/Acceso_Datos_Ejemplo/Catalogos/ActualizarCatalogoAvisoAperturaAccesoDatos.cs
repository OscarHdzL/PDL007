using Acceso_Datos.Base;
using Conexion;
using Microsoft.EntityFrameworkCore;
using Modelos.Modelos;
using Modelos.Modelos.Request;
using Modelos.Modelos.Response;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceso_Datos.Catalogos
{
    public class ActualizarCatalogoAvisoAperturaAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_Actualizar_convocatoria = "religiosos.sp_actualizar_catalogo_avisoap";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ActualizarCatalogoAvisoAperturaAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ActualizarCatalogoAvisoAperturaRequest entidad)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "c_id", Tipo = "Int", Valor = entidad.c_id},
               new EntidadParametro { Nombre = "c_nombre_n", Tipo = "String", Valor = entidad.c_nombre_n?? "NULL" },
               new EntidadParametro { Nombre = "c_descripcion_n", Tipo = "String", Valor = entidad.c_descripcion_n?? "NULL"},
              new EntidadParametro { Nombre = "c_f_inic_vig", Tipo = "Date", Valor = string.IsNullOrEmpty(entidad.c_f_inic_vig) ? DBNull.Value:DateTime.ParseExact(entidad.c_f_inic_vig,"yyyy-MM-dd",CultureInfo.InvariantCulture)},
               new EntidadParametro { Nombre = "c_f_fin_vig", Tipo = "Date", Valor =  string.IsNullOrEmpty(entidad.c_f_fin_vig) ? DBNull.Value:DateTime.ParseExact(entidad.c_f_fin_vig,"yyyy-MM-dd",CultureInfo.InvariantCulture)},
            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ActualizarCatalogoAvisoAperturaResponse>>> Operacion(ActualizarCatalogoAvisoAperturaRequest request)
        {
            List<ActualizarCatalogoAvisoAperturaResponse> respuesta = new List<ActualizarCatalogoAvisoAperturaResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_Actualizar_convocatoria);
                            respuesta = await conexion.ActualizarCatalogoAvisoAperturaResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_Actualizar_convocatoria, tipo: "SELECT * FROM");
                            respuesta = await conexion.ActualizarCatalogoAvisoAperturaResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ActualizarCatalogoAvisoAperturaResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ActualizarConvocatoriaAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
