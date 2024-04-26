using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class ConsultaListaCatalogoDirectorResponse
    {
        public int? director_id { get; set; }
        public string director_nombre { get; set; }
        public string director_apaterno { get; set; }
	    public string director_amaterno { get; set; }
        public string director_titulo { get; set; }
	    public string director_cargo { get; set; }
        public bool? director_activo { get; set; }

    }
}
