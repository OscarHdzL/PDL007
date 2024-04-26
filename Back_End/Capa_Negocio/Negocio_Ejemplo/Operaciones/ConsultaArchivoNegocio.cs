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
using Utilidades.CifradoMd5;

namespace Negocio.Operaciones
{
    public class ConsultaArchivoNegocio : BaseNegocio
    {
        #region Propidades
        private readonly ConsultaArchivoAccesoDatos _AccesoDatos;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor Inicial 
        /// </summary>
        public ConsultaArchivoNegocio()
            : base()
        {
            _AccesoDatos = new ConsultaArchivoAccesoDatos();
        }
        #endregion

        #region Métodos Publicos
        /// <summary>
        /// Consultar los datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public async Task<ResponseGeneric<List<ArchivoResponse>>> Consultar(long id, int idArchivoTramite)
        {
            try
            {
                var respuesta = await _AccesoDatos.Consultar(id, idArchivoTramite);
                foreach (var archivo in respuesta.Response)
                {
                    // Decodificamos la ruta del archivo
                    CifradoMd5 cifradoMd5 = new CifradoMd5();
                    string fileName = cifradoMd5.descifrar(archivo.ruta);
                    archivo.ext = fileName.Split('.')[1];

                    // Leemos el archivo y lo retornamos en base64
                    archivo.ruta = Convert.ToBase64String(System.IO.File.ReadAllBytes(fileName));
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaArchivoNegocio - Consultar", ex);
                throw;
            }
        }
        
        public async Task<ResponseGeneric<List<PlantillaBaseResponse>>> GetPlantilla(int p_id_archivo)
        {
            try
            {
                PlantillaBaseResponse objeto = new PlantillaBaseResponse();
                List<PlantillaBaseResponse> respuesta = new List<PlantillaBaseResponse>();
                
                var resultado = await _AccesoDatos.GetPlantilla(p_id_archivo);

                if (resultado.Response.Count > 0)
                {
                    int indice = resultado.Response[0].ruta.LastIndexOf('.');
                    objeto.extension = resultado.Response[0].ruta.Substring(indice + 1);
                    objeto.base64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(resultado.Response[0].ruta));
                    
                    respuesta.Add(objeto);
                    
                    return new ResponseGeneric<List<PlantillaBaseResponse>>(respuesta);
                    
                }
                else
                {
                    return new ResponseGeneric<List<PlantillaBaseResponse>>(respuesta);
                }
                
            }
            catch (Exception ex)
            {
                LogErrores("ConsultaArchivoNegocio - ObtenerPlantilla", ex);
                throw;
            }
        }
        #endregion
    }
}
