using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class ConsultaPermisoPersonaResponse
    {
        public int? idTramite { get; set; }
        public int? idEstatusTramite { get; set; }
        public Boolean? bndRegistro { get; set; }
        public Boolean? bndConsultaR { get; set; }
        public Boolean? bndTomaNota { get; set; }
        public Boolean? bndConsultaTN { get; set; }
    }
}
