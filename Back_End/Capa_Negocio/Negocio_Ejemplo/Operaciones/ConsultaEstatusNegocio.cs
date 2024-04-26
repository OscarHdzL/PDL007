using Acceso_Datos.Operaciones;
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
    public class ConsultaEstatusNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ConsultaEstatusAccesoDatos inicioAccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para la consulta de municipios en negocio
        /// </summary>
        public ConsultaEstatusNegocio()
            : base()
        {
            inicioAccesoDatos = new ConsultaEstatusAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar los estados
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaEstatusResponse>>> Consultar()
        {
            try
            {
                return await inicioAccesoDatos.Consultar();
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
