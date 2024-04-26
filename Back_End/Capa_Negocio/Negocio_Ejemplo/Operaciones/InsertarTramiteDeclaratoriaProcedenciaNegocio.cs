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
    public class InsertarTramiteDeclaratoriaProcedenciaNegocio : BaseNegocio
    {
        #region Propidades
        private readonly InsertarTramiteDeclaratoriaProcedenciaAccesoDatos _accesoDatos;
        #endregion
        
        #region Contructor
        public InsertarTramiteDeclaratoriaProcedenciaNegocio() : base()
        {
            _accesoDatos = new InsertarTramiteDeclaratoriaProcedenciaAccesoDatos();
        }
        #endregion
        
        #region Métodos Publicos
        
        public async Task<ResponseGeneric<List<ResponseGenerico>>> InsertarPaso1(InsertarTramiteDeclaratoriaPaso1 request)
        {
            try
            {
                return await _accesoDatos.InsertarPaso1(request);
            }
            catch (Exception ex)
            {
                LogErrores("InsertarTramitesDeclaratoriaProcedenciaAccesoDatos - Paso1", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ResponseGenerico>>> InsertarPaso2(InsertarTramiteDeclaratoriaPaso2 request)
        {
            try
            {
                return await _accesoDatos.InsertarPaso2(request);
            }
            catch (Exception ex)
            {
                LogErrores("InsertarTramitesDeclaratoriaProcedenciaAccesoDatos - Paso2", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ResponseGenerico>>> InsertarPaso4(InsertarTramiteDeclaratoriaPaso4 request)
        {
            try
            {
                return await _accesoDatos.InsertarPaso4(request);
            }
            catch (Exception ex)
            {
                LogErrores("InsertarTramitesDeclaratoriaProcedenciaAccesoDatos - Paso4", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ResponseGenerico>>> InsertarPaso5(InsertarTramiteDeclaratoriaPaso5 request)
        {
            try
            {
                return await _accesoDatos.InsertarPaso5(request);
            }
            catch (Exception ex)
            {
                LogErrores("InsertarTramitesDeclaratoriaProcedenciaAccesoDatos - Paso5", ex);
                throw;
            }
        }
        
        #endregion
    }
}