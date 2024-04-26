using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class ConsultaOficioTransmisionResponse
    {
        public int id_transmision { get; set; }
        public string referencia { get; set; }
        public string expediente { get; set; }
        public string oficio { get; set; }
        public int id_firmante { get; set; }
        public string nombre_firmante { get; set; }
        public string cargo_firmante { get; set; }
        public string titulo_firmante { get; set; }
        public string puesto_firmante { get; set; }
        public int id_ccp { get; set; }
        public string nombre_ccp { get; set; }
        public string cargo_ccp { get; set; }
        public string titulo_ccp { get; set; }
        public string nombre_dictaminador { get; set; }
        public string nombre_asignador { get; set; }
    }
}
