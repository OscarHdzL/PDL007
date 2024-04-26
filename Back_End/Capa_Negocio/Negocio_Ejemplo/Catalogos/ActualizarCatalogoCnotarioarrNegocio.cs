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
    public class ActualizarCatalogoCnotarioarrNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ActualizarCatalogoCnotarioarrAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public ActualizarCatalogoCnotarioarrNegocio()
            : base()
        {
            _AccesoDatos = new ActualizarCatalogoCnotarioarrAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ActualizarCatalogoCnotarioarrResponse>>> Operacion(ActualizarCatalogoCnotarioarrRequest entidad)
        {
            try
            {
                return await _AccesoDatos.Operacion(entidad);
            }
            catch (Exception ex)
            {
                LogErrores("ActualizarCatalogoCnotarioarrNegocio - Operacion", ex);
                throw;
            }
        }
        #endregion
    }
}
