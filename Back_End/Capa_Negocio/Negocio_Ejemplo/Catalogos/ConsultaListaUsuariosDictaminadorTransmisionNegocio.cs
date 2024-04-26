using Acceso_Datos.Base;
using Acceso_Datos.Catalogos;
using Modelos.Modelos.Response;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Catalogos
{
    public class ConsultaListaUsuariosDictaminadorTransmisionNegocio : BaseAccesoDatos
    {
        #region Propidades
        private readonly ConsultaListaUsuariosDictaminadorTransmisionAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public ConsultaListaUsuariosDictaminadorTransmisionNegocio() : base()
        {
            _AccesoDatos = new ConsultaListaUsuariosDictaminadorTransmisionAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar los datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaListaUsuariosDictaminadorTransmisionResponse>>> Consultar()
        {
            try
            {
                return await _AccesoDatos.Consultar();
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaListaUsuariosDictaminadorTransmisionNegocio - Consultar", ex);
                throw;
            }
        }
        #endregion
    }
}
