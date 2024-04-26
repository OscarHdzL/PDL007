using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class InsertarTransmisionActosReligiososResponse
    {
        public int id_acto_religioso { get; set; }
        public string mensaje { get; set; }
        public bool proceso_exitoso { get; set; }
    }

    public class InsertarActosFechasResponse
    {
        public string mensaje { get; set; }
        public bool proceso_exitoso { get; set; }
    }

    public class InsertarActosEmisorasResponse
    {
        public string mensaje { get; set; }
        public bool proceso_exitoso { get; set; }
    }
}
