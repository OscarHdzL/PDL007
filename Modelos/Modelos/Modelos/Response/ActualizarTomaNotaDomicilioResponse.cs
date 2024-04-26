using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class ActualizarTomaNotaDomicilioResponse
    {
        public int id_domicilio { get; set; }

        public string mensaje { get; set; }
        public bool proceso_existoso { get; set; }
    }
}
