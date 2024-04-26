using Conexion;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Modelos.Interfaz;
using Utilidades.Log4Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Acceso_Datos.Base
{
    /// <summary>
    /// Clase base encargada de compartir propiedades, métodos o rutinas axuilares
    /// </summary>
    public abstract class BaseAccesoDatos
    {
        #region Propiedades
        private readonly ILoggerManager _logger;
        protected readonly IConfiguration Configuration;
        private int _tipoBase;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Base del acceso de datos
        /// </summary>
        protected BaseAccesoDatos()
        {
            _logger = new LoggerManager();
            Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
            _tipoBase = int.Parse(Configuration["TipoBase"].ToString());
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Método encargado de escribir un menaje de error en el log
        /// </summary>
        /// <param name="mensajeError">(Capa : AccesoDatos | Clase : GenericaAccesoDatos) </param>
        /// <param name="exception">La exception que se genero</param>
        protected void LogErrores(string mensajeError, Exception exception)
        {
            _logger.LogError(mensajeError, exception);
        }
        #endregion

    }
}
