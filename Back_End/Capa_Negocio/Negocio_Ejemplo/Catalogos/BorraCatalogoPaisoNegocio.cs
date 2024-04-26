﻿using Acceso_Datos.Catalogos;
using Modelos.Modelos.Request;
using Modelos.Modelos.Response;
using Modelos.Response;
using Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Catalogos
{
    public class BorraCatalogoPaisoNegocio : BaseNegocio
    {
        #region Propidades
        private readonly BorraCatalogoPaisoAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public BorraCatalogoPaisoNegocio()
            : base()
        {
            _AccesoDatos = new BorraCatalogoPaisoAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<BorraCatalogoPaisoResponse>>> Operacion(BorraCatalagoPaisoRequest entidad)
        {
            try
            {
                return await _AccesoDatos.Operacion(entidad);
            }
            catch (Exception ex)
            {
                LogErrores("BorraCatalogoPaisoNegocio - Operacion", ex);
                throw;
            }
        }
        #endregion
    }
}
