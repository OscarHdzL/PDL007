using Utilidades.Log4Net;
using Modelos.Interfaz;
using System;

namespace Negocio.Base
{
    /// <summary>
    /// Clase base encargada de compartir métodos o rutinas axuilares
    /// </summary>
    public abstract class BaseNegocio
    {
        #region Propiedades
        private readonly ILoggerManager _logger;
        #endregion

        #region Contructor 
        /// <summary>
        /// Constructor Base del negocio
        /// </summary>
        protected BaseNegocio()
        {
            _logger = new LoggerManager();
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Método encargado de escribir un menaje de error en el log
        /// </summary>
        /// <param name="mensajeError">(Capa : Negocio | Clase : GenericaNegocio) </param>
        /// <param name="exception">La exception que se genero</param>
        protected void LogErrores(string mensajeError, Exception exception)
        {
            _logger.LogError(mensajeError, exception);
        }
        #endregion
    }
}
