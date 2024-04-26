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
    /// <summary>
    /// Clase encargada del negocio de datos para borrar una plantilla de documento de transmisión
    /// </summary>
    public class BorrarPlantillaDocTransmisionNegocio : BaseNegocio
    {
        #region Propiedades
        private readonly BorrarPlantillaDocTransmisionAccesoDatos _accesoAdatos;
        #endregion

        #region Constructor
        /// <summary>
        /// Consttructor inical 
        /// </summary>
        /// <param name="configuration">Ioc del archivo de configuraciones </param>
        public BorrarPlantillaDocTransmisionNegocio()
            : base() 
        {
            _accesoAdatos = new ();
        }
        #endregion

        #region Operaciones
        public async Task<ResponseGeneric<List<BorrarPlantillaDocTransmisionResponse>>> Operacion(BorrarPlantillaDocTransmisionRequest request)
        {
            try
            {
                return await _accesoAdatos.Operacion(request);
            }
            catch (Exception ex)
            {
                LogErrores("BorrarPlantillaDocTransmisionNegocio", ex);
                throw;
            }
        }
        #endregion
    }
}
