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
    /// Clase encargada de las reglas del negocio de la Insertar una plantilla de documento de transmisión
    /// </summary>
    public class InsertarPlantillaDocTransmisionNegocio : Base.BaseNegocio
    {
        #region Propiedades
        private readonly InsertarPlantillaDocTransmisionAccesoDatos _accesoAdatos;
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        /// <summary>
        /// Consttructor inical 
        /// </summary>
        /// <param name="configuration">Ioc del archivo de configuraciones </param>
        public InsertarPlantillaDocTransmisionNegocio(IConfiguration configuration)
            : base ()
        {
            _configuration = configuration;
            _accesoAdatos = new InsertarPlantillaDocTransmisionAccesoDatos();
        }
        #endregion

        #region Operaciones
        public async Task<ResponseGeneric<List<InsertarPlantillaDocTransmisionResponse>>> Operacion(InsertarPlantillaRequest request)
        {
            try
            {
                if (request != null && !string.IsNullOrEmpty(request.ArchivoBase64))
                {
                    var resultado = FileManager.GuardarArchivoWord(request.ArchivoBase64, _configuration["OficioRuta"], request.c_nombre);
                    if (resultado.Status == ResponseStatus.Success)
                    {
                        request.c_ruta = resultado.respuesta;
                        return await _accesoAdatos.Operacion(request);
                    }
                    else
                        return (ResponseGeneric<List<InsertarPlantillaDocTransmisionResponse>>)resultado;
                }
                else
                {
                    throw new ArgumentException("No se cargo el documento");
                }
            }
            catch (Exception ex)
            {
                LogErrores("BorraConvocatoriaAccesoDatos", ex);
                throw;
            }
        }
        #endregion
    }
}
