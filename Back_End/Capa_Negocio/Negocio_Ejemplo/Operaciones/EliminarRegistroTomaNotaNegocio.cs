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
    public class EliminarRegistroTomaNotaNegocio :BaseNegocio
    {
        #region Propidades
        private readonly EliminarRegistroTomaNotaAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public EliminarRegistroTomaNotaNegocio()
            : base()
        {
            _AccesoDatos = new EliminarRegistroTomaNotaAccesoDatos();
        }
        #endregion

        #region Métodos Publicos

        /// <summary>
        /// Metodo encargado de eliminar el registro de toma de nota
        /// </summary>
        /// <param name="id_tnota"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<EliminarRegistroTomaNotaResponse>>> EliminarRegistro(EliminarRegistroTomaNotaRequest request)
        {
            try
            {
                return await _AccesoDatos.EliminarRegistro(request);
            }
            catch (Exception ex)
            {
                LogErrores("EliminarRegistrosResponse ", ex);
                throw;
            }
        }
        #endregion
    }
}
