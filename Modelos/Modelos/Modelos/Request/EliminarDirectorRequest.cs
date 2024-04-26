using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class EliminarDirectorRequest
    {
        public int? director_id { get; set; }
        public bool? director_activo { get; set; }
    }
}
