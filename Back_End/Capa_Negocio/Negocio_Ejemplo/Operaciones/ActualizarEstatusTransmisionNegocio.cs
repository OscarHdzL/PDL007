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
    public class ActualizarEstatusTransmisionNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ActualizarEstatusTransmisionAccesoDatos _accesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public ActualizarEstatusTransmisionNegocio() : base()
        {
            _accesoDatos = new ActualizarEstatusTransmisionAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        public async Task<ResponseGeneric<List<ActualizarEstatusTransmisionResponse>>> Operacion(ActualizarEstatusTransmisionRequest request)
        {
            try
            {
                return await _accesoDatos.Operacion(request);
            }
            catch (Exception ex)
            {
                LogErrores("ActualizarEstatusTransmisionNegocio - Operacion", ex);
                throw;
            }
        }
        #endregion
    }
}
