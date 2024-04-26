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

namespace Acceso_Datos.Operaciones
{
    public class ConsultaMunicipiosAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string SP_Consulta_Datos = "religiosos.SP_Consulta_Municipios";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaMunicipiosAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtenr los municipios
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ConsultaMunicipiosRequest entidad)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "idestadoreporte", Tipo = "Int", Valor = entidad.idestadoreporte == null ? "NULL" : entidad.idestadoreporte.Value },
            };
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado de validar las credenciales para el inicio de sesion
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaMunicipiosResponse>>> Consultar(ConsultaMunicipiosRequest request)
        {
            List<ConsultaMunicipiosResponse> respuesta = new List<ConsultaMunicipiosResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), SP_Consulta_Datos);
                            respuesta = await conexion.ConsultaMunicipiosResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), SP_Consulta_Datos, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaMunicipiosResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaMunicipiosResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("AccesoDatos ConsultaDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
