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
    public class FinalizarTomaNotaNegocio : BaseNegocio
    {
        #region Propidades
        private readonly FinalizarTomaNotaAccesoDatos _accesoAdatos;
        private readonly ConsultaDetalleUsuarioSistemaAccesoDatos _accesoAdatosUsuario;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public FinalizarTomaNotaNegocio()
            : base()
        {
            _accesoAdatos = new FinalizarTomaNotaAccesoDatos();
            _accesoAdatosUsuario = new ConsultaDetalleUsuarioSistemaAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Procesar encargado ejecutar el guardado de todo el registro completo
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<FinalizarTomaNotaResponse>>> Operacion(FinalizarTomaNotaRequest request)
        {
            try
            {
                return await _accesoAdatos.Operacion(request);
            }
            catch (Exception ex)
            {
                LogErrores("FinalizarTomaNotaNegocio - Operacion", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener Usuario Envío confirmación finalización trámite
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ConsultaDetalleUsuarioSistemaResponse>>> Consulta(ConsultaDetalleUsuarioSistemaRequest request)
        {
            try
            {
                return await _accesoAdatosUsuario.Consultar(request);
            }
            catch (Exception ex)
            {
                LogErrores("FinalizarTomaNotaNegocio - Operacion", ex);
                throw;
            }
        }
        #endregion
    }
}
