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
    public class ConsultaListaTomaNotaDictaminadorNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ConsultaListaTomaNotaDictaminadorAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public ConsultaListaTomaNotaDictaminadorNegocio()
            : base()
        {
            _AccesoDatos = new ConsultaListaTomaNotaDictaminadorAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar los datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaListaTomaNotaResponse>>> Consultar(ConsultaListaTomaNotaDictaminadorRequest request, DtParametersrequest parametersrequest)
        {
            try
            {
                return await _AccesoDatos.Consultar(request, parametersrequest);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaListaTomaNotaDictaminadorAccesoDatos - Consultar", ex);
                throw;
            }
        }
        /// <summary>
        /// Conteo de los datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConteoTomaNotaResponse>>> Conteo(ConsultaListaTomaNotaDictaminadorRequest request)
        {
            try
            {
                return await _AccesoDatos.Conteo(request);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaListaTomaNotaDictaminadorAccesoDatos - Consultar", ex);
                throw;
            }
        }
        #endregion
    }
}
