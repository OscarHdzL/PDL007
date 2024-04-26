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
    public class ConsultaListaTomaNotaNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ConsultaListaTomaNotaAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public ConsultaListaTomaNotaNegocio()
            : base()
        {
            _AccesoDatos = new ConsultaListaTomaNotaAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar los datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaListaTomaNotaResponse>>> Consultar(ConsultaListaTomaNotaRequest request, DtParametersrequest parametersrequest)
        {
            try
            {
                return await _AccesoDatos.Consultar(request, parametersrequest);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaListaTomaNotaNegocio - Consultar", ex);
                throw;
            }
        }
        /// <summary>
        /// Conteo de los datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConteoTomaNotaResponse>>> Conteo(ConsultaListaTomaNotaRequest request)
        {
            try
            {
                return await _AccesoDatos.Conteo(request);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaListaTomaNotaNegocio - Consultar", ex);
                throw;
            }
        }
        #endregion
    }
}
