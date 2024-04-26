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
    public class ConsultaDetalleMovimientosTomaNotaNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ConsultaDetalleMovimientosTomaNotaAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public ConsultaDetalleMovimientosTomaNotaNegocio()
            : base()
        {
            _AccesoDatos = new ConsultaDetalleMovimientosTomaNotaAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar los datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaDetalleMovimientosTomaNotaResponse>>> Consultar(ConsultaDetalleTomaNotaRequest request)
        {
            try
            {
                return await _AccesoDatos.Consultar(request);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaDetalleMovimientosTomaNotaNegocio - Consultar", ex);
                throw;
            }
        }
        #endregion
    }
}
