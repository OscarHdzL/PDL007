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
    public class BorraCatalogoCredoNegocio : BaseNegocio
    {
        #region Propidades
        private readonly BorraCatalogoCredoAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public BorraCatalogoCredoNegocio()
            : base()
        {
            _AccesoDatos = new BorraCatalogoCredoAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<BorraCatalogoCredoResponse>>> Operacion(BorraCatalagoCredoRequest entidad)
        {
            try
            {
                return await _AccesoDatos.Operacion(entidad);
            }
            catch (Exception ex)
            {
                LogErrores("BorraCatalogoCredoNegocio - Operacion", ex);
                throw;
            }
        }
        #endregion
    }
}
