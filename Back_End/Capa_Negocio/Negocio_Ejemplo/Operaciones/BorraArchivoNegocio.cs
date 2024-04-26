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
    public class BorraArchivoNegocio : BaseNegocio
    {
        #region Propidades
        private readonly BorraArchivoAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public BorraArchivoNegocio()
            : base()
        {
            _AccesoDatos = new BorraArchivoAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<ArchivoResponse>> Operacion(ArchivoRequest request)
        {
            try
            {
                return await _AccesoDatos.Operacion(request);
            }
            catch (Exception ex)
            {
                LogErrores("BorraArchivoNegocio - Operacion", ex);
                throw;
            }
        }
        #endregion
    }
}