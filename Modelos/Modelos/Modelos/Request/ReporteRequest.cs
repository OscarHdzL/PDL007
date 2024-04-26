using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class ReporteRequest
    {
        public int? EntidadRegistro { get; set; }
        public int? CredoRegistro { get; set; }
        public int? movRealizado { get; set; }
        public int? MunicipioRegistro { get; set; }
        public int? EstatusRegistro { get; set; }
        public DateTime? FechaI { get; set; }
        public DateTime? FechaF { get; set; }
        public int? Ttramite { get; set; }

    }
}
