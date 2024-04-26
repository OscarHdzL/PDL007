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
    public class ActualizarCatalogoSJuridicaAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_Actualizar_convocatoria = "religiosos.sp_actualizar_catalogo_sjuridica";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ActualizarCatalogoSJuridicaAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ActualizarCatalogoSJuridicaRequest entidad)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "c_id", Tipo = "Int", Valor = entidad.c_id},
               new EntidadParametro { Nombre = "c_nombre", Tipo = "String", Valor = entidad.c_nombre?? "NULL" },
               new EntidadParametro { Nombre = "c_descripcion", Tipo = "String", Valor = entidad.c_descripcion ?? "NULL"},
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
        public async Task<ResponseGeneric<List<ActualizarCatalogoSJuridicaResponse>>> Operacion(ActualizarCatalogoSJuridicaRequest request)
        {
            List<ActualizarCatalogoSJuridicaResponse> respuesta = new List<ActualizarCatalogoSJuridicaResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_Actualizar_convocatoria);
                            respuesta = await conexion.ActualizarCatalogoSJuridicaResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_Actualizar_convocatoria, tipo: "SELECT * FROM");
                            respuesta = await conexion.ActualizarCatalogoSJuridicaResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ActualizarCatalogoSJuridicaResponse>>(respuesta);
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
