using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades.GestionCreacionDocumentos.POCOs.Modelos
{
    /// <summary>
    /// Clase de ejecutar las configuraciones que tendran los valores de los renglones
    /// </summary>
    public class ModeloValor
    {
        #region Propiedades
        /// <summary>
        /// Propiedad para obtener un valor 
        /// </summary>
        public string Descripcion { get; private set; }

        /// <summary>
        /// Propiedad para obtener la clave
        /// </summary>
        public string ClaveDescricion { get; private set; }

        /// <summary>
        /// Propiedad para indicar si es un texto
        /// </summary>
        public bool EsTextoCompuesto { get; private set; } = false;

        /// <summary>
        /// Propiedad para saber si tiene negritas
        /// </summary>
        public bool TieneNegrita { get; set; } = false;

        /// <summary>
        /// Propiedad para saber si tiene negritas
        /// </summary>
        public bool EsTituloResaltado { get; set; } = false;
        #endregion

        #region Constructor
        /// <summary>
        /// Contructor Mostrar EJEMPLO: XXXXXX
        /// </summary>
        /// <param name="claveDescricion">Descricion del texto</param>
        /// <param name="texto">Valor del texto </param>
        /// <param name="caracter">Valor del caracter </param>
        public ModeloValor(string claveDescricion, char caracter, string texto)
        {
            Descripcion = texto;
            ClaveDescricion = $"{claveDescricion}{caracter} ";
            EsTextoCompuesto = true; 
        }

        /// <summary>
        /// Contructor Mostrar  un texto o un renglon
        /// </summary>
        /// <param name="texto">Texto que puede ser un titulo o solo texto</param>
        /// <param name="esTitulo">Inidica si llevara negritas el texto o titulo</param>
        public ModeloValor(string texto, bool esTitulo)
        {
            Descripcion = $"{texto}";
            EsTituloResaltado = esTitulo;
        }

        /// <summary>
        /// Contructor Mostrar  un texto o un renglon
        /// </summary>
        /// <param name="texto">Texto que puede ser un titulo o solo texto</param>
        public ModeloValor(string texto)
        {
            Descripcion = $"{texto}";
        }
        #endregion
    }
}
