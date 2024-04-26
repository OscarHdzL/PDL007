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
    public class InsertarCatalogoPaisoNegocio : BaseNegocio
    {
        #region Propidades
        private readonly InsertarCatalogoPaisoAccesoDatos _accesoAdatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public InsertarCatalogoPaisoNegocio()
            : base()
        {
            _accesoAdatos = new InsertarCatalogoPaisoAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Procesar encargado ejecutar el guardado de todo el registro completo
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<CatalogoPaisoInsertResponse>>> Operacion(CatalogoPaisoInsertRequest[] request)
        {
            try
            {
                return await _accesoAdatos.Operacion(request);
            }
            catch (Exception ex)
            {
                LogErrores("CatalogoCredoInsertNegocio - Operacion", ex);
                throw;
            }
        }
        #endregion
    }
}
