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
using Acceso_Datos.Operaciones;
using Acceso_Datos.Operaciones;

namespace Negocio.Operaciones
{
    public class ReporteTramitesNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ReporteTramitesAccesoDatos _accesoDatos;
        #endregion
        
        #region Contructor
        public ReporteTramitesNegocio() : base()
        {
            _accesoDatos = new ReporteTramitesAccesoDatos();
        }
        #endregion
        
        #region Métodos Publicos
        public async Task<ResponseGeneric<List<ReporteTramitesResponse>>> GetTransmisiones(ReporteTramitesRequest request)
        {
            try
            {
                return await _accesoDatos.GetTransmisiones(request);
            }
            catch (Exception ex)
            {
                LogErrores("ReporteTramitesNegocio - GetTransmisiones", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ReporteTramitesResponse>>> GetDeclaratorias(ReporteTramitesRequest request)
        {
            try
            {
                return await _accesoDatos.GetDeclaratorias(request);
            }
            catch (Exception ex)
            {
                LogErrores("ReporteTramitesNegocio - GetDeclaratorias", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ReporteTramitesResponse>>> GetNotas(ReporteTramitesRequest request)
        {
            try
            {
                return await _accesoDatos.GetNota(request);
            }
            catch (Exception ex)
            {
                LogErrores("ReporteTramitesNegocio - GetNotas", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<ReporteTramitesResponse>>> GetRegistros(ReporteTramitesRequest request)
        {
            try
            {
                return await _accesoDatos.GetRegistro(request);
            }
            catch (Exception ex)
            {
                LogErrores("ReporteTramitesNegocio - GetRegistros", ex);
                throw;
            }
        }
        
        #endregion
        
    }
}
