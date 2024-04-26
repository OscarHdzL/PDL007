using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class ConsultaDetalleAnexoAsuntoResponse
    {
        public string nombre_anexo { get; set; }
        public string url_anexo { get; set; }
        [NotMapped]
        public string base64_anexo { get; set; }
    }
}
