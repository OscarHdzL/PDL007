using Acceso_Datos.Catalogos;
using Modelos.Modelos.Request;
using Modelos.Modelos.Response;
using Modelos.Response;
using Negocio.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Catalogos
{
    public class CatalogosTramiteDeclaratoriaNegocio  : BaseNegocio
    {
        #region Propidades
        private readonly CatalogosTramiteDeclaratoriaAccesoDatos _accesoDatos;
        #endregion
        
        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public CatalogosTramiteDeclaratoriaNegocio() : base()
        {
            _accesoDatos = new CatalogosTramiteDeclaratoriaAccesoDatos();
        }
        #endregion
        
        #region Métodos Publicos
        
        public async Task<ResponseGeneric<List<CatalogoGenericoResponse>>> GetUsoInmueble()
        {
            try
            {
                return await _accesoDatos.GetUsoInmueble();
            }
            catch (Exception ex)
            {
                LogErrores("CatalogosTramiteDeclaratoriaNegocio - ConsultarEstatus", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<CatalogoGenericoResponse>>> GetEstatus()
        {
            try
            {
                return await _accesoDatos.GetEstatus();
            }
            catch (Exception ex)
            {
                LogErrores("CatalogosTramiteDeclaratoriaNegocio - ConsultarEstatus", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ConsultaListaUsuariosDictaminadorTransmisionResponse>>> GetDictaminadores()
        {
            try
            {
                return await _accesoDatos.GetDictaminadores();
            }
            catch (Exception ex)
            {
                LogErrores("CatalogosTramiteDeclaratoriaNegocio - GetDictaminadores", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<CatalogoGenericoResponse>>> GetEstatusReporte(int p_tipo_tramite)
        {
            try
            {
                return await _accesoDatos.GetEstatusReporte(p_tipo_tramite);
            }
            catch (Exception ex)
            {
                LogErrores("CatalogosTramiteDeclaratoriaNegocio - GetEstatusReporte", ex);
                throw;
            }
        }
        #endregion
        
    }
}
