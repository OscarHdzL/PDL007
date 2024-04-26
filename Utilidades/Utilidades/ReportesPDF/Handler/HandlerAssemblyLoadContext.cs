using System;
using System.Reflection;
using System.Runtime.Loader;

namespace Utilidades.ReportesPDF.Handler
{
    /// <summary>
    /// Clase encargada de manipular la carga de las dll
    /// para la creacion de un pdf.
    /// </summary>
    public class HandlerAssemblyLoadContext : AssemblyLoadContext
    {
        /// <summary>
        /// Método que ejecuta la carga de las dll 
        /// </summary>
        /// <param name="absolutePath">Ruta donde se encuentran los archivos de tipo .dll </param>
        /// <returns></returns>
        public IntPtr LoadUnmanagedLibrary(string absolutePath)
        {
            return LoadUnmanagedDll(absolutePath);
        }

        /// <summary>
        /// Método encargado cargar las dll mediende su nombre
        /// </summary>
        /// <param name="unmanagedDllName">Nombre de la dll a cargar al inicio </param>
        /// <returns></returns>
        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            return LoadUnmanagedDllFromPath(unmanagedDllName);
        }

        /// <summary>
        /// Método sin implementacion
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        protected override Assembly Load(AssemblyName assemblyName)
        {
            throw new NotImplementedException();
        }
    }
}
