using Acceso_Datos.Catalogos;
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
    /// Clase encargada de las reglas del negocio de la consultar las  plantillas de documento de transmisión
    /// </summary>
    public class ConsultaPlantillaDocTransmisionNegocio : BaseNegocio
    {
        #region Propiedades
        private readonly ConsultaPlantillaDocTransmisionAccesoDatos _accesoAdatos;
        #endregion

        #region Constructor
        /// <summary>
        /// Consttructor inical 
        /// </summary>
        /// <param name="configuration">Ioc del archivo de configuraciones </param>
        public ConsultaPlantillaDocTransmisionNegocio()
            : base()  
        {
            _accesoAdatos = new ConsultaPlantillaDocTransmisionAccesoDatos();
        }
        #endregion

        #region Operaciones
        /// <summary>
        /// Método encargado de consultar todas las plantillas para su administracion
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultarPlantillaDocTransmisionResponse>>> Consultar()
        {
            try
            {
                return await _accesoAdatos.Consultar();
            }
            catch (Exception ex)
            {
                LogErrores("BorraConvocatoriaAccesoDatos", ex);
                throw;
            }
        }

        /// <summary>
        /// Método encargado de obtener la plantilla principal activa
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultarPlantillaDocTransmisionResponse>>> ConsultarActiva()
        {
            try
            {
                return await _accesoAdatos.ConsultarActiva();
            }
            catch (Exception ex)
            {
                LogErrores("BorraConvocatoriaAccesoDatos", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ConsultarPlantillaDocTransmisionResponse>>> GetPlantilla(int idPlantilla)
        {
            try
            {
                return await _accesoAdatos.GetPlantilla(idPlantilla);
            }
            catch (Exception ex)
            {
                LogErrores("BorraConvocatoriaAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
