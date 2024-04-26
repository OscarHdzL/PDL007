using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class InsertarCatalogoDirectorResponse
    {
        public int? id { get; set; }
		public bool? proceso_exitoso { get; set; }
		public string mensaje { get; set; }

    }
}
