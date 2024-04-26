using Acceso_Datos.Operaciones;
using Modelos.Modelos.Request;
using Modelos.Modelos.Response;
using Modelos.Modelos.Utilidades.Request;
using Modelos.Modelos.Utilidades.Response;
using Modelos.Response;
using Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Operaciones
{
    /// <summary>
    /// Clase del proceso del negocio
    /// </summary>
    public class ConfirmarCorreoNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ConfirmarCorreoAccesoDatos _accesoAdatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public ConfirmarCorreoNegocio()
            : base()
        {
            _accesoAdatos = new ConfirmarCorreoAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Procesar encargado ejecutar el guardado de todo el registro completo
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConfirmarCorreoResponse>>> Operacion(ConfirmarCorreoRequest request)
        {
            try
            {
                return await _accesoAdatos.Operacion(request);
            }
            catch (Exception ex)
            {
                LogErrores("ConfirmarCorreoNegocio - Operacion", ex);
                throw;
            }
        }
        #endregion
    }
}
