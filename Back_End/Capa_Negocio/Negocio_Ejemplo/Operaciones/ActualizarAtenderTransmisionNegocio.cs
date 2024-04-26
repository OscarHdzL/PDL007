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
    public class ActualizarAtenderTransmisionNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ActualizarAtenderTransmisionAccesoDatos _accesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public ActualizarAtenderTransmisionNegocio() : base()
        {
            _accesoDatos = new ActualizarAtenderTransmisionAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Procesar encargado ejecutar el guardado de todo el registro completo
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ActualizarAtenderTransmisionResponse>>> Operacion(ActualizarAtenderTransmisionRequest request)
        {
            try
            {
                return await _accesoDatos.Operacion(request);
            }
            catch (Exception ex)
            {
                LogErrores("ActualizarAtenderTransmisionAccesoDatos - Operacion", ex);
                throw;
            }
        }
        #endregion

    }
}
