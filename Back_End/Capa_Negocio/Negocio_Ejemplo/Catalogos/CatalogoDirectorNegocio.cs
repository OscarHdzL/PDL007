using Acceso_Datos.Catalogos;
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

namespace Negocio.Catalogos
{
    public class CatalogoDirectorNegocio: BaseNegocio
    {
        #region Propidades
        private readonly CatalogoDirectorAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public CatalogoDirectorNegocio()
            : base()
        {
            _AccesoDatos = new CatalogoDirectorAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar los datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaListaCatalogoDirectorResponse>>> Consultar(ConsultaListaDirectorRequest request)
        {
            try
            {
                return await _AccesoDatos.Consultar(request);
            }
            catch (Exception ex)
            {
                LogErrores("CatalogoDirectorNegocio - Consultar", ex);
                throw;
            }
        }

        public async Task<ResponseGeneric<List<InsertarCatalogoDirectorResponse>>> Insertar(InsertarCatalogoDirectorRequest[] request)
        {
            try
            {
                return await _AccesoDatos.Insertar(request);
            }
            catch (Exception ex)
            {
                LogErrores("CatalogoDirectorNegocio - Insertar", ex);
                throw;
            }
        }

        public async Task<ResponseGeneric<List<ActualizarDirectorResponse>>> Modificar(ActualizarDirectorRequest request)
        {
            try
            {
                return await _AccesoDatos.Modificar(request);
            }
            catch (Exception ex)
            {
                LogErrores("CatalogoDirectorNegocio - Modificar", ex);
                throw;
            }
        }

        public async Task<ResponseGeneric<List<EliminarDirectorResponse>>> Eliminar(EliminarDirectorRequest request)
        {
            try
            {
                return await _AccesoDatos.Eliminar(request);
            }
            catch (Exception ex)
            {
                LogErrores("CatalogoDirectorNegocio - Eliminar", ex);
                throw;
            }
        }
        #endregion

    }
}
