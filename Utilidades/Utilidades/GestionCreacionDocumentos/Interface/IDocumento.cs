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
    public interface IDocumento
    {
        /// <summary>
        /// Método encargado generar el documento
        /// </summary>
        /// <returns></returns>
        ValueTask<byte[]> GenerarDocumento();
    }
}
