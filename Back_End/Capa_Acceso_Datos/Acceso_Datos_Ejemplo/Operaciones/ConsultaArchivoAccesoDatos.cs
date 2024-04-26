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
    public class ConsultaArchivoAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones

        private const string sp_consulta_archivo = "religiosos.sp_consulta_archivo";
        private const string sp_consulta_path_plantilla =  "religiosos.sp_consulta_path_plantilla";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaArchivoAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para insertar usuario al sistema
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(long id, int idArchivoTramite)
        {
            return new List<EntidadParametro>
            {
                 new EntidadParametro { Nombre = "id", Tipo = "Int", Valor = id },
                 new EntidadParametro { Nombre = "id_archivo_tramite", Tipo = "Int", Valor = idArchivoTramite },
            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ArchivoResponse>>> Consultar(long id, int idArchivoTramite)
        {
            List<ArchivoResponse> respuesta = new List<ArchivoResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(id, idArchivoTramite), sp_consulta_archivo);
                            respuesta = await conexion.ArchivoResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(id, idArchivoTramite), sp_consulta_archivo, tipo: "SELECT * FROM");
                            respuesta = await conexion.ArchivoResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ArchivoResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaArchivoAccesoDatos", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<PlantillaResponse>>>GetPlantilla(int p_id_archivo)
        {
            List<PlantillaResponse> respuesta = new List<PlantillaResponse>();
            
            List<EntidadParametro> parametros = new List<EntidadParametro>();
            parametros.Add(new EntidadParametro { Nombre = "p_id_archivo", Tipo = "Int", Valor = p_id_archivo });

            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(parametros, sp_consulta_path_plantilla);
                            respuesta = await conexion.PlantillaResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(parametros, sp_consulta_path_plantilla, tipo: "SELECT * FROM");
                            respuesta = await conexion.PlantillaResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<PlantillaResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaArchivoAccesoDatos - GetPlantilla", ex);
                throw;
            }
        }
        #endregion
    }
}
