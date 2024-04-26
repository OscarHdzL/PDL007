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
    public class ConsultaListaRegistrosTransmisionAccesoDatos :BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_lista_registros_transmision = "religiosos.sp_consulta_lista_registros_transmisiones";

        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaListaRegistrosTransmisionAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL

        private List<EntidadParametro> ObtenerParametros(ConsultaListaRegistrosTransmisionRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "id_usuario", Tipo = "Int", Valor = request.id_usuario },
               new EntidadParametro { Nombre = "numero_sgar_desc", Tipo = "String", Valor = request.numero_sgar_desc },
               new EntidadParametro { Nombre = "denominacion_desc", Tipo = "String", Valor = request.denominacion_desc },
               new EntidadParametro { Nombre = "estatus_desc", Tipo = "Int", Valor = request.estatus_desc },
               new EntidadParametro { Nombre = "representante_desc", Tipo = "String", Valor = request.representante_desc },

            };
        }
        #endregion

        #region Métodos Publicos      
        /// <summary>
        /// Método encargado de obtener la lista de registros asociados a un usuario
        /// </summary>
        /// <param name="id_usuario">Parametro de entrada </param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaListaRegistrosTransmisionResponse>>> ConsultaRegistros(ConsultaListaRegistrosTransmisionRequest request)
        {
            List<ConsultaListaRegistrosTransmisionResponse> respuesta = new List<ConsultaListaRegistrosTransmisionResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_consulta_lista_registros_transmision);
                            respuesta = await conexion.ConsultaListaRegistrosTransmisionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_consulta_lista_registros_transmision, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaListaRegistrosTransmisionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaListaRegistrosTransmisionResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaListaRegistrosTransmisionAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
