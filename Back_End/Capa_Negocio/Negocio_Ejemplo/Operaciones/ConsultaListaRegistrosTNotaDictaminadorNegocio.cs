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
    public class ConsultaListaRegistrosTNotaDictaminadorNegocio: BaseNegocio
    {
        #region Propidades
        private readonly ConsultaListaRegistrosTNotaDictaminadorAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public ConsultaListaRegistrosTNotaDictaminadorNegocio()
            : base()
        {
            _AccesoDatos = new ConsultaListaRegistrosTNotaDictaminadorAccesoDatos();
        }
        #endregion

        #region Métodos Publicos

        /// <summary>
        /// Metodo encargado de obtener los registros
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaListaRegistrosTNotaDictaminadorResponse>>> ConsultaRegistros(ConsultaListaRegistrosTNotaDictaminadorRequest request)
        {
            try
            {
                return await _AccesoDatos.ConsultaRegistros(request);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaListaRegistrosTNotaDictaminadorResponse - ConsultaRegistros", ex);
                throw;
            }
        }
        #endregion
    }
}
