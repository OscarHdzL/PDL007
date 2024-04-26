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
    /// <summary>
    /// Clase del proceso del negocio
    /// </summary>
    public class FinalizarTramiteNegocio : BaseNegocio
    {
        #region Propidades
        private readonly FinalizarTramiteAccesoDatos _accesoAdatos;
        private readonly ConsultaDetalleUsuarioSistemaAccesoDatos _accesoAdatosUsuario;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public FinalizarTramiteNegocio()
            : base()
        {
            _accesoAdatos = new FinalizarTramiteAccesoDatos();
            _accesoAdatosUsuario = new ConsultaDetalleUsuarioSistemaAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Procesar encargado ejecutar el guardado de todo el registro completo
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<FinalizarTramiteResponse>>> Operacion(FinalizarTramiteRequest request)
        {
            try
            {
                return await _accesoAdatos.Operacion(request);
            }
            catch (Exception ex)
            {
                LogErrores("FinalizarTramiteSextoNegocio - Operacion", ex);
                throw;
            }
        }
        
        /// <summary>
        /// Obtener Usuario Envío confirmación finalización trámite
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaDetalleUsuarioSistemaResponse>>> Consulta(ConsultaDetalleUsuarioSistemaRequest request)
        {
            try
            {
                return await _accesoAdatosUsuario.Consultar(request);
            }
            catch (Exception ex)
            {
                LogErrores("FinalizarTramiteSextoNegocio - Operacion", ex);
                throw;
            }
        }
        #endregion
    }
}
