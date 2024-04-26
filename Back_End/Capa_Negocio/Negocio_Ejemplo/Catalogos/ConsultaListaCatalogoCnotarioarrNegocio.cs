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
    public class ConsultaListaCatalogoCnotarioarrNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ConsultaListaCatalogoCnotarioarrAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public ConsultaListaCatalogoCnotarioarrNegocio()
            : base()
        {
            _AccesoDatos = new ConsultaListaCatalogoCnotarioarrAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar los datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaListaCatalogoCnotarioarrResponse>>> Consultar(int? tipoSolicitud)
        {
            try
            {
                return await _AccesoDatos.Consultar(tipoSolicitud);
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
