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
    public class InsertarUsuarioSistemaAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_insertar_usuario_sistema = "religiosos.sp_insertar_usuario_sistema";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public InsertarUsuarioSistemaAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para insertar usuario al sistema
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(InsertarUsuarioSistemaRequest request)
        {
            return new List<EntidadParametro>
            {
                 new EntidadParametro { Nombre = "p_nombre", Tipo = "String", Valor = request.nombre == null ? "NULL" : request.nombre.ToString() },
                 new EntidadParametro { Nombre = "p_apellido_p", Tipo = "String", Valor = request.apellido_p == null ? "NULL" : request.apellido_p.ToString() },
                 new EntidadParametro { Nombre = "p_apellido_m", Tipo = "String", Valor = request.apellido_m == null ? "NULL" : request.apellido_m.ToString() },
                 new EntidadParametro { Nombre = "telefono_movil", Tipo = "String", Valor = request.telefono_movil == null ? "NULL" : request.telefono_movil.ToString() },
                 new EntidadParametro { Nombre = "correo_electronico", Tipo = "String", Valor = request.correo_electronico == null ? "NULL" : request.correo_electronico.ToString() },
                 new EntidadParametro { Nombre = "usuario", Tipo = "String", Valor = request.usuario == null ? "NULL" : request.usuario.ToString() },
                 new EntidadParametro { Nombre = "contrasena", Tipo = "Text", Valor = request.contrasena == null ? "NULL" : request.contrasena.ToString() },
                 new EntidadParametro { Nombre = "b_privacidad", Tipo = "Boolean", Valor = request.b_privacidad  },
                 //new EntidadParametro { Nombre = "id_ca_perfiles", Tipo = "Int", Valor = request.id_ca_perfiles == null ? "NULL" : request.id_ca_perfiles.Value }
            };
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Método encargado de ejecutar el proceso completo del registro del tutor y del alumno
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<InsertarUsuarioSistemaResponse>>> Operacion(InsertarUsuarioSistemaRequest request)
        {
            List<InsertarUsuarioSistemaResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_insertar_usuario_sistema);
                            respuesta = await conexion.InsertarUsuarioSistemaResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_insertar_usuario_sistema, tipo: "SELECT * FROM");
                            respuesta = await conexion.InsertarUsuarioSistemaResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<InsertarUsuarioSistemaResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("InsertarUsuarioSistemaAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
