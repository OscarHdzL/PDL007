﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class CatalogoCnotarioarrInsertRequest
    {
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string f_inic_vig { get; set; }
        public string f_fin_vig { get; set; }
        public int? i_tipo_escrito { get; set; }
    }
}
