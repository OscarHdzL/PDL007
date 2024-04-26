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
    /// <summary>
    /// Clase del proceso del negocio
    /// </summary>
    public class InsertarUsuarioSistemaNegocio : BaseNegocio
    {
        #region Propidades
        private readonly InsertarUsuarioSistemaAccesoDatos _accesoAdatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public InsertarUsuarioSistemaNegocio()
            : base()
        {
            _accesoAdatos = new InsertarUsuarioSistemaAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Procesar encargado ejecutar el guardado de todo el registro completo
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<InsertarUsuarioSistemaResponse>>> Operacion(InsertarUsuarioSistemaRequest request)
        {
            try
            {
                return await _accesoAdatos.Operacion(request);
            }
            catch (Exception ex)
            {
                LogErrores("InsertarUsuarioSistemaNegocio - Operacion", ex);
                throw;
            }
        }
        #endregion
    }
}
