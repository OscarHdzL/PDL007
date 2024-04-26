using Acceso_Datos.Base;
using Conexion;
using Microsoft.EntityFrameworkCore;
using Modelos.Modelos.Request;
using Modelos.Modelos.Response;
using Modelos.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceso_Datos.Operaciones
{
    /// <summary>
    /// Clase encargado del acceso de datos
    /// </summary>
    public class InsertarArchivoAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_insertar_archivo = "religiosos.sp_insertar_archivo";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public InsertarArchivoAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para insertar usuario al sistema
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ArchivoRequest request)
        {
            return new List<EntidadParametro>
            {
                 new EntidadParametro { Nombre = "id", Tipo = "Int", Valor = request.id },
                 new EntidadParametro { Nombre = "archivo", Tipo = "String", Valor = request.archivo == null ? "NULL" : request.archivo.ToString() },
                 new EntidadParametro { Nombre = "id_archivo_tramite", Tipo = "Int", Valor = request.idArchivoTramite },
            };
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Método encargado de ejecutar el proceso completo del registro del tutor y del alumno
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ArchivoResponse>>> Operacion(ArchivoRequest request)
        {
            List<ArchivoResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_insertar_archivo);
                            respuesta = await conexion.ArchivoResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_insertar_archivo, tipo: "SELECT * FROM");
                            respuesta = await conexion.ArchivoResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ArchivoResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("InsertarArchivoAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
