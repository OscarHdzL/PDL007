using Acceso_Datos.Operaciones;
using Modelos.Modelos.Request;
using Modelos.Modelos.Response;
using Modelos.Response;
using Negocio.Base;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Negocio.Catalogos;

namespace Negocio.Operaciones
{
    public class ConsultaActosReligiososNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ConsultaActosReligiososAccesoDatos _AccesoDatos;
        private readonly ConsultaDetalleTramiteTransmisionAccesoDatos _AccesoDatosTramiteTransmision;
        private readonly ConsultaOficioTransmisionAccesoDatos _AccesoDatosConsultaOficioTransmision;
        private readonly ConsultaPlantillaDocTransmisionNegocio _AccesoDatosConsultaPlantillaDocTransmision;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public ConsultaActosReligiososNegocio()
            : base()
        {
            _AccesoDatos = new();
            _AccesoDatosTramiteTransmision = new();
            _AccesoDatosConsultaOficioTransmision = new();
            _AccesoDatosConsultaPlantillaDocTransmision = new();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar los datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaActosReligiososResponse>>> ConsultarActos(ConsultaActosReligiososRequest request)
        {
            try
            {
                return await _AccesoDatos.ConsultarActos(request);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultarActos - Negocio", ex);
                throw;
            }
        }

        public async Task<ResponseGeneric<List<ConsultaActosMediosTrasmisionResponse>>> ConsultarActosMediosTransmision(ConsultaActosReligiososRequest request)
        {
            try
            {
                return await _AccesoDatos.ConsultarActosMediosTransmision(request);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultarActosMediosTransmision - Negocio", ex);
                throw;
            }
        }

        public async Task<ResponseGeneric<List<ConsultaActosFechasResponse>>> ConsultarActosFechas(ConsultaActosReligiososRequest request)
        {
            try
            {
                return await _AccesoDatos.ConsultarActosFechas(request);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultarActosFechas - Negocio", ex);
                throw;
            }
        }

        /// <summary>
        /// Método encargado de consultar la información para la creacion del oficio de transmiciones
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<ContenidoConsultaActosReligiososResponse>> ObtenerDatosDocTransmicion(ConsultaActosReligiososDocOficioRequest request)
        {
            try
            {
                var requetsActos = new ConsultaActosReligiososRequest { i_id_transmision = request.i_id_transmision, i_id_acto_religioso = request.i_id_acto_religioso };
                var resultadoDetalle = await _AccesoDatosTramiteTransmision.Consultar(new ConsultaTramiteTransmisionRequest { i_id_transmision = request.i_id_transmision, s_id_us = request.s_id_us });
                var resultadoOficio = await _AccesoDatosConsultaOficioTransmision.Consultar(new ConsultaOficioTransmisionRequest { i_id_transmision = request.i_id_transmision, i_id_tramite = request.i_id_tramite });
                var resultadoActos = await _AccesoDatos.ConsultarActos(requetsActos);
                var resultadoActosMedios = await _AccesoDatos.ConsultarActosMediosTransmision(requetsActos);
                var resultadoActosFechas = await _AccesoDatos.ConsultarActosFechas(requetsActos);
                //var resultadoPlantilla = await _AccesoDatosConsultaPlantillaDocTransmision.ConsultarActiva(9);
                var resultadoPlantilla = await _AccesoDatosConsultaPlantillaDocTransmision.GetPlantilla(9);

                return new ResponseGeneric<ContenidoConsultaActosReligiososResponse>(new ContenidoConsultaActosReligiososResponse
                {
                    ConsultaActosReligiosos = resultadoActos.Response,
                    ConsultaActosMediosTrasmision = resultadoActosMedios.Response,
                    ConsultaActosFechas = resultadoActosFechas.Response.Where(w => w.i_id_cat_periodo == null || w.i_id_cat_periodo == 0).ToList(),
                    ConsultaActosFrecuencia = resultadoActosFechas.Response.Where(w => w.i_id_cat_periodo != null || w.i_id_cat_periodo != 0).ToList(),
                    ConsultaDetalleTramiteTransmisions = resultadoDetalle.Response.FirstOrDefault(),
                    ConsultaOficioTransmisions = resultadoOficio.Response.FirstOrDefault(),
                    RutaDocumento = $"{resultadoPlantilla.Response.FirstOrDefault()?.c_ruta}"
                });

            }
            catch (Exception ex)
            {
                LogErrores("ObtenerDatosDocTransmicion - Negocio", ex);
                throw;
            }
        }
        #endregion
    }
}
