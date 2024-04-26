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
    public class ConsultaListaCatalogoColoniaNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ConsultaListaCatalogoColoniaAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public ConsultaListaCatalogoColoniaNegocio()
            : base()
        {
            _AccesoDatos = new ConsultaListaCatalogoColoniaAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar los datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaListaCatalogoColoniaResponse>>> Consultar(ConsultaListaCatalogoColoniaRequest request)
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
        /// Consultar los datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaListaCatalogoColoniaResponse>>> Consultar(ConsultaListaCatalogoColoniaRequest request, DtParametersrequest parametersrequest)
        {
            try
            {
                return await _AccesoDatos.Consultar(request, parametersrequest);
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
        public async Task<ResponseGeneric<List<ConteoColoniaResponse>>> Conteo(ConsultaListaCatalogoColoniaRequest request, DtParametersrequest parametersrequest)
        {
            try
            {
                return await _AccesoDatos.Conteo(request,parametersrequest);
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
