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
using Utilidades.CifradoMd5;

namespace Negocio.Operaciones
{
    public class InsertarArchivoNegocio : BaseNegocio
    {
        #region Propidades
        private readonly InsertarArchivoAccesoDatos _accesoAdatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public InsertarArchivoNegocio()
            : base()
        {
            _accesoAdatos = new InsertarArchivoAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Procesar encargado ejecutar el guardado de todo el registro completo
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ArchivoResponse>>> Operacion(ArchivoRequest request)
        {
            try
            {
                return await _accesoAdatos.Operacion(request);
            }
            catch (Exception ex)
            {
                LogErrores("InsertarArchivoNegocio - Operacion", ex);
                throw;
            }
        }
        #endregion


    }
}