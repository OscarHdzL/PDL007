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
    public class ConsultaEstatusRegistroNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ConsultaEstatusRegistroAccesoDatos inicioAccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para la consulta de municipios en negocio
        /// </summary>
        public ConsultaEstatusRegistroNegocio()
            : base()
        {
            inicioAccesoDatos = new ConsultaEstatusRegistroAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar los estados
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaEstatusResponse>>> Consultar(ConsultaEstatusNewRequest entidad)
        {
            try
            {
                return await inicioAccesoDatos.Consultar(entidad);
            }
            catch (Exception ex)
            {
                LogErrores("Negocio ConsultaEstados-Consulta", ex);
                throw;
            }
        }
        #endregion
    }
}
