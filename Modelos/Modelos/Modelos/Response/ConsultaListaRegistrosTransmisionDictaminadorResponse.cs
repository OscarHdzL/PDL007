using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
   public  class ConsultaListaRegistrosTransmisionDictaminadorResponse
    {
        public int numero_registro { get; set; }
        public string folio { get; set; }
        public int id_transmision { get; set; }
        public string domicilio { get; set; }
        public string numero_sgar { get; set; }
        public string denominacion { get; set; }
        public string representante { get; set; }
        public int id_estatus { get; set; }
        public string estatus { get; set; }
        public DateTime? fecha_solicitud { get; set; }
        public DateTime? fecha_autorizacion { get; set; }
    }
}
