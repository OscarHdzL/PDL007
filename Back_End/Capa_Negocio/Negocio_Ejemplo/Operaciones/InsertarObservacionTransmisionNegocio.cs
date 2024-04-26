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
    public class InsertarObservacionTransmisionNegocio : BaseNegocio
    {
        #region Propidades
        private readonly InsertarObservacionTransmisionAccesoDatos _accesoDatos;
        private readonly ConsultaDetalleUsuarioSistemaAccesoDatos _accesoAdatosUsuario;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public InsertarObservacionTransmisionNegocio() : base()
        {
            _accesoDatos = new InsertarObservacionTransmisionAccesoDatos();
            _accesoAdatosUsuario = new ConsultaDetalleUsuarioSistemaAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        public async Task<ResponseGeneric<List<InsertarObservacionTransmisionResponse>>> Operacion(InsertarObservacionTransmisionRequest request)
        {
            try
            {
                return await _accesoDatos.Operacion(request);
            }
            catch (Exception ex)
            {
                LogErrores("InsertarObservacionTransmisionNegocio - Operacion", ex);
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
                LogErrores("InsertarObservacionTransmisionNegocio - Consulta", ex);
                throw;
            }
        }
        #endregion
    }
}
