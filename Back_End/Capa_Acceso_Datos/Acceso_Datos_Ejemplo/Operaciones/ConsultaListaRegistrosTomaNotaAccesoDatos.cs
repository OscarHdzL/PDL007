using Acceso_Datos.Base;
using Conexion;
using Microsoft.EntityFrameworkCore;
using Modelos.Modelos.Request;
using Modelos.Modelos.Response;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acceso_Datos.Operaciones
{
    public class ConsultaListaRegistrosTomaNotaAccesoDatos  :BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_lista_registros_tnota = "religiosos.sp_consulta_lista_registros_tnota_i";

        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaListaRegistrosTomaNotaAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL

        private List<EntidadParametro> ObtenerParametrosConsulta(ConsultaListaRegistrosTomaNotaRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "id_usuario", Tipo = "Int", Valor = request.id_usuario },
               new EntidadParametro { Nombre = "denominacion_desc", Tipo = "String", Valor = request.denominacion_desc },
               new EntidadParametro { Nombre = "nombre_desc", Tipo = "String", Valor = request.nombre_desc },
               new EntidadParametro { Nombre = "estatus_desc", Tipo = "Int", Valor = request.estatus_desc },

            };
        }
        #endregion

        #region Métodos Publicos      
        /// <summary>
        /// Método encargado de obtener la lista de registros asociados a un usuario
        /// </summary>
        /// <param name="id_usuario">Parametro de entrada </param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaListaRegistrosTomaNotaResponse>>> ConsultaRegistros(ConsultaListaRegistrosTomaNotaRequest request)
        {
            List<ConsultaListaRegistrosTomaNotaResponse> respuesta = new List<ConsultaListaRegistrosTomaNotaResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosConsulta(request), sp_consulta_lista_registros_tnota);
                            respuesta = await conexion.ConsultaListaRegistrosTomaNotaResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosConsulta(request), sp_consulta_lista_registros_tnota, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaListaRegistrosTomaNotaResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaListaRegistrosTomaNotaResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaListaRegistrosTomaNotaResponse", ex);
                throw;
            }
        }
        #endregion
    }
}
