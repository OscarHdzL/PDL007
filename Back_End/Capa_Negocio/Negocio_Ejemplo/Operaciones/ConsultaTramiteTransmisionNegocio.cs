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
    public class ConsultaTramiteTransmisionNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ConsultaTramiteTransmisionAccesoDatos _accesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public ConsultaTramiteTransmisionNegocio() : base()
        {
            _accesoDatos = new ConsultaTramiteTransmisionAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        public async Task<ResponseGeneric<List<ConsultaTramiteTransmisionResponse>>> Consulta(ConsultaTramiteTransmisionRequest request)
        {
            try
            {
                return await _accesoDatos.Consultar(request);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaTramiteTransmisionNegocio - Consulta", ex);
                throw;
            }
        }
        #endregion
    }
}
