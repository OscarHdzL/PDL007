using Acceso_Datos.Operaciones;
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
    public class ConsultaListaUsuariosDictaminadorTomaNotaNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ConsultaListaUsuariosDictaminadorTomaNotaAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public ConsultaListaUsuariosDictaminadorTomaNotaNegocio()
            : base()
        {
            _AccesoDatos = new ConsultaListaUsuariosDictaminadorTomaNotaAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar los datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaListaUsuariosDictaminadorTomaNotaResponse>>> Consultar()
        {
            try
            {
                return await _AccesoDatos.Consultar();
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaListaUsuariosDictaminadorTomaNotaNegocio - Consultar", ex);
                throw;
            }
        }
        #endregion
    }
}
