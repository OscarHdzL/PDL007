using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades.GestionCreacionDocumentos.POCOs;

namespace Utilidades.GestionCreacionDocumentos.Interface
{
    /// <summary>
    /// Contrato de ejecutar en la fabrica
    /// </summary>
    public interface ITipoDocumento
    {
        /// <summary>
        /// Enumerador para saber el tipo de documento
        /// </summary>
        EnumTipoDocumento Tipo { get; }
    }
}
