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
    public class ReporteTransmisionNegocio: BaseNegocio
    {
        #region Propidades
        private readonly ReporteTransmisionAccesoDatos reporteDatos;
        #endregion

        #region Contructor

        public ReporteTransmisionNegocio()
            : base()
        {
            reporteDatos = new ReporteTransmisionAccesoDatos();
        }
        #endregion
        public async Task<ResponseGeneric<List<ReporteTransmisionListaEstatusResponse>>> ConsultarEstatusTransmision(ReporteTransmisionListaEstatusRequest entidad)
        {
            try
            {
                return await reporteDatos.ConsultarEstatusTransmision(entidad);
            }
            catch (Exception ex)
            {
                LogErrores("Negocio ReporteTransmisionNegocio ConsultarEstatusTransmision", ex);
                throw;
            }
        }

        public async Task<ResponseGeneric<List<ReporteTransmisionResponse>>> ConsultarReporteTransmision(ReporteTransmisionRequest entidad)
        {
            try
            {
                return await reporteDatos.ConsultarReporteTransmision(entidad);
            }
            catch (Exception ex)
            {
                LogErrores("Negocio ReporteTransmisionNegocio ConsultarReporteTransmision", ex);
                throw;
            }
        }


    }
}
