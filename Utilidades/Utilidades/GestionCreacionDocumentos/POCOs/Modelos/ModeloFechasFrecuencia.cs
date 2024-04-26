using Modelos.Modelos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Globalization;

namespace Utilidades.GestionCreacionDocumentos.POCOs.Modelos
{
    /// <summary>
    /// Clase de convertir el texto de las fechas y frecuencias.
    /// </summary>
    public class ModeloFechasFrecuencia
    {
        #region Propiedades
        /// <summary>
        /// Propiedad para agregar un titulo 
        /// </summary>
        public ModeloValor Titulo { get; private set; }

        /// <summary>
        /// Propiedad para obtener los datos de las fechas
        /// </summary>
        public List<ModeloValor> DatosFechas { get; private set; }

        /// <summary>
        /// Propiedad para obtener los datos de las frecuencias
        /// </summary>
        public ModeloTabla DatosFrecuencias { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor de inicial de modelo
        /// </summary>
        /// <param name="responseFechas">Lista de las fechas filtradas por los actos religiosos</param>
        /// <param name="responseFrecuencias">Lista de las frecuencias filtradas por los actos religiosos</param>
        public ModeloFechasFrecuencia(List<ConsultaActosFechasResponse> responseFechas, List<ConsultaActosFechasResponse> responseFrecuencias)
        {
            DatosFechas = new();

            Titulo = new ModeloValor("Fechas y Horario", esTitulo: true);

            foreach (var item in responseFechas)
                    DatosFechas.Add(new ModeloValor($"Del { ConvertirFormatoFecha(item.c_fecha_inicio) } al { ConvertirFormatoFecha(item.c_fecha_fin) }.      De { item.c_hora_inicio } a { item.c_hora_fin} horas."));

            if (responseFrecuencias != null)
                DatosFrecuencias = new ModeloTabla(responseFrecuencias);
        }
        #endregion

        #region Métodos privados
        /// <summary>
        /// Método encargado de convertir el formato de fecha al tradiccional
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        private static string ConvertirFormatoFecha(string fecha)
        {
            if (!string.IsNullOrEmpty(fecha))
            {
                var arg = fecha.Split('-');
                return $"{arg[2]}/{arg[1]}/{arg[0]}";
            } 
            else
                return fecha;
        }
        #endregion

    }

    /// <summary>
    /// Clase para deserealizar el JSON
    /// </summary>
    public class CatGenerico
    {
        #region Propiedades de argumenos del JSON
        public int? i_id { get; set; }
        public string c_nombre { get; set; }
        #endregion
    }

    /// <summary>
    /// Clase encargada de Procesar los datos de las frecuencias.
    /// </summary>
    public class CatalogosFrecuencias
    {
        #region Constantes
        private const int primerDia = 1;
        #endregion

        #region Propiedades privadas y axuilares
        /// <summary>
        /// Lista encargado de almacenar los argumentos de los años
        /// </summary>
        private List<CatGenerico> CategoriaAnio { get; set; }

        /// <summary>
        /// Lista encargado de almacenar los argumentos de los meses
        /// </summary>
        private List<CatGenerico> CategoriaMes { get; set; }

        /// <summary>
        /// Lista encargado de almacenar los argumentos de los días
        /// </summary>
        private List<CatGenerico> CategoriaDias { get; set; }

        /// <summary>
        /// arg para el agoritmo que calculara los meses validos
        /// </summary>
        private Dictionary<int, string> MesesValidos
        {
            get => new()
            {
                { 1, "Enero" },
                { 2, "Febrero" },
                { 3, "Marzo" },
                { 4, "Abril" },
                { 5, "Mayo" },
                { 6, "Junio" },
                { 7, "Julio" },
                { 8, "Agosto" },
                { 9, "Septiembre" },
                { 10, "Octubre" },
                { 11, "Noviembre" },
                { 12, "Diciembre" }
            };
        }
        #endregion

        #region Propiedades publicas
        /// <summary>
        /// Propiedad para obtener el anios en una linea
        /// </summary>
        public List<string> Anio { get => CategoriaAnio.Select(s => s.c_nombre).ToList(); }

        /// <summary>
        /// Propiedad para obtener el meses en una linea
        /// </summary>
        public List<string> Mes { get => CategoriaMes.Select(s => s.c_nombre).ToList(); }

        /// <summary>
        /// Propiedad para obtener el dias en una linea
        /// </summary>
        public string Dia { get => string.Join(", ", CategoriaDias.Select(s => s.c_nombre).ToArray()); }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor inical
        /// </summary>
        /// <param name="catAnio">arg de los anios</param>
        /// <param name="catMes">arg de los meses</param>
        /// <param name="catDias">arg de los dias</param>
        public CatalogosFrecuencias(string catAnio, string catMes, string catDias)
        {
            CategoriaAnio = new();
            CategoriaMes = new();
            CategoriaDias = new();

            if (!string.IsNullOrEmpty(catAnio) && catAnio.Length > 0)
                CategoriaAnio = JsonConvert.DeserializeObject<List<CatGenerico>>(catAnio);

            if (!string.IsNullOrEmpty(catAnio) && catAnio.Length > 0)
                CategoriaMes = JsonConvert.DeserializeObject<List<CatGenerico>>(catMes);

            if (!string.IsNullOrEmpty(catAnio) && catAnio.Length > 0)
                CategoriaDias = JsonConvert.DeserializeObject<List<CatGenerico>>(catDias);

        }
        #endregion

        #region Métodos publicos
        /// <summary>
        /// Método encargado de obtener los meses validos por el anio actual
        /// </summary>
        /// <param name="anio"></param>
        /// <returns></returns>
        public string DatosMostrar(string anio)
        {
            var AnioActual = $"{DateTime.Now.Year}";

            var mesesValidos = MesesValidos.Where(w => w.Key >= DateTime.Now.Month).Select(s => s.Value).ToList();

            if (anio == AnioActual)
            {
                var meses = CategoriaMes.Where(w => mesesValidos.Contains(w.c_nombre)).Select(s => s.c_nombre).ToArray();
                return string.Join(" ", meses);
            }
            else 
            {
               return  string.Join(" ", CategoriaMes.Select(s => s.c_nombre).ToArray());  
            }
        }

        /// <summary>
        /// Método encargado de validar el mes actual
        /// </summary>
        /// <param name="anio"></param>
        /// <param name="mes"></param>
        /// <returns></returns>
        public string DatosMostrarMes(string anio, string mes)
        {
            var AnioActual = $"{DateTime.Now.Year}";

            var mesesValidos = MesesValidos.Where(w => w.Key >= DateTime.Now.Month).Select(s => s.Value.ToUpper()).ToList();

            if (anio == AnioActual) 
            {
                if (mesesValidos.Any(elemento => elemento == mes.ToUpper()))
                    return mes; 
                else
                    return string.Empty;
            }
            else 
                return mes; 
        }

        /// <summary>
        /// Método encargado de obtener los días del mes.
        /// </summary>
        /// <param name="anio">Datos del anio</param>
        /// <param name="mes">Datos del mes</param>
        /// <returns></returns>
        public string DatosMotrarTodosLosDias(string anio, string mes)
        {
            int.TryParse(anio, out int anioActual);

            var mesActual = MesesValidos.Where(w => w.Value == mes).Select(s => s.Key).FirstOrDefault();

            if (anioActual == DateTime.Now.Year && mesActual == DateTime.Now.Month)
                return ProcesoObtenerDias(anioActual, mesActual, noEsMesActual: false);
            else
                return ProcesoObtenerDias(anioActual, mesActual);
        }     
        #endregion

        #region Métodos privados
        /// <summary>
        /// Método encargado de obtner el ultimo día del mes correspondiente
        /// </summary>
        /// <param name="anioActual">Fecha del año</param>
        /// <param name="mesActual">Fecha del mes</param>
        /// <returns></returns>
        private static DateTime ObtenerUltimoDiaMes(int anioActual, int mesActual)
            => new DateTime(anioActual, mesActual, primerDia).AddMonths(1).AddDays(-1);

        /// <summary>
        /// Método encargado de obtener la descripn del día
        /// </summary>
        /// <param name="fechaTemporal">Fecha temporal creada</param>
        /// <returns></returns>
        private static string ObtenerDescripcionDelDia(DateTime fechaTemporal)
            => fechaTemporal.ToString("dddd", new CultureInfo("es-MX")).ToUpper();

        /// <summary>
        /// Metodo para obtener los dias en el siguiente formato Ejemplo : 1,3,5,6 y 30 de tal mes del anio (XXXX)
        /// </summary>
        /// <param name="anioActual">Fecha del año</param>
        /// <param name="mesActual">Fecha del mes</param>
        /// <param name="noEsMesActual">Argummento para saber con que día va iniciar</param>
        /// <returns></returns>
        private string ProcesoObtenerDias(int anioActual, int mesActual, bool noEsMesActual = true)
        {
            string descripcionDias = string.Empty;

            var lstDias = new List<string>();

            var ultimoDiaMes = ObtenerUltimoDiaMes(anioActual, mesActual);

            for (int dia = noEsMesActual ? primerDia : DateTime.Now.Day; dia <= ultimoDiaMes.Day; dia++)
            {
                var fechaTemporal = new DateTime(anioActual, mesActual, dia);

                var tienesElDia = CategoriaDias.Where(w => w.c_nombre.ToUpper() == ObtenerDescripcionDelDia(fechaTemporal))
                                               .Select(s => s.c_nombre)
                                               .FirstOrDefault();

                if (!string.IsNullOrEmpty(tienesElDia) && dia < ultimoDiaMes.Day)
                {
                    lstDias.Add($"{fechaTemporal.Day}");
                }
                else if (dia == ultimoDiaMes.Day)
                {
                    if (string.IsNullOrEmpty(tienesElDia))
                    {
                        string ultimoValor = lstDias.LastOrDefault();
                        lstDias.Remove(ultimoValor);
                        descripcionDias = string.Join(", ", lstDias.Select(s => s).ToArray());
                        descripcionDias += $" y {ultimoValor} de {fechaTemporal.ToString("MMMM", new CultureInfo("es-MX")) } de { anioActual }.";
                    }
                    else 
                    {
                        descripcionDias = string.Join(", ", lstDias.Select(s => s).ToArray());
                        descripcionDias += $" y {fechaTemporal.Day} de {fechaTemporal.ToString("MMMM", new CultureInfo("es-MX")) } de { anioActual }.";
                    }
                    
                }
            }

            return descripcionDias; 
        }
        #endregion
    }
}
