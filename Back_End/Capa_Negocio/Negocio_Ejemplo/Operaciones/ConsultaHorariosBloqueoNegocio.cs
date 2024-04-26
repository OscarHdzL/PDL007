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
    public class ConsultaHorariosBloqueoNegocio : BaseNegocio
    {
        #region Propidades
       private readonly ConsultaHorariosBloqueoAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public ConsultaHorariosBloqueoNegocio()
            : base()
        {
            _AccesoDatos = new ConsultaHorariosBloqueoAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar 
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
       public async Task<ResponseGeneric<List<ConsultaHorariosBloqueoResponse>>> Consultar(ConsultaHorariosBloqueoRequest entidad)
        {
            try
            {
                return await _AccesoDatos.Consultar(entidad);
            }
            catch (Exception ex)
            {
                LogErrores("Negocio ConsultaHorariosBloqueoNegocio", ex);
                throw;
            }
        }
        #endregion
    }
}
