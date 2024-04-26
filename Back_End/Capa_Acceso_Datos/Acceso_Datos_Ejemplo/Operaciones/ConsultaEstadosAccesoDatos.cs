using Acceso_Datos.Base;
using Conexion;
using Microsoft.EntityFrameworkCore;
using Modelos.Modelos.Response;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceso_Datos.Operaciones
{
    public class ConsultaEstadosAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string SP_Consulta_Datos = "religiosos.SP_Consulta_Estados";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaEstadosAccesoDatos()
            : base() { }
        #endregion

        //private List<EntidadParametro> ObtenerParametros()
        //{
        //    return new List<EntidadParametro>
        //    {
        //       new EntidadParametro { Nombre = "idestadoalumno", Tipo = "Int", Valor = 1 == null ? "NULL" : 1 },
        //    };
        //}

        #region Métodos Publicos
        /// <summary>
        /// Método encargado de consultar los estados
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaEstadosResponse>>> Consultar()
        {
            List<ConsultaEstadosResponse> respuesta = new List<ConsultaEstadosResponse>();
            List<EntidadParametro> parametros = new List<EntidadParametro>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(null, SP_Consulta_Datos);
                            respuesta = await conexion.ConsultaEstadosResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(null, SP_Consulta_Datos, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaEstadosResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaEstadosResponse>>(respuesta);
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
