using System;
using System.IO;
using System.Linq;

namespace Utilidades.GestorImagenes
{
    /// <summary>
    /// Gestion de imagenes
    /// </summary>
    public class GestorArchivos
    {
        private readonly string path = Directory.GetCurrentDirectory() + "/AlmacenArchivos/"; //Path
        /// <summary>
        /// Guardar imagen
        /// </summary>
        /// <param name="ImgStr"></param>
        /// <param name="ImgName"></param>
        /// <returns></returns>
        public string GuardarArchivo(string ImgStrbase64, string ImgName, string extension)
        {
            if (!System.IO.Directory.Exists(path)){Directory.CreateDirectory(path); }
            string imageName = ImgName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + extension;
            string imgPath = Path.Combine(path, imageName);
            byte[] imageBytes = Convert.FromBase64String(ImgStrbase64);
            File.WriteAllBytes(imgPath, imageBytes);
            return imageName;
        }
        /// <summary>
        /// Obtener imagen en base64
        /// </summary>
        /// <param name="ImgName"></param>
        /// <returns></returns>
        public string ObtenerArchivo(string ImgName)
        {
            try
            {
                string imgPath = Path.Combine(path, ImgName);
                byte[] imageArray = System.IO.File.ReadAllBytes(imgPath);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                return base64ImageRepresentation;
            }
            catch (Exception)
            {

                return "";
            }

        }
        /// <summary>
        /// Eliminar imagen fisica
        /// </summary>
        /// <param name="ImgName"></param>
        /// <returns></returns>
        public int EliminarArchivo(string ImgName)
        {
            try
            {
                string imgPath = Path.Combine(path, ImgName);
                if (System.IO.File.Exists(imgPath))
                {
                    System.IO.File.Delete(imgPath);
                    return 1;

                }
                else
                {
                    return 0;

                }
            }
            catch (Exception)
            {

                return 0;
            }

        }

        /// <summary>
        /// Método encargado de la creacion de los directorios donde se almacenara los
        /// archivos temp al crear el reporte.
        /// </summary>
        /// <returns></returns>
        public static void DirectoriosTempReporte()
        {
            try
            {
                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "AlmacenArchivos", "Reportes")))
                     Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "AlmacenArchivos", "Reportes"));
                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "AlmacenArchivos","Reportes", "ReportesTemporales")))
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "AlmacenArchivos", "Reportes", "ReportesTemporales"));
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Método encargado de eliminar los archivos generados para el reporte
        /// </summary>
        /// <param name="carpeta"></param>
        /// <returns></returns>
        public static void ProcesoLimpiarTempReporte(string carpeta)
        {
            try
            {
                string[] ListaArchivos = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "AlmacenArchivos", "Reportes", carpeta));

                if (ListaArchivos != null && ListaArchivos.Length > 0)
                    ListaArchivos.ToList().ForEach(archivo => EliminarArhivoDelServidor(archivo, carpeta));
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Método encargado de eliminar el archivo del servidor
        /// </summary>
        /// <param name="nameFile"></param>
        /// <returns></returns>
        public static bool EliminarArhivoDelServidor(string nombreArchivo, string carpeta)
        {
            bool isSuccesFull = false;
            try
            {
                if (SiExisteArchivo(nombreArchivo, carpeta))
                {
                    File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "AlmacenArchivos", "Reportes", carpeta, nombreArchivo));
                    isSuccesFull = true;
                }
                else
                    return isSuccesFull;
            }
            catch (Exception)
            {
                return isSuccesFull;
            }
            return isSuccesFull;

        }

        /// <summary>
        /// Método encargado de validar si exite el mismo archivo en el servidor
        /// </summary>
        /// <param name="nombreArchivo"></param>
        /// <returns></returns>
        private static bool SiExisteArchivo(string nombreArchivo, string carpeta)
            => File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "AlmacenArchivos", "Reportes", carpeta, nombreArchivo));
    }
}
