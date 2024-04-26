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
    public class ConsultaDetalleTramiteTransmisionNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ConsultaDetalleTramiteTransmisionAccesoDatos _accesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public ConsultaDetalleTramiteTransmisionNegocio() : base()
        {
            _accesoDatos = new ConsultaDetalleTramiteTransmisionAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        public async Task<ResponseGeneric<List<ConsultaDetalleTramiteTransmisionResponse>>> Consulta(ConsultaTramiteTransmisionRequest request)
        {
            try
            {
                return await _accesoDatos.Consultar(request);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaDetalleTramiteTransmisionNegocio - Consulta", ex);
                throw;
            }
        }
        /// <summary>
        /// Consultar los datos para el inicio de session
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaDetalleTransmisionCotejoPublicoResponse>>> ConsultarTrans(ConsultaTramiteTransmisionRequest entidad)
        {
            try
            {
                return await _accesoDatos.ConsultarTrans(entidad);
            }
            catch (Exception ex)
            {
                LogErrores("Negocio ConsultaDetalleTramiteTransmisionCotejoNegocio", ex);
                throw;
            }
        }
        #endregion
    }
}
