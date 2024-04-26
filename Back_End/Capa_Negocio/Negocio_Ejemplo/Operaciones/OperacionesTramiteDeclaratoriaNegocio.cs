using Acceso_Datos.Operaciones;
using Modelos.Modelos.Request;
using Modelos.Modelos.Response;
using Modelos.Response;
using Negocio.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acceso_Datos.Catalogos;
using Acceso_Datos.Operaciones;
using Acceso_Datos.Operaciones;

namespace Negocio.Operaciones
{
    public class OperacionesTramiteDeclaratoriaNegocio : BaseNegocio
    {
        #region Propidades
        private readonly OperacionesTramiteDeclaratoriaAccesoDatos _accesoDatos;
        private readonly ConsultaDetalleUsuarioSistemaAccesoDatos _accesoAdatosUsuario;
        private readonly ConsultaPlantillaDocTransmisionAccesoDatos _accesoDatosConsultaPlantilla;
        private readonly ConsultaTramiteDeclaratoriaProcedenciaAccesoDatos _accesoTramiteDeclaratoriaInfoInicial;
        #endregion
        
        #region Contructor
        /// <summary>
        /// Constructor Inicial del inicio de sesion en negocio
        /// </summary>
        public OperacionesTramiteDeclaratoriaNegocio() : base()
        {
            _accesoDatos = new OperacionesTramiteDeclaratoriaAccesoDatos();
            _accesoAdatosUsuario = new ConsultaDetalleUsuarioSistemaAccesoDatos();
            _accesoDatosConsultaPlantilla = new ConsultaPlantillaDocTransmisionAccesoDatos();
            _accesoTramiteDeclaratoriaInfoInicial = new ConsultaTramiteDeclaratoriaProcedenciaAccesoDatos();
        }
        #endregion
        
        #region Métodos Publicos
        
        public async Task<ResponseGeneric<List<ResponseGenerico>>> Activar(ActivarTramiteDeclaratoria request)
        {
            try
            {
                return await _accesoDatos.Activar(request);
            }
            catch (Exception ex)
            {
                LogErrores("OperacionesTramiteDeclaratoriaAccesoDatos - Activar", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ResponseGenerico>>> Finalizar(ActualizarEstatusDeclaratoria request)
        {
            try
            {
                return await _accesoDatos.Finalizar(request);
            }
            catch (Exception ex)
            {
                LogErrores("OperacionesTramiteDeclaratoriaAccesoDatos - ActualizarEstatus", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ConsultaDetalleUsuarioSistemaResponse>>> Consulta(ConsultaDetalleUsuarioSistemaRequest request)
        {
            try
            {
                return await _accesoAdatosUsuario.Consultar(request);
            }
            catch (Exception ex)
            {
                LogErrores("OperacionesTramiteDeclaratoriaNegocio - Consulta", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ResponseGenerico>>> Asignar(AsignarDeclaratoria request)
        {
            try
            {
                return await _accesoDatos.Asignar(request);
            }
            catch (Exception ex)
            {
                LogErrores("OperacionesTramiteDeclaratoriaNegocio - Asignar", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ResponseGenerico>>> PostComentarios(ComentariosDeclaratoria request)
        {
            try
            {
                return await _accesoDatos.PostComentarios(request);
            }
            catch (Exception ex)
            {
                LogErrores("OperacionesTramiteDeclaratoriaNegocio - PostComentarios", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ResponseGenerico>>> GenerarOficio(ActualizarEstatusDeclaratoria request)
        {
            try
            {
                return await _accesoDatos.GenerarOficio(request);
            }
            catch (Exception ex)
            {
                LogErrores("OperacionesTramiteDeclaratoriaNegocio - GenerarOficio", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ResponseGenerico>>> Aprobar(ActualizarEstatusDeclaratoria request)
        {
            try
            {
                return await _accesoDatos.Aprobar(request);
            }
            catch (Exception ex)
            {
                LogErrores("OperacionesTramiteDeclaratoriaAccesoDatos - Aprobar", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ResponseGenerico>>> Autorizar(AutorizarDeclaratoria request)
        {
            try
            {
                return await _accesoDatos.Autorizar(request);
            }
            catch (Exception ex)
            {
                LogErrores("OperacionesTramiteDeclaratoriaAccesoDatos - Autorizar", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ResponseGenerico>>> Concluir(ConcluirDeclaratoria request)
        {
            try
            {
                return await _accesoDatos.Concluir(request);
            }
            catch (Exception ex)
            {
                LogErrores("OperacionesTramiteDeclaratoriaAccesoDatos - Concluir", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<ObtenerInfoOficio>> ObtenerInformacion(int id_declaratoria)
        {
            try
            {
                //var requetsActos = new ConsultaActosReligiososRequest { i_id_transmision = request.i_id_transmision, i_id_acto_religioso = request.i_id_acto_religioso };
                var resultado = await _accesoTramiteDeclaratoriaInfoInicial.ConsultaPaso1(id_declaratoria);
                var notificaciones = await _accesoTramiteDeclaratoriaInfoInicial.ConsultaPaso2(id_declaratoria, 2);
                var domicilio = await _accesoTramiteDeclaratoriaInfoInicial.ConsultaPaso2(id_declaratoria, 3);
                var colindancia = await _accesoTramiteDeclaratoriaInfoInicial.ConsultaPaso4(id_declaratoria);
                var resultadoPlantilla = await _accesoDatosConsultaPlantilla.GetPlantilla(10);
                
                return new ResponseGeneric<ObtenerInfoOficio>(new ObtenerInfoOficio
                {
                    informacionPrincipal = resultado.Response[0],
                    notificaciones = "Calle: " + notificaciones.Response[0].calle + ", Núm. Ext: " + notificaciones.Response[0].numeroe + " Núm. Int: " + notificaciones.Response[0].numeroi +
                                     "\n" + notificaciones.Response[0].colonia + ", " + notificaciones.Response[0].ciudad + ", C.P. " + notificaciones.Response[0].codigo_postal,
                    domicilio = "Calle: " + domicilio.Response[0].calle + ", Núm. Ext: " + domicilio.Response[0].numeroe + " Núm. Int: " + domicilio.Response[0].numeroi +
                                     "\n" + domicilio.Response[0].colonia + ", " + domicilio.Response[0].ciudad + ", C.P. " + domicilio.Response[0].codigo_postal,
                    uso = colindancia.Response[0].uso.ToString(),
                    rutaPlantilla = $"{resultadoPlantilla.Response.FirstOrDefault()?.c_ruta}"
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
