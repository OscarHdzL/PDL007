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
    public class InsertarTramiteTransmisionNegocio : BaseNegocio
    {
        #region Propidades
        private readonly InsertarTramiteTransmisionAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public InsertarTramiteTransmisionNegocio() : base()
        {
            _AccesoDatos = new InsertarTramiteTransmisionAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar los datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<InsertarTramiteTransmisionResponse>>> Operacion(InsertarTramiteTransmisionRequest entidad)
        {
            try
            {
                return await _AccesoDatos.Operacion(entidad);
            }
            catch (Exception ex)
            {
                LogErrores("InsertarTramiteTransmisionNegocio - Operacion", ex);
                throw;
            }
        }
        #endregion
    }
}
