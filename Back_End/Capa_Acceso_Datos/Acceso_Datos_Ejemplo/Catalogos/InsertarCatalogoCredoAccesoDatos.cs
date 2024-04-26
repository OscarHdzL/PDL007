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
    public class InsertarCatalogoCredoAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_borra_convocatoria = "religiosos.sp_insertar_catalogo_credo";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public InsertarCatalogoCredoAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(CatalogoCredoInsertRequest entidad)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "c_nombre_n", Tipo = "String", Valor = entidad.nombre?? "NULL" },
            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<CatalogoCredoInsertResponse>>> Operacion(CatalogoCredoInsertRequest[] request)
        {
            List<CatalogoCredoInsertResponse> respuesta = new List<CatalogoCredoInsertResponse>();
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
                                var records = await conexion.CatalogoCredoInsertResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                                respuesta.Add(records[0]);
                            }
                            break;

                        case 2:
                            foreach (var item in request)
                            {
                                var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(item), sp_borra_convocatoria, tipo: "SELECT * FROM");
                                var records = await conexion.CatalogoCredoInsertResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                                respuesta.Add(records[0]);
                            }

                            break;
                    }
                }

                return new ResponseGeneric<List<CatalogoCredoInsertResponse>>(respuesta);
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
