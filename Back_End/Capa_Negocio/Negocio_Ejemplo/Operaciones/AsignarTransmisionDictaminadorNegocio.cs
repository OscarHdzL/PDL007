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
    public class AsignarTransmisionDictaminadorNegocio : BaseNegocio
    {
        #region Propidades
        private readonly AsignarTransmisionDictaminadorAccesoDatos AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public AsignarTransmisionDictaminadorNegocio() : base()
        {
            AccesoDatos = new AsignarTransmisionDictaminadorAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        public async Task<ResponseGeneric<List<AsignarTransmisionDictaminadorResponse>>> Operacion(AsignarTransmisionDictaminadorRequest request)
        {
            try
            {
                return await AccesoDatos.Operacion(request);
            }
            catch (Exception ex)
            {
                LogErrores("AsignarTransmisionDictaminadorNegocio - Operacion", ex);
                throw;
            }
        }
        #endregion
    }
}
