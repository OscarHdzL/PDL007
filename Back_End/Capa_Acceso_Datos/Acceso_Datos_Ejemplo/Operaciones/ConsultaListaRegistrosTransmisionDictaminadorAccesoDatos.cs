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
   public class ConsultaListaRegistrosTransmisionDictaminadorAccesoDatos :BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_lista_registros_transmision_dict = "religiosos.sp_consulta_lista_registros_transmisiones_dict";

        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ConsultaListaRegistrosTransmisionDictaminadorAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL

        private List<EntidadParametro> ObtenerParametros(ConsultaListaRegistrosTransmisionDictaminadorRequest request)
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
        public async Task<ResponseGeneric<List<ConsultaListaRegistrosTransmisionDictaminadorResponse>>> ConsultaRegistros(ConsultaListaRegistrosTransmisionDictaminadorRequest request)
        {
            List<ConsultaListaRegistrosTransmisionDictaminadorResponse> respuesta = new List<ConsultaListaRegistrosTransmisionDictaminadorResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_consulta_lista_registros_transmision_dict);
                            respuesta = await conexion.ConsultaListaRegistrosTransmisionDictaminadorResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_consulta_lista_registros_transmision_dict, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaListaRegistrosTransmisionDictaminadorResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaListaRegistrosTransmisionDictaminadorResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaListaRegistrosTransmisionDictaminadorAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
