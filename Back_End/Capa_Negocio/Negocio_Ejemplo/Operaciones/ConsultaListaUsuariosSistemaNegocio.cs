using Acceso_Datos_Ejemplo.Operaciones;
using Modelos.Modelos.Utilidades.Request;
using Modelos.Modelos.Utilidades.Response;
using Modelos.Response;
using Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio_Ejemplo.Operaciones
{
    public class ConsultaListaUsuariosSistemaNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ConsultaListaUsuariosSistemaAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial del inicio de sesion en negocio
        /// </summary>
        public ConsultaListaUsuariosSistemaNegocio()
            : base()
        {
            _AccesoDatos = new ConsultaListaUsuariosSistemaAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar los datos para el inicio de session
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaListaUsuariosSistemaResponse>>> Consultar(ConsultaListaUsuariosSistemaRequest entidad)
        {
            try
            {
                return await _AccesoDatos.Consultar(entidad);
            }
            catch (Exception ex)
            {
                LogErrores("Negocio ConsultaListaUsuariosSistemaNegocio", ex);
                throw;
            }
        }
        #endregion
    }
}
