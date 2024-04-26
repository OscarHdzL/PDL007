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
    public class ConsultaListaCatalogoCotejoNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ConsultaListaCatalogoCotejoAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public ConsultaListaCatalogoCotejoNegocio()
            : base()
        {
            _AccesoDatos = new ConsultaListaCatalogoCotejoAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar los datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaListaCatalogoCotejoResponse>>> Consultar(CatalogoCotejoListaRequest request)
        {
            try
            {
                return await _AccesoDatos.Consultar(request);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaListaConvoactoriasNegocio - Consultar", ex);
                throw;
            }
        }
        #endregion
    }
}
