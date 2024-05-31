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
    public class ConsultaCatalogoMovimientosTomaNotaNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ConsultaCatalogoMovimientosTomaNotaAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public ConsultaCatalogoMovimientosTomaNotaNegocio()
            : base()
        {
            _AccesoDatos = new ConsultaCatalogoMovimientosTomaNotaAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar los datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaCatalogoMovimientosTomaNotaResponse>>> Consultar(bool p_activos)
        {
            try
            {
                return await _AccesoDatos.Consultar(p_activos);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaCatalogoMovimientosTomaNotaNegocio - Consultar", ex);
                throw;
            }
        }
        #endregion
    }
}
