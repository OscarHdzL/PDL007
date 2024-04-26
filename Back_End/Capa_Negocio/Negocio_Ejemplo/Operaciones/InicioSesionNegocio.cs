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
    public class InicioSesionNegocio : BaseNegocio
    {
        #region Propidades
        private readonly InicioSesionAccesoDatos inicioAccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial del inicio de sesion en negocio
        /// </summary>
        public InicioSesionNegocio()
            : base ()
        {
            inicioAccesoDatos = new InicioSesionAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar los datos para el inicio de session
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<InicioSesionResponse>>> Consultar(InicioSesionRequest entidad)
        {
            try
            {
                return await inicioAccesoDatos.Consultar(entidad);
            }
            catch (Exception ex)
            {
                LogErrores("InicioSesion - Consulta", ex);
                throw;
            }
        }
        #endregion
    }
}
