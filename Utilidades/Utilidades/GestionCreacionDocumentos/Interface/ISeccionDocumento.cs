using Utilidades.GestionCreacionDocumentos.POCOs.Propiedades;

namespace Utilidades.GestionCreacionDocumentos.Interface
{
    /// <summary>
    /// Contrato para la creacion de las secciones de los documentos
    /// </summary>
    public interface ISeccionDocumento
    {
        /// <summary>
        /// Método encargado de agregar el texto de un parrafo.
        /// </summary>
        void AgregarParrafo(PropiedadesDocumentoTexto propiedad, int OrdenElemento );

        /// <summary>
        /// Método encargado de agregar una tabla al documento.
        /// </summary>
        void AgregarTabla(PropiedadesDocumentoTabla propiedad, int OrdenElemento);
    }
}
