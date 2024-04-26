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
    public class BorraCatalogoColoniaNegocio : BaseNegocio
    {
        #region Propidades
        private readonly BorraCatalogoColoniaAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public BorraCatalogoColoniaNegocio()
            : base()
        {
            _AccesoDatos = new BorraCatalogoColoniaAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<BorraCatalogoColoniaResponse>>> Operacion(BorraCatalagoColoniaRequest entidad)
        {
            try
            {
                return await _AccesoDatos.Operacion(entidad);
            }
            catch (Exception ex)
            {
                LogErrores("BorraCatalogoColoniaNegocio - Operacion", ex);
                throw;
            }
        }
        #endregion
    }
}
