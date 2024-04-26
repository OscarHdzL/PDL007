using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class ArchivoRequest
    {
        public long id { get; set; }
        public string archivo { get; set; }
        public int idArchivoTramite { get; set; }
    }

    public class ArchivosRequest
    {
        public int? transmisionId { get; set; } = 0;
    }
}
