using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class InsertarObservacionTransmisionResponse
    {
        public string mensaje { get; set; }
        public int usuario { get; set; }
        public string destinatario { get; set; }
        public string observacion_emitida { get; set; }
        public bool proceso_exitoso { get; set; }
    }
}
