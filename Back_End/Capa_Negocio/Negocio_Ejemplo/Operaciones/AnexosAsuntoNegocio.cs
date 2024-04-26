using Acceso_Datos.Operaciones;
using Microsoft.Extensions.Configuration;
using Modelos.Modelos.Request;
using Modelos.Modelos.Response;
using Modelos.Response;
using Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades.FileManager;

namespace Negocio.Operaciones
{
    public class AnexosAsuntoNegocio : BaseNegocio
    {
        #region Propiedades

        private readonly AnexosAsuntoAccesoDatos _AccesoDatos;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor inicial
        /// </summary>
        public AnexosAsuntoNegocio() : base()
        {
            _AccesoDatos = new AnexosAsuntoAccesoDatos();
        }

        #endregion

        #region Métodos Públicos
        /// <summary>
        /// Método encargado de ejecutar la lista de los registros
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaListaAnexoAsuntoResponse>>> ConsultarListaAnexo(ConsultaListaAnexoAsuntoRequest request)
        {
            try
            {
                return await _AccesoDatos.ConsultarListaAnexo(request); 
            }
            catch (Exception ex)
            {
                LogErrores("Negocio ConsultaListaAnexoAsuntoNegocio-Consultar", ex);
                throw;
            }
        }

        /// <summary>
        /// Método encargado de ejecutar la consulta de un registro
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaDetalleAnexoAsuntoResponse>>> ConsultarDetalleAnexo(ConsultaDetalleAnexoAsuntoRequest request, string ruta)
        {
            try
            {
                FileManager fileManager = new FileManager();
                var response = await _AccesoDatos.ConsultarDetalleAnexo(request);
                if (response.Response.Count > 0)
                {
                    var respuesta = response.Response[0];
                    respuesta.base64_anexo = fileManager.ReadFile(respuesta.url_anexo, ruta);
                }

                return response;
            }
            catch (Exception ex)
            {
                LogErrores("Negocio ConsultaCatalogoCartografiaMapaNegocio-Consultar", ex);
                throw;
            }
        }

        /// <summary>
        /// Método encargado de insertar el registro
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<InsertarAnexoAsuntoResponse>>> InsertarAnexoAsunto(InsertarAnexoAsuntoRequest request, string ruta)
        {
            try
            {
                FileManager fileManager = new FileManager();
                var url = await fileManager.SaveFileAnexo(request.anexo, request.id_asunto.ToString(), request.extension, ruta);
                InsertarAnexoRequest request_anexo = new InsertarAnexoRequest
                {
                    id_toma_nota = request.id_asunto,
                    nombre_anexo = request.nombre_anexo,
                    url_anexo = url,
                    id_tramite = request.id_tramite
                };
                return await _AccesoDatos.InsertarAnexoAsunto(request_anexo);
            }
            catch (Exception ex)
            {
                LogErrores("Negocio InsertarAnexoAsuntoNegocio-Operacion", ex);
                throw;
            }
        }

        /// <summary>
        /// Método encargado de ejecutar la actualización del registro completo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<BorrarAnexoBDResponse>>> BorrarAnexoAsunto(BorrarAnexoRequest request, string ruta)
        {
            try
            {
                var resultado_url = await _AccesoDatos.BorrarAnexoAsunto(request);

                FileManager fileManager = new FileManager();
                fileManager.DeleteFile(resultado_url.Response[0].url_anexo, ruta);

                return await _AccesoDatos.BorrarAnexoBD(request);
            }
            catch (Exception ex)
            {
                LogErrores("Negocio BorrarAnexoAsuntoNegocio-Operacion", ex);
                throw;
            }
        }

        #endregion
    }
}
