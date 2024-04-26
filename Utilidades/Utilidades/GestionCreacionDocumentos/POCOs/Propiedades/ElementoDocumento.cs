using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Document.NET;

namespace Utilidades.GestionCreacionDocumentos.POCOs.Propiedades
{
    /// <summary>
    /// Elementos de un documento
    /// </summary>
    public class ElementoDocumento
    {
        #region Propiedades
        public bool Negritas { get; set; }
        public Alignment Alinacion { get; set; }
        public double TamanioLetra { get; set; }
        public string Letra { get; set; }
        public double EspacioEntreParrafos { get; set; }
        #endregion
    }
}
