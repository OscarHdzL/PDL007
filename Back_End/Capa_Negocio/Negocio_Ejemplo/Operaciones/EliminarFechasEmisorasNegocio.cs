﻿using Acceso_Datos.Operaciones;
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
    public class EliminarFechasEmisorasNegocio : BaseNegocio
    { 
        #region Propidades
        private readonly EliminarFechasEmisorasAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public EliminarFechasEmisorasNegocio() : base()
        {
            _AccesoDatos = new EliminarFechasEmisorasAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar los datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<EliminarFechasEmisorasResponse>>> Operacion(EliminarFechasEmisorasRequest entidad)
        {
            try
            {
                return await _AccesoDatos.Operacion(entidad);
            }
            catch (Exception ex)
            {
                LogErrores("EliminarFechasEmisorasAccesoDatos - Operacion", ex);
                throw;
            }
        }
        #endregion
    }
}
