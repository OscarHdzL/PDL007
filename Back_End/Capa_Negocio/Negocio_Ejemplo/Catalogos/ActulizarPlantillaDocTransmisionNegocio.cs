using Acceso_Datos.Catalogos;
using Microsoft.Extensions.Configuration;
using Modelos.Modelos.Request;
using Modelos.Modelos.Response;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades.FileManager;

namespace Negocio.Catalogos
{
    /// <summary>
    /// Clase encargada de las reglas del negocio de la actulizar una plantilla de documento de transmisión
    /// </summary>
    public class ActulizarPlantillaDocTransmisionNegocio : Base.BaseNegocio
    {
        #region Propiedades
        private readonly ActulizarPlantillaDocTransmisionAccesoDatos _accesoAdatos;
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        /// <summary>
        /// Consttructor inical 
        /// </summary>
        /// <param name="configuration">Ioc del archivo de configuraciones </param>
        public ActulizarPlantillaDocTransmisionNegocio(IConfiguration configuration)
            : base()
        {
            _configuration = configuration;
            _accesoAdatos = new ActulizarPlantillaDocTransmisionAccesoDatos();
        }
        #endregion

        #region Operaciones
        public async Task<ResponseGeneric<List<ActulizarPlantillaDocTransmisionResponse>>> Operacion(ActulizarPlantillaRequest request)
        {
            try
            {
                if (!string.IsNullOrEmpty(request.ArchivoBase64))
                {
                    var resultado = FileManager.GuardarArchivoWord(request.ArchivoBase64, _configuration["OficioRuta"], request.c_nombre);
                    if (resultado.Status == ResponseStatus.Success)
                    {
                        request.c_ruta = resultado.respuesta;
                        return await _accesoAdatos.Operacion(request);
                    }
                    else
                        return (ResponseGeneric<List<ActulizarPlantillaDocTransmisionResponse>>)resultado;
                }
                else
                {
                    request.c_ruta = null;
                    return await _accesoAdatos.Operacion(request);
                }
            }
            catch (Exception ex)
            {
                LogErrores("ActulizarPlantillaDocTransmisionNegocio", ex);
                throw;
            }
        }

        public async Task<ResponseGeneric<List<ActulizarPlantillaDocTransmisionResponse>>> OperacionSeleccionaPlantilla(ActulizarPlantillaRequest request)
        {
            try
            {
                return await _accesoAdatos.OperacionSeleccionaPlantilla(request);
            }
            catch (Exception ex)
            {
                LogErrores("ActulizarPlantillaDocTransmisionNegocio", ex);
                throw;
            }
        }
        #endregion
    }
}
