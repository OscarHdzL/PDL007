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
using Acceso_Datos.Catalogos;
using Acceso_Datos.Operaciones;

namespace Negocio.Operaciones
{
    public class ConsultaTramiteDeclaratoriaNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ConsultaTramiteDeclaratoriaProcedenciaAccesoDatos _accesoDatos;
        #endregion
        
        #region Contructor
        /// <summary>
        /// Constructor Inicial del inicio de sesion en negocio
        /// </summary>
        public ConsultaTramiteDeclaratoriaNegocio() : base()
        {
            _accesoDatos = new ConsultaTramiteDeclaratoriaProcedenciaAccesoDatos();
        }
        #endregion
        
        #region Métodos Publicos

        public async Task<ResponseGeneric<List<ConsultarTramiteDeclaratoriaPaso1>>> ConsultaPaso1(int id_declaratoria)
        {
            try
            {
                return await _accesoDatos.ConsultaPaso1(id_declaratoria);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaTramiteDeclaratoriaNegocio - Paso1", ex);
                throw;
            }
        }

        public async Task<ResponseGeneric<List<ConsultarTramiteDeclaratoriaPaso2>>> ConsultaPaso2(int id_declaratoria, int tipo_domicilio)
        {
            try
            {
                return await _accesoDatos.ConsultaPaso2(id_declaratoria, tipo_domicilio);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaTramiteDeclaratoriaNegocio - Paso2", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ConsultarTramiteDeclaratoriaPaso4>>> ConsultaPaso4(int id_declaratoria)
        {
            try
            {
                return await _accesoDatos.ConsultaPaso4(id_declaratoria);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaTramiteDeclaratoriaNegocio - Paso4", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ConsultarTramiteDeclaratoriaPaso5>>> ConsultaPaso5(int id_declaratoria)
        {
            try
            {
                return await _accesoDatos.ConsultaPaso5(id_declaratoria);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaTramiteDeclaratoriaNegocio - Paso5", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ConsultarTramiteDeclaratoriaAvance>>> ConsultaAvance(int id_declaratoria)
        {
            try
            {
                return await _accesoDatos.ConsultaAvance(id_declaratoria);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaTramiteDeclaratoriaNegocio - Avance", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ConsultarTramiteDeclaratoriaLista>>> ConsultaLista(int id_usuario, int id_rol)
        {
            try
            {
                return await _accesoDatos.ConsultaLista(id_usuario, id_rol);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaTramiteDeclaratoriaNegocio - Avance", ex);
                throw;
            }
        }

        public async Task<ResponseGeneric<List<ConsultarTramiteDeclaratoriaLista>>> ConsultaLista(ConsultaTramiteDeclaratoriaListaFiltrosRequest request)
        {
            try
            {
                return await _accesoDatos.ConsultaLista(request);
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaTramiteDeclaratoriaNegocio - ConsultaLista", ex);
                throw;
            }
        }
        #endregion
    }
}
