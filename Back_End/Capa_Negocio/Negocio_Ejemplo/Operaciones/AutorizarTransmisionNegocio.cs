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
    public class AutorizarTransmisionNegocio : BaseNegocio
    {
        #region Propidades
        private readonly AutorizarTransmisionAccesoDatos _accesoDatos;
        private readonly ConsultaDetalleUsuarioSistemaAccesoDatos _accesoAdatosUsuario;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public AutorizarTransmisionNegocio() : base()
        {
            _accesoDatos = new AutorizarTransmisionAccesoDatos();
            _accesoAdatosUsuario = new ConsultaDetalleUsuarioSistemaAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        public async Task<ResponseGeneric<List<AutorizarTransmisionResponse>>> Operacion(AutorizarTransmisionRequest request)
        {
            try
            {
                return await _accesoDatos.Operacion(request);
            }
            catch (Exception ex)
            {
                LogErrores("AutorizarTransmisionAccesoDatos - Operacion", ex);
                throw;
            }
        }

        public async Task<ResponseGeneric<List<ConsultaDetalleUsuarioSistemaResponse>>> Consulta(ConsultaDetalleUsuarioSistemaRequest request)
        {
            try
            {
                return await _accesoAdatosUsuario.Consultar(request);
            }
            catch (Exception ex)
            {
                LogErrores("AutorizarTransmisionNegocio - Consulta", ex);
                throw;
            }
        }
        #endregion
    }
}
