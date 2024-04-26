using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class ConsultaDetalleTomaNotaRequest
    {
        public int? i_id_c { get; set; }
        public int? s_id_us { get; set; }
        public int? id_tramite { get; set; }
        //public bool? dictaminador { get; set; }
    }

    public class RequestParamMovimientos 
    {
        #region Propiedades
        public int? p_id_tramite { get; set; } = 0;
        public bool? estatutos { get; set; } = false;
        public bool? denominacion { get; set; } = false;
        public bool? rep_legal { get; set; } = false;
        public bool? apoderado { get; set; } = false;
        public bool? dom_notificaciones { get; set; } = false;
        public bool? dom_legal { get; set; } = false;
        #endregion
    }
}
