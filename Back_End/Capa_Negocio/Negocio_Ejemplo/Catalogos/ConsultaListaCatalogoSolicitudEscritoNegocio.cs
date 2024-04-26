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
    public class ConsultaListaCatalogoSolicitudEscritoNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ConsultaListaCatalogoTSolRegAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public ConsultaListaCatalogoSolicitudEscritoNegocio()
            : base()
        {
            _AccesoDatos = new ConsultaListaCatalogoTSolRegAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar los datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaListaCatalogoTSolRegaResponse>>> Consultar(CatalogoSolicitudEscritoListaRequest request)
        {
            try
            {
                return await _AccesoDatos.Consultar(request);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaListaCatalogoSolicitudEscritoNegocio - Consultar", ex);
                throw;
            }
        }
        #endregion
    }
}
