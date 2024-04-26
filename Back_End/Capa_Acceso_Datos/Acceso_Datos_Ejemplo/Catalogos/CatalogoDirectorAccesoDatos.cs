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

namespace Acceso_Datos.Catalogos
{
    public class CatalogoDirectorAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_lista = "religiosos.sp_consulta_lista_catalogos_director";
        private const string sp_insertar_catalogo = "religiosos.sp_insertar_catalogo_director";
        private const string sp_actualizar_catalogo = "religiosos.sp_actualizar_catalogo_director";
        private const string sp_eliminar_catalogo = "religiosos.sp_desactivar_catalogo_director";

        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos
        /// </summary>
        public CatalogoDirectorAccesoDatos()
            : base() { }
        #endregion

        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros(ConsultaListaDirectorRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "p_activo", Tipo = "Boolean", Valor = request.activos},
            };
        }

        private List<EntidadParametro> ObtenerParametrosInsertar(InsertarCatalogoDirectorRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "director_nombre", Tipo = "String", Valor = request.director_nombre??"NULL"},
               new EntidadParametro { Nombre = "director_apaterno", Tipo = "String", Valor = request.director_apaterno??"NULL"},
               new EntidadParametro { Nombre = "director_amaterno", Tipo = "String", Valor = request.director_amaterno??"NULL"},
               new EntidadParametro { Nombre = "director_titulo", Tipo = "String", Valor = request.director_titulo??"NULL"},
               new EntidadParametro { Nombre = "director_cargo", Tipo = "String", Valor = request.director_cargo??"NULL"},

            };
        }

        private List<EntidadParametro> ObtenerParametrosModificar(ActualizarDirectorRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "director_id", Tipo = "Int", Valor = request.director_id==null? "NULL":request.director_id},
               new EntidadParametro { Nombre = "director_nombre", Tipo = "String", Valor = request.director_nombre??"NULL"},
               new EntidadParametro { Nombre = "director_apaterno", Tipo = "String", Valor = request.director_apaterno??"NULL"},
               new EntidadParametro { Nombre = "director_amaterno", Tipo = "String", Valor = request.director_amaterno??"NULL"},
               new EntidadParametro { Nombre = "director_titulo", Tipo = "String", Valor = request.director_titulo??"NULL"},
               new EntidadParametro { Nombre = "director_cargo", Tipo = "String", Valor = request.director_cargo??"NULL"},

            };
        }

        private List<EntidadParametro> ObtenerParametrosEliminar(EliminarDirectorRequest request)
        {
            return new List<EntidadParametro>
            {
               new EntidadParametro { Nombre = "director_id", Tipo = "Int", Valor = request.director_id==null? "NULL":request.director_id},
               new EntidadParametro { Nombre = "director_cargo", Tipo = "Boolean", Valor = request.director_activo == true ? true: false },

            };
        }

        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Método encargado 
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaListaCatalogoDirectorResponse>>> Consultar(ConsultaListaDirectorRequest request)
        {
            List<ConsultaListaCatalogoDirectorResponse> respuesta = new List<ConsultaListaCatalogoDirectorResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametros(request), sp_consulta_lista);
                            respuesta = await conexion.ConsultaListaCatalogoDirectorResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametros(request), sp_consulta_lista, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultaListaCatalogoDirectorResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultaListaCatalogoDirectorResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("CatalogoDirectorAccesoDatos Consultar", ex);
                throw;
            }
        }

        public async Task<ResponseGeneric<List<InsertarCatalogoDirectorResponse>>> Insertar(InsertarCatalogoDirectorRequest[] request)
        {
            List<InsertarCatalogoDirectorResponse> respuesta = new List<InsertarCatalogoDirectorResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            foreach (var item in request)
                            {
                                var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosInsertar(item), sp_insertar_catalogo);
                                var records = await conexion.InsertarCatalogoDirectorResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                                respuesta.Add(records[0]);
                            }
                            break;

                        case 2:
                            foreach (var item in request)
                            {
                                var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosInsertar(item), sp_insertar_catalogo, tipo: "SELECT * FROM");
                                var records = await conexion.InsertarCatalogoDirectorResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                                respuesta.Add(records[0]);
                            }

                            break;
                    }
                }

                return new ResponseGeneric<List<InsertarCatalogoDirectorResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("CatalogoDirectorAccesoDatos Insertar", ex);
                throw;
            }
        }

        public async Task<ResponseGeneric<List<ActualizarDirectorResponse>>> Modificar(ActualizarDirectorRequest request)
        {
            List<ActualizarDirectorResponse> respuesta = new List<ActualizarDirectorResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosModificar(request), sp_actualizar_catalogo);
                            respuesta = await conexion.ActualizarDirectorResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosModificar(request), sp_actualizar_catalogo, tipo: "SELECT * FROM");
                            respuesta = await conexion.ActualizarDirectorResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ActualizarDirectorResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("CatalogoDirectorAccesoDatos Modificar", ex);
                throw;
            }
        }

        public async Task<ResponseGeneric<List<EliminarDirectorResponse>>> Eliminar(EliminarDirectorRequest request)
        {
            List<EliminarDirectorResponse> respuesta = new List<EliminarDirectorResponse>();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(ObtenerParametrosEliminar(request), sp_eliminar_catalogo);
                            respuesta = await conexion.EliminarDirectorResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(ObtenerParametrosEliminar(request), sp_eliminar_catalogo, tipo: "SELECT * FROM");
                            respuesta = await conexion.EliminarDirectorResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<EliminarDirectorResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("CatalogoDirectorAccesoDatos Eliminar", ex);
                throw;
            }
        }
        #endregion

    }
}
