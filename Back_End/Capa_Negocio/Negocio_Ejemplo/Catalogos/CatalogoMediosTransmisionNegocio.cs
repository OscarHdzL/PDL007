using Acceso_Datos.Catalogos;
using Modelos.Modelos.Request;
using Modelos.Modelos.Response;
using Modelos.Response;
using Negocio.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Catalogos
{
    public class CatalogoMediosTransmisionNegocio : BaseNegocio
    {
        #region Propidades
        private readonly CatalogoMediosTransmisionAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public CatalogoMediosTransmisionNegocio()
            : base()
        {
            _AccesoDatos = new CatalogoMediosTransmisionAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar los datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<CatalogoMediosTransmisionResponse>>> Consultar(CatalogoMediosTransmisionResponse model)
        {
            try
            {
                return await _AccesoDatos.Consultar(model);
            }
            catch (Exception ex)
            {
                LogErrores("CatalogoMediosTransmision - Consultar", ex);
                throw;
            }
        }

        public async Task<ResponseGeneric<List<CatalogoMediosTransmisionResponse>>> Operacion(CatalogoMediosTransmisionResponse entidad)
        {
            try
            {
                return await _AccesoDatos.Operacion(entidad);
            }
            catch (Exception ex)
            {
                LogErrores("InsertarCatalogoColoniaNegocio - Operacion", ex);
                throw;
            }
        }

        public async Task<ResponseGeneric<List<ActualizarMediosTransmisionResponse>>> Modificar(ActualizarMediosTransmisionRequest entidad)
        {
            try
            {
                return await _AccesoDatos.Modificar(entidad);
            }
            catch (Exception ex)
            {
                LogErrores("CatalogoMediosTransmisionNegocio - Modificar", ex);
                throw;
            }
        }

        public async Task<ResponseGeneric<List<BorraMediosTransmisionResponse>>> Eliminar(BorraMediosTransmisionRequest entidad)
        {
            try
            {
                return await _AccesoDatos.Eliminar(entidad);
            }
            catch (Exception ex)
            {
                LogErrores("CatalogoMediosTransmisionNegocio - Eliminar", ex);
                throw;
            }
        }
        #endregion
    }
}
