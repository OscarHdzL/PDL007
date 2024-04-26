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
    public class ActualizarTomaNotaDomicilioLegalNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ActualizarTomaNotaDomicilioLegalAccesoDatos _accesoAdatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public ActualizarTomaNotaDomicilioLegalNegocio()
            : base()
        {
            _accesoAdatos = new ActualizarTomaNotaDomicilioLegalAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Procesar encargado ejecutar el guardado de todo el registro completo
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ActualizarTomaNotaDomicilioResponse>>> Operacion(ActualizarDomicilioRequest request)
        {
            try
            {
                return await _accesoAdatos.Operacion(request);
            }
            catch (Exception ex)
            {
                LogErrores("ActualizarTomaNotaDomicilioNegocio - Operacion", ex);
                throw;
            }
        }
        #endregion
    }
}
