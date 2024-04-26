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
    public class ConsultaOficioTransmisionNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ConsultaOficioTransmisionAccesoDatos _accesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public ConsultaOficioTransmisionNegocio() : base()
        {
            _accesoDatos = new ConsultaOficioTransmisionAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        public async Task<ResponseGeneric<List<ConsultaOficioTransmisionResponse>>> Consulta(ConsultaOficioTransmisionRequest request)
        {
            try
            {
                return await _accesoDatos.Consultar(request);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaOficioTransmisionNegocio - Consulta", ex);
                throw;
            }
        }
        #endregion
    }
}
