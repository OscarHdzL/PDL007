using Microsoft.AspNetCore.Http;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades.Log4Net;

namespace Utilidades.FileManager
{
    public class FileManager
    {
        /// <summary>
        /// Método encargado de crear un archivo en servidro
        /// </summary>
        /// <param name="archivoB64">Archivo en base64</param>
        /// <param name="rutaCompleta">Ruta encargada de donde se escribira el archivo</param>
        /// <param name="nombrePlantilla">Nombre del archivo que tendra en el servidor</param>
        /// <returns></returns>
        public static Response GuardarArchivoWord(string archivoB64, string rutaCompleta, string nombrePlantilla )
        {
            try
            {
                if (!Directory.Exists(rutaCompleta))
                     Directory.CreateDirectory(rutaCompleta);

                string rutaArchivo = $"{rutaCompleta}{nombrePlantilla}";

                if (File.Exists(rutaArchivo))
                    File.Delete(rutaArchivo);

                byte[] archivoEnBytes = Convert.FromBase64String(archivoB64.Split(',')[1]);

                File.WriteAllBytes($"{rutaArchivo}", archivoEnBytes);

                return new Response() { mensaje = "Guardado exitoso", Status = ResponseStatus.Success, respuesta = rutaArchivo };
            }
            catch (Exception ex)
            {
                new LoggerManager().LogError($"Carga de archivos { ex.Message }");
                return new Response() { mensaje = $"Error en guardado de archivo - Message: {ex.Message}", Status = ResponseStatus.Failed, respuesta = "" };
            }
        }

        public async Task<string> SaveFileAnexo(IFormFile file, string fileFolder, string extension, string ruta)
        {
            string db_path = Path.Combine("Anexos", fileFolder);
            string path = Path.Combine(ruta, db_path);
            if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
            string name = DateTime.Now.Ticks + "_" + Guid.NewGuid().ToString() + extension;
            string fullPath = Path.Combine(path, name);
            db_path = Path.Combine(db_path, name);

            using (Stream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return db_path;
        }

        public string ReadFile(string db_path, string ruta)
        {
            try
            {
                // string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                string path = Path.Combine(ruta, db_path);
                byte[] fileArray = File.ReadAllBytes(path);
                string base64FileRepresentation = Convert.ToBase64String(fileArray);
                return base64FileRepresentation;
            }
            catch (Exception)
            {
                return "";
            }

        }

        public string DeleteFile(string db_path, string ruta)
        {
            try
            {
                // string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                string path = Path.Combine(ruta, db_path);
                File.Delete(path);
                return "El archivo se eliminó correctamente";
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
