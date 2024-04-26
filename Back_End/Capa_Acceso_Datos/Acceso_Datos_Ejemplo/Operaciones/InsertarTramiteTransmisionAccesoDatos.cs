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
    public class InsertarTramiteTransmisionAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_insertar_tramite_transmision = "religiosos.sp_insertar_tramite_transmision";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public InsertarTramiteTransmisionAccesoDatos() : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(InsertarTramiteTransmisionRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "i_id_tbl_transmision", Tipo = "Int", Valor = request.id_transmision },
               new EntidadParametro { Nombre = "i_id_usuario", Tipo = "Int", Valor = request.id_usuario },
               new EntidadParametro { Nombre = "c_denominacion", Tipo = "String", Valor = request.denominacion == null ? "NULL" : request.denominacion },
               new EntidadParametro { Nombre = "c_numero_sgar", Tipo = "String", Valor = request.numero_sgar == null ? "NULL" : request.numero_sgar },
               new EntidadParametro { Nombre = "c_domicilio", Tipo = "String", Valor = request.domicilio == null ? "NULL" : request.domicilio },
               new EntidadParametro { Nombre = "c_correo_electronico", Tipo = "String", Valor = request.correo_electronico == null ? "NULL" : request.correo_electronico },
               new EntidadParametro { Nombre = "c_numero_tel", Tipo = "String", Valor = request.numero_tel == null ? "NULL" : request.numero_tel },
               new EntidadParametro { Nombre = "rep_nombre_completo", Tipo = "String", Valor = request.rep_nombre_completo == null ? "NULL" : request.rep_nombre_completo }
            };
        }
        #endregion

        #region Métodos Publicos
        public async Task<ResponseGeneric<List<InsertarTramiteTransmisionResponse>>> Operacion(InsertarTramiteTransmisionRequest model)
        {
            List<InsertarTramiteTransmisionResponse> respuesta = new List<InsertarTramiteTransmisionResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(model), sp_insertar_tramite_transmision);
                            respuesta = await conexion.InsertarTramiteTransmisionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(model), sp_insertar_tramite_transmision, tipo: "SELECT * FROM");
                            respuesta = await conexion.InsertarTramiteTransmisionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<InsertarTramiteTransmisionResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("InsertarTramiteTransmisionAccesoDatos - Operacion", ex);
                throw;
            }
        }
        #endregion
    }
}
