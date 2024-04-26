﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class ActualizarMediosTransmisionRequest
    {
		public int? id { get; set; }
		public string frecuencia_canal { get; set; }
		public string proveedor { get; set; }
		public string televisora_radiodifusora { get; set; }
		public int? lugar_transmision { get; set; }
		public int? televisora { get; set; }

	}
}
