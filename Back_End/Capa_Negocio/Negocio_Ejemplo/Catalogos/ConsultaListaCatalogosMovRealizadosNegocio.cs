using Acceso_Datos.Catalogos;
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
    public class ConsultaListaCatalogosMovRealizadosNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ConsultaListaCatalogosMovRealizadosAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public ConsultaListaCatalogosMovRealizadosNegocio()
            : base()
        {
            _AccesoDatos = new ConsultaListaCatalogosMovRealizadosAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar los datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaListaCatalogosMovRealizadosResponse>>> Consultar(ConsultaListaCatalogosMovRealizadosRequest request)
        {
            try
            {
                return await _AccesoDatos.Consultar(request);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaListaCatalogosMovRealizadosNegocio - Consultar", ex);
                throw;
            }
        }
        #endregion
    }
}