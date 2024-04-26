using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class FinalizarTomaNotaRequest
    {
        public int? i_id_trtn { get; set; }
        public int? s_id_us { get; set; }

        public bool b_estatutos { get; set; }
        public bool b_denominacion { get; set; }
        public bool b_miembros { get; set; }
        public bool b_representante { get; set; }
        public bool b_apoderado { get; set; }
        public bool b_dom_legal { get; set; }
        public bool b_dom_notificacion { get; set; }

    }
}
