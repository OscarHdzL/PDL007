using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class ActualizarTomaNotaEstatutosDenominacionResponse
    {
        public int id_toma_nota { get; set; }

        public string mensaje { get; set; }
        public bool proceso_exitoso { get; set; }
    }
}
