using Acceso_Datos.Operaciones;
using Modelos.Modelos.Request;
using Modelos.Modelos.Response;
using Modelos.Modelos.Utilidades.Request;
using Modelos.Response;
using Negocio.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Operaciones
{
    public class RecuperarContrasenaNegocio : BaseNegocio
    {

        #region Propidades
        private readonly RecuperarContrasenaAccesoDatos recuperaContraAccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial del inicio de sesion en negocio
        /// </summary>
        public RecuperarContrasenaNegocio()
            : base()
        {
            recuperaContraAccesoDatos = new RecuperarContrasenaAccesoDatos();
        }
        #endregion
        #region Métodos Publicos

        public async Task<ResponseGeneric<List<RecuperarContrasenaResponse>>> Consultar(ConsultarCorreoRequest entidad)
        {
            try
            {
                return await recuperaContraAccesoDatos.Consultar(entidad);
            }
            catch (Exception ex)
            {
                LogErrores("RecuperarContrasenaNegocio - Consulta", ex);
                throw;
            }
        }
        public async Task<ResponseGeneric<List<RecuperarContrasenaResponse>>> Operacion(RecuperarContrasenaRequest entidad, int? UsuarioId)
        {
            try
            {
                return await recuperaContraAccesoDatos.Operacion(entidad, UsuarioId);
            }
            catch (Exception ex)
            {
                LogErrores("RecuperarContrasenaNegocio - Operacion", ex);
                throw;
            }
        }



        #endregion

    }
}
