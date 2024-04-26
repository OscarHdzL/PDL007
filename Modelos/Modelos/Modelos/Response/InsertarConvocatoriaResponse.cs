﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    /// <summary>
    /// Clase del response del registro compleo del usuario
    /// </summary>
    public class InsertarConvocatoriaResponse
    {
        public int id_convocatoria { get; set; }
        public string mensaje { get; set; }
        public bool? proceso_exitoso { get; set; }
    }
}
