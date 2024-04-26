using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class InsertarAnexoRequest
    {
        public int id_toma_nota { get; set; }
        public string nombre_anexo { get; set; }
        public string url_anexo { get; set; }
        public int id_tramite { get; set; }
    }
}
