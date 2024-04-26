﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    /// <summary>
    /// Clase del response para consultar datos
    /// </summary>
    public class ConsultaListaCaalogoSJuridicaResponse
    {
        public int? c_id { get; set; }
        public string c_nombre { get; set; }
        public string c_descripcion { get; set; }
        public DateTime? c_f_inic_vig { get; set; }
        public DateTime? c_f_fin_vig { get; set; }
        public  bool c_activo { get; set; }
    }
}
