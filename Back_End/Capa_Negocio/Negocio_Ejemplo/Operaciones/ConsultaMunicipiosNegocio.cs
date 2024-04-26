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
    public class ConsultaMunicipiosNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ConsultaMunicipiosAccesoDatos inicioAccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial para la consulta de municipios en negocio
        /// </summary>
        public ConsultaMunicipiosNegocio()
            : base()
        {
            inicioAccesoDatos = new ConsultaMunicipiosAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar los municipios
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaMunicipiosResponse>>> Consultar(ConsultaMunicipiosRequest entidad)
        {
            try
            {
                return await inicioAccesoDatos.Consultar(entidad);
            }
            catch (Exception ex)
            {
                LogErrores("Negocio ConsultaMunicipios-Consulta", ex);
                throw;
            }
        }
        #endregion
    }
}
