using Modelos.Modelos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades.GestionCreacionDocumentos.POCOs.Modelos
{
    /// <summary>
    /// Clase de convertir el texto.
    /// </summary>
    public class ModeloActosReligiosos
    {
        #region Propiedades
        public ModeloValor Titulo { get; private set; }
        public List<ModeloValor> DatosReligioso { get; private set; }
        #endregion

        #region Constructor
        public ModeloActosReligiosos(List<ConsultaActosReligiososResponse> responses)
        {
            DatosReligioso = new List<ModeloValor>
            {
               new  ModeloValor("Acto religioso", esTitulo: true),
            };

            DatosReligioso.AddRange(responses.Select(s => new ModeloValor(s.c_nombre)).ToList());
        }
        #endregion
    }
}
