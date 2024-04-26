using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class ConsultaMunicipiosResponse
    {
        public int? IdMunicipio { get; set; }
        public string Nombre { get; set; }
        public int? Clave { get; set; }
        public string Descripcion { get; set; }
        public int? CodigodeError { get; set; }
    }
}
