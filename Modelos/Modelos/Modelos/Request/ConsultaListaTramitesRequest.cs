﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    public class ConsultaListaTramitesRequest
    {

        public string keyword { get; set; }
        public int c_id { get; set; }
        public bool c_activo { get; set; }
        public int? start { get; set; }
        public int? length { get; set; }

        public string order { get; set; }

        //[NotMapped]
        //public int tipoOperacion { get; set; }
        [NotMapped]
        public string column { get; set; }
    }

    public class ConsultaListaTramitesAsignadorRequest
    {
        public bool? Activos { get; set; }
    }

    public class ConsultaListaTramitesByAsignadorRequest
    {
        public int id_asignador { get; set; }
    }
}
