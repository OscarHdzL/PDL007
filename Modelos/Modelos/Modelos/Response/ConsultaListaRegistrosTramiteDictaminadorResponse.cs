using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class ConsultaListaRegistrosTramiteDictaminadorResponse
    {
        public int id_registro { get; set; }
        public string nombre { get; set; }
        public string denominacion { get; set; }
        public string credo { get; set; }
        public string folio { get; set; }
        public DateTime fecha_registro { get; set; }
        public string sol_registro { get; set; }
        public int? id_estatus { get; set; }
        public string? estatus { get; set; }
    }
}
