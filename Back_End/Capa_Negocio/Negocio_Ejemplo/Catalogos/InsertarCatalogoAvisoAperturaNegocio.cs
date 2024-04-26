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
    /// <summary>
    /// Clase del proceso del negocio
    /// </summary>
    public class InsertarCatalogoAvisoAperturaNegocio : BaseNegocio
    {
        #region Propidades
        private readonly InsertarCatalogoAvisoAperturaAccesoDatos _accesoAdatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public InsertarCatalogoAvisoAperturaNegocio()
            : base()
        {
            _accesoAdatos = new InsertarCatalogoAvisoAperturaAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Procesar encargado ejecutar el guardado de todo el registro completo
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<CatalogoAvisoAperturaInsertResponse>>> Operacion(CatalogoAvisoAperturaInsertRequest[] request)
        {
            try
            {
                return await _accesoAdatos.Operacion(request);
            }
            catch (Exception ex)
            {
                LogErrores("CatalogoAvisoAperturaInsertNegocio - Operacion", ex);
                throw;
            }
        }
        #endregion
    }
}
