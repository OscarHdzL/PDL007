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
    public class EliminarRegistroTransmisionNegocio :BaseNegocio
    {
        #region Propidades
        private readonly EliminarRegistroTransmisionAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public EliminarRegistroTransmisionNegocio()
            : base()
        {
            _AccesoDatos = new EliminarRegistroTransmisionAccesoDatos();
        }
        #endregion

        #region Métodos Publicos

        /// <summary>
        /// Metodo encargado de eliminar el registro
        /// </summary>
        /// <param name="id_tramite"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<EliminarRegistroTransmisionResponse>>> EliminarRegistro(EliminarRegistroTransmisionRequest request)
        {
            try
            {
                return await _AccesoDatos.EliminarRegistro(request);
            }
            catch (Exception ex)
            {
                LogErrores("EliminarRegistroTransmisionResponse ", ex);
                throw;
            }
        }
        #endregion

    }
}
