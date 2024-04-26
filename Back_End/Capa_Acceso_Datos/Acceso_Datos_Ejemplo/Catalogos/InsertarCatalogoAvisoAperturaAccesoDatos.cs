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
    public class InsertarCatalogoAvisoAperturaAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_borra_convocatoria = "religiosos.sp_insertar_catalogo_avisoap";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public InsertarCatalogoAvisoAperturaAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(CatalogoAvisoAperturaInsertRequest entidad)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "c_nombre_n", Tipo = "String", Valor = entidad.nombre},
               new EntidadParametro { Nombre = "c_descripcion_n", Tipo = "String", Valor = entidad.descripcion},
               new EntidadParametro { Nombre = "c_f_ini_vig", Tipo = "String", Valor = entidad.f_inic_vig?? "NULL" },
               new EntidadParametro { Nombre = "c_f_fin_vig", Tipo = "String", Valor = entidad.f_fin_vig?? "NULL" },
            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<CatalogoAvisoAperturaInsertResponse>>> Operacion(CatalogoAvisoAperturaInsertRequest[] request)
        {
            List<CatalogoAvisoAperturaInsertResponse> respuesta = new List<CatalogoAvisoAperturaInsertResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            foreach (var item in request)
                            {
                                var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(item), sp_borra_convocatoria);
                                var records = await conexion.CatalogoAvisoAperturaInsertResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                                respuesta.Add(records[0]);
                            }
                            break;

                        case 2:
                            foreach (var item in request)
                            {
                                var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(item), sp_borra_convocatoria, tipo: "SELECT * FROM");
                                var records = await conexion.CatalogoAvisoAperturaInsertResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                                respuesta.Add(records[0]);
                            }

                            break;
                    }
                }

                return new ResponseGeneric<List<CatalogoAvisoAperturaInsertResponse>>(respuesta);
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
