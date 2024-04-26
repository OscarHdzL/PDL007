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
    public class ActualizarCatalogoColoniaNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ActualizarCatalogoColoniaAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public ActualizarCatalogoColoniaNegocio()
            : base()
        {
            _AccesoDatos = new ActualizarCatalogoColoniaAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ActualizarCatalogoColoniaResponse>>> Operacion(ActualizarCatalogoColoniaRequest entidad)
        {
            try
            {
                return await _AccesoDatos.Operacion(entidad);
            }
            catch (Exception ex)
            {
                LogErrores("ActualizarCatalogoColoniaNegocio - Operacion", ex);
                throw;
            }
        }
        #endregion
    }
}
