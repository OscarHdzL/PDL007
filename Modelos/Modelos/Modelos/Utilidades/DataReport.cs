using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Utilidades
{
    /// <summary>
    /// Modelo de la información del reporte (Respuesta del reporte creado)
    /// </summary>
    public class DataReport
    {
        public string NombreDocumento { get; set; }
        public byte[] DocumentoPDF { get; set; }
    }
}
