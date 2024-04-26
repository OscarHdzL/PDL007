using Acceso_Datos.Catalogos;
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

namespace Negocio.Catalogos
{
    public class ConsultaListaTramitesDictaminadorNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ConsultaListaTramitesDictaminadorAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public ConsultaListaTramitesDictaminadorNegocio()
            : base()
        {
            _AccesoDatos = new ConsultaListaTramitesDictaminadorAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar los datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaListaTramitesResponse>>> Consultar(ConsultaListaTramitesDictaminadorgetRequest request)
        {
            try
            {
                return await _AccesoDatos.Consultar(request);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaListaConvoactoriasNegocio - Consultar", ex);
                throw;
            }
        }
        /// <summary>
        /// Conteo de los datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConteoTramitesResponse>>> Conteo(ConsultaListaTramitesDictaminadorRequest request)
        {
            try
            {
                return await _AccesoDatos.Conteo(request);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaListaConvoactoriasNegocio - Consultar", ex);
                throw;
            }
        }
        #endregion
    }
}
