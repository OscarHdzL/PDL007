using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
   public class ConsultaListaRegistrosTomaNotaResponse
    {
        public int id_tramite { get; set; }
        public int id_tnota { get; set; }
        public string denominacion { get; set; }
        public DateTime? fecha_registro { get; set; }
        public DateTime? fecha_autorizacion { get; set; }
        public string nombre { get; set; }
        public string tramite_solicitado{ get; set; }
        public string folio { get; set; }
        public int id_estatus { get; set; }
        public string estatus { get; set; }
	}
}
