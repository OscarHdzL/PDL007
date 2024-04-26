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

namespace Acceso_Datos.Catalogos
{
    /// <summary>
    /// Clase encargada de las reglas del negocio de la consultar las plantilla de documento de transmisión
    /// </summary>
    public class ConsultaPlantillaDocTransmisionAccesoDatos : BaseAccesoDatos
    {
        #region SP_Operaciones
        private const string sp_consulta_lista_catalogo_plantilla = "religiosos.sp_consulta_lista_catalogo_plantilla";
        private const string sp_consulta_lista_catalogo_plantilla_activa = "religiosos.sp_consulta_lista_catalogo_plantilla_activa";
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para el acceso de datos 
        /// </summary>
        public ConsultaPlantillaDocTransmisionAccesoDatos()
            : base() { }
        #endregion


        #region Parametros SQL
        /// <summary>
        /// Método encargado de obtener los parametros para obtener los datos
        /// </summary>
        /// <param name="entidad">Entidades del request</param>
        /// <returns></returns>
        private List<EntidadParametro> ObtenerParametros()
            => new ();
        #endregion


        #region Métodos Publicos
        /// <summary>
        /// Método encargado de consultar todas las plantillas
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultarPlantillaDocTransmisionResponse>>> Consultar()
        {
            List<ConsultarPlantillaDocTransmisionResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(null, sp_consulta_lista_catalogo_plantilla);
                            respuesta = await conexion.ConsultarPlantillaDocTransmisionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(null, sp_consulta_lista_catalogo_plantilla, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultarPlantillaDocTransmisionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultarPlantillaDocTransmisionResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("BorraConvocatoriaAccesoDatos", ex);
                throw;
            }
        }

        /// <summary>
        /// Método encargado de consultar la plantilla activa
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultarPlantillaDocTransmisionResponse>>> ConsultarActiva()
        {
            List<ConsultarPlantillaDocTransmisionResponse> respuesta = new();
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(null, sp_consulta_lista_catalogo_plantilla_activa);
                            respuesta = await conexion.ConsultarPlantillaDocTransmisionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(null, sp_consulta_lista_catalogo_plantilla_activa, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultarPlantillaDocTransmisionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultarPlantillaDocTransmisionResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("BorraConvocatoriaAccesoDatos", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ConsultarPlantillaDocTransmisionResponse>>> GetPlantilla(int idPlantilla)
        {
            List<ConsultarPlantillaDocTransmisionResponse> respuesta = new();
            List<EntidadParametro> parametros = new List<EntidadParametro>();
            parametros.Add(new EntidadParametro{Nombre = "p_id_plantilla", Tipo = "Int", Valor = idPlantilla});
            try
            {
                using (var conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 1:
                            var resulMySQL = StoreProcedureParametros.ParametrosMySQL(parametros, sp_consulta_lista_catalogo_plantilla_activa);
                            respuesta = await conexion.ConsultarPlantillaDocTransmisionResponse.FromSqlRaw(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync();
                            break;

                        case 2:
                            var resulPostgreSQL = StoreProcedureParametros.ParametrosPostgreSQL(parametros, sp_consulta_lista_catalogo_plantilla_activa, tipo: "SELECT * FROM");
                            respuesta = await conexion.ConsultarPlantillaDocTransmisionResponse.FromSqlRaw(resulPostgreSQL.Query, resulPostgreSQL.ListaParametros.ToArray()).ToListAsync();
                            break;
                    }
                }

                return new ResponseGeneric<List<ConsultarPlantillaDocTransmisionResponse>>(respuesta);
            }
            catch (Exception ex)
            {
                LogErrores("BorraConvocatoriaAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}

