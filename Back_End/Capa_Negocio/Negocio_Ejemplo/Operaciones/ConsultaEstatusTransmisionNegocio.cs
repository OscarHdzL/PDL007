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
    public class ConsultaEstatusTransmisionNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ConsultaEstatusTransmisionAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public ConsultaEstatusTransmisionNegocio() : base()
        {
            _AccesoDatos = new ConsultaEstatusTransmisionAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar los datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaEstatusTransmisionResponse>>> Consulta(ConsultaEstatusTransmisionRequest request)
        {
            try
            {
                return await _AccesoDatos.Consultar(request);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaEstatusTransmisionNegocio - Consulta", ex);
                throw;
            }
        }

        public async Task<ResponseGeneric<List<ConsultaEstatusTransmisionResponse>>> ConsultaFiltrado(ConsultaEstatusTransmisionFiltradoRequest request)
        {
            try
            {
                return await _AccesoDatos.ConsultarFiltrado(request);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaEstatusTransmisionNegocio - Consulta", ex);
                throw;
            }
        }

        

        #endregion
    }
}
