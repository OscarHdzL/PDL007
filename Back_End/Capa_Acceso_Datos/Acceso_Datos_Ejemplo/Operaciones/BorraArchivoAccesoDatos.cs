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

namespace Acceso_Datos.Operaciones
{
    public class BorraArchivoAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_borra_archivo = "religiosos.sp_eliminar_archivo";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public BorraArchivoAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ArchivoRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "id", Tipo = "Int", Valor = request.id },
               new EntidadParametro { Nombre = "id_archivo_tramite", Tipo = "Int", Valor = request.idArchivoTramite },
            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<ArchivoResponse>> Operacion(ArchivoRequest request)
        {
            ArchivoResponse respuesta = new ArchivoResponse();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_borra_archivo);
                            respuesta = await conexion.ArchivoResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_borra_archivo, tipo: "SELECT * FROM");
                            respuesta = await conexion.ArchivoResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).FirstOrDefaultAsync();
                            break;
                    }
                }

                return new ResponseGeneric<ArchivoResponse>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("BorraArchivoAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
