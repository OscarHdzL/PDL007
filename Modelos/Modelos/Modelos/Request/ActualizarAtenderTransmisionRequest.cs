using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class ActualizarAtenderTransmisionRequest
    {
        public int id_transmision { get; set; }
        public string referencia { get; set; }
        public string expediente { get; set; }
        public string oficio { get; set; }
        public int id_firmante { get; set; }
        public string puesto_firmante { get; set; }
        public int id_ccp { get; set; }
    }
}
