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
    public class ConsultaDetalleTomaNotaNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ConsultaDetalleTomaNotaAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial del inicio de sesion en negocio
        /// </summary>
        public ConsultaDetalleTomaNotaNegocio()
            : base()
        {
            _AccesoDatos = new ConsultaDetalleTomaNotaAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar los datos para el inicio de session
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaDetalleTomaNotaInfoResponse>>> Consultar(ConsultaDetalleTomaNotaRequest entidad)
        {
            try
            {
                return await _AccesoDatos.Consultar(entidad);
            }
            catch (Exception ex)
            {
                LogErrores("Negocio ConsultaDetalleTomaNotaNegocio", ex);
                throw;
            }
        }

        /// <summary>
        /// Consultar los datos para el inicio de session
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ResponseGenerico>>> OperacionMovimientos(RequestParamMovimientos entidad)
        {
            try
            {
                return await _AccesoDatos.OperacionMovimientos(entidad);
            }
            catch (Exception ex)
            {
                LogErrores("Negocio ConsultaDetalleTomaNotaNegocio", ex);
                throw;
            }
        }
        #endregion
    }
}
