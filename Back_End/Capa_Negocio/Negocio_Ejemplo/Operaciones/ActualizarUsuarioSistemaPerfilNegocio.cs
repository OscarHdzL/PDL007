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
    public class ActualizarUsuarioSistemaPerfilNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ActualizarUsuarioSistemaPerfilAccesoDatos _accesoAdatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public ActualizarUsuarioSistemaPerfilNegocio()
            : base()
        {
            _accesoAdatos = new ActualizarUsuarioSistemaPerfilAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Procesar encargado ejecutar el guardado de todo el registro completo
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ActualizarUsuarioSistemaResponse>>> Operacion(ActualizarUsuarioSistemaRequest request)
        {
            try
            {
                return await _accesoAdatos.Operacion(request);
            }
            catch (Exception ex)
            {
                LogErrores("Negocio ActualizarUsuarioSistemaPerfilNegocio-Operacion", ex);
                throw;
            }
        }
        #endregion
    }
}
