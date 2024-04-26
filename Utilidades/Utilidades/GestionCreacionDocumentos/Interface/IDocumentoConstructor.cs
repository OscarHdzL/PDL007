using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades.GestionCreacionDocumentos.Interface
{
    /// <summary>
    /// Contrato para la creacion de un documento
    /// </summary>
    public interface IDocumentoConstructor
    {
        /// <summary>
        /// Método encargado de iniciar un nuevo documento.
        /// </summary>
        void Iniciar();

        /// <summary>
        ///  Método encargado de agregar un elemento.
        /// </summary>
        /// <param name="texto"></param>
        void AgregarElemento(string elemento);

        /// <summary>
        /// Método encargado de crear o escribir el docuemnto.
        /// </summary>
        /// <returns></returns>
        void Crear();
    }
}
