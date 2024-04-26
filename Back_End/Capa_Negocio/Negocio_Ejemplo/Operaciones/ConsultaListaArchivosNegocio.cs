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
    /// <summary>
    /// Clase del negocio encargado de consultar la lista de los archivos.
    /// </summary>
    public class ConsultaListaArchivosNegocio : BaseNegocio
    {
        #region Propiedades
        readonly ConsultaListaArchivosAccesoDatos _accesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor base
        /// </summary>
        public ConsultaListaArchivosNegocio() : base()
        {
            _accesoDatos = new ConsultaListaArchivosAccesoDatos();
        }
        #endregion

        #region Métodos publicos
        /// <summary>
        /// Método encargado consultar la lista de archivos
        /// </summary>
        /// <param name="request">Objeto de tranporte de la solicitud</param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaArchivosListaResponse>>> Consultar(ArchivosRequest request)
        {
            try
            {
                return await _accesoDatos.Consultar(request);
            }
            catch (Exception ex)
            {
                LogErrores("Negocio ConsultaHorariosBloqueoNegocio", ex);
                throw;
            }
        }
        #endregion

    }
}
