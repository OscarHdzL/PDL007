using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class ConsultaListaTomaNotaRequest
    {
        public string keyword { get; set; }
        public int c_id { get; set; }
        public bool c_activo { get; set; }
        public int? start { get; set; }
        public int? length { get; set; }

        public string order { get; set; }

        //[NotMapped]
        //public int tipoOperacion { get; set; }
        [NotMapped]
        public string column { get; set; }
    }
}
