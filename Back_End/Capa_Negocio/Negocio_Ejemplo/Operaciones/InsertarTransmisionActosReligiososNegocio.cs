using Acceso_Datos.Operaciones;
using Modelos.Modelos.Request;
using Modelos.Modelos.Response;
using Modelos.Response;
using Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Operaciones
{
    public class InsertarTransmisionActosReligiososNegocio : BaseNegocio
    {
        #region Propidades
        private readonly InsertarTransmisionActosReligiososAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public InsertarTransmisionActosReligiososNegocio()
            : base()
        {
            _AccesoDatos = new InsertarTransmisionActosReligiososAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar los datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<InsertarTransmisionActosReligiososResponse>>> Operacion(InsertarTransmisionActosReligiososRequest entidad)
        {
            try
            {
                return await _AccesoDatos.Operacion(entidad);
            }
            catch (Exception ex)
            {
                LogErrores("InsertarTransmisionActosReligiososNegocio - Operacion", ex);
                throw;
            }
        }

        public Response GuardarFechas(InsertarActosFechasRequest entidad)
        {
            try
            {
                return  _AccesoDatos.GuardarFechas(entidad);
            }
            catch (Exception ex)
            {
                LogErrores("InsertarTransmisionActosFechasNegocio - Operacion", ex);
                throw;
            }
        }

        public Response GuardarEmisoras(InsertarActosEmisorasRequest entidad)
        {
            try
            {
                return _AccesoDatos.GuardarEmisoras(entidad);
            }
            catch (Exception ex)
            {
                LogErrores("InsertarTransmisionActosmisorasNegocio - Operacion", ex);
                throw;
            }
        }
        #endregion
    }
}
