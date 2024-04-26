using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class ActualizarCatalogoSJuridicaRequest
    {
        public int c_id { get; set; }
        public string c_nombre { get; set; }
        public string c_descripcion { get; set; }
        public string c_f_inic_vig { get; set; }
        public string c_f_fin_vig { get; set; }
}
}
