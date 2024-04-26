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
    public class ActualizarCotejoTomaNotaAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_insertar_usuario_sistema = "religiosos.sp_actualizar_cotejo_toma_nota";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public ActualizarCotejoTomaNotaAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para insertar usuario al sistema
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ActualizarSolicitudEscritoTomaNotaRequest request)
        {
            return new List<EntidadParametro>
            {

                new EntidadParametro { Nombre = "s_id_trtn", Tipo = "Int", Valor = request.tomanota },
                new EntidadParametro { Nombre = "c_id_cat_cotejo", Tipo = "Int", Valor = request.solicitudescrito },
            };
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Método encargado de ejecutar el proceso completo del registro del tutor y del alumno
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ActualizarSolicitudEscritoTomaNotaResponse>>> Operacion(ActualizarSolicitudEscritoTomaNotaRequest request)
        {
            List<ActualizarSolicitudEscritoTomaNotaResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_insertar_usuario_sistema);
                            respuesta = await conexion.ActualizarSolicitudEscritoTomaNotaResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_insertar_usuario_sistema, tipo: "SELECT * FROM");
                            respuesta = await conexion.ActualizarSolicitudEscritoTomaNotaResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ActualizarSolicitudEscritoTomaNotaResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("ActualizarCotejoTomaNotaAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
