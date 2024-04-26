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
    public class ConsultaListaCatalogosTMovimientosNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ConsultaListaCatalogosTMovimientosAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public ConsultaListaCatalogosTMovimientosNegocio()
            : base()
        {
            _AccesoDatos = new ConsultaListaCatalogosTMovimientosAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar los datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaListaCatalogosTMovimientosResponse>>> Consultar(ConsultaListaCatalogosTMovimientosRequest request)
        {
            try
            {
                return await _AccesoDatos.Consultar(request);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaListaCatalogosTMovimientosNegocio - Consultar", ex);
                throw;
            }
        }
        #endregion
    }
}
