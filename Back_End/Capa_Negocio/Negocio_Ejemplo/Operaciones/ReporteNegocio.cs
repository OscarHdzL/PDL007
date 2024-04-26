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
    public class ReporteNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ReporteAccesoDatos reporteDatos;
        #endregion

        #region Contructor

        public ReporteNegocio()
            : base()
        {
            reporteDatos = new ReporteAccesoDatos();
        }
        #endregion
        public async Task<ResponseGeneric<List<ReporteResponse>>> Consultar(ReporteRequest entidad)
        {
            try
            {
                return await reporteDatos.Consultar(entidad);
            }
            catch (Exception ex)
            {
                LogErrores("Negocio ReporteContactos-Consulta", ex);
                throw;
            }
        }

        public async Task<ResponseGeneric<List<ReporteResponseTnota>>> ConsultarTnota(ReporteRequest entidad)
        {
            try
            {
                return await reporteDatos.ConsultarTnota(entidad);
            }
            catch (Exception ex)
            {
                LogErrores("Negocio ReporteContactos-Consulta", ex);
                throw;
            }
        }
    }
}
