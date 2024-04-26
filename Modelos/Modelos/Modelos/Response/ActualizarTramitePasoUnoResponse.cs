using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    /// <summary>
    /// Clase del response del registro compleo del usuario
    /// </summary>
    public class ActualizarTramitePasoUnoResponse
    {
        public int id_tramite { get; set; }

        public string mensaje { get; set; }
        public bool proceso_existoso { get; set; }
    }
}
