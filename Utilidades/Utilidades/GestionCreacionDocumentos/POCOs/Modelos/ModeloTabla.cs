using Modelos.Modelos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades.GestionCreacionDocumentos.POCOs.Modelos
{
    /// <summary>
    /// Modelo
    /// </summary>
    public class ModeloTabla
    {
        #region Propiedades
        /// <summary>
        /// Propiedad de la cabezeras de la tablas
        /// </summary>
        public List<ModeloValor> TitulosCabezera { get; private set; }

        /// <summary>
        /// Propiedad del los datos del contenido
        /// </summary>
        public List<List<ModeloValor>> DatosContenido { get; private set; }
        #endregion

        #region Constructores
        /// <summary>
        /// Constructor que resive los datos de medios de transmisicion
        /// </summary>
        /// <param name="responses"></param>
        public ModeloTabla(List<ConsultaActosMediosTrasmisionResponse> responses)
        {
            TitulosCabezera = new List<ModeloValor>
            {
                new ModeloValor("Frecuencia/Canal", esTitulo: true),
                new ModeloValor("Proveedor de Servicio", esTitulo: true),
                new ModeloValor("Televisora/Radiodifusora", esTitulo: true),
                new ModeloValor("Lugar de Transmisión", esTitulo: true),
            };

            DatosContenido = new();

            responses.ForEach(resp =>
            {
                var listFila = new List<ModeloValor>
                {
                    new ModeloValor(resp.frecuencia_canal, esTitulo: false),
                    new ModeloValor(resp.proveedor, esTitulo: false),
                    new ModeloValor(resp.televisora_radiodifusora, esTitulo: false),
                    new ModeloValor(resp.lugar_transmision, esTitulo: false)
                };

                DatosContenido.Add(listFila);
            });
        }

        /// <summary>
        /// Constructor de resive los datos las frecuencias
        /// para procesar la tabla de frecuencias
        /// </summary>
        /// <param name="responseFrecuencia"></param>
        public ModeloTabla(List<ConsultaActosFechasResponse> responseFrecuencia)
        {

            TitulosCabezera = new List<ModeloValor>
            {
                new ModeloValor("Mes", esTitulo: true),
                new ModeloValor("Día", esTitulo: true),
                new ModeloValor("Horario", esTitulo: true),
                new ModeloValor("Año", esTitulo: true),
            };

            DatosContenido = new();

            foreach (var item in responseFrecuencia)
            {
                var argDatos = new CatalogosFrecuencias(item.cat_anio, item.cat_mes, item.cat_dia);

                foreach (var anio in argDatos.Anio.OrderBy(o => o))
                {
                    argDatos.Mes.ForEach(mes => 
                    {
                        string mesValido = argDatos.DatosMostrarMes(anio, mes);
                        if (!string.IsNullOrEmpty(mesValido)) 
                        {
                             DatosContenido.Add(new List<ModeloValor>
                             {
                                new ModeloValor($"{ mesValido}", esTitulo: false),
                                new ModeloValor($"{ argDatos.DatosMotrarTodosLosDias(anio, mes)}", esTitulo: false),
                                new ModeloValor($"De { item.c_hora_inicio ?? " "} a {item.c_hora_fin ?? " "} horas", esTitulo: false),
                                new ModeloValor($"{ anio}", esTitulo: false),
                             });
                        }
                    });
                }
            }
        }
        #endregion
    }
}
