using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class ReporteTransmisionResponse
    {
        public int? transmision_id { get; set; }
        //public string transmision_domicilio { get; set; }
        //public string transmision_correo_electronico { get; set; }
        //public string transmision_numero_tel { get; set; }
        public DateTime? transmision_fecha_solicitud { get; set; }
        //public int? acto_religioso_id { get; set; }
        //public string acto_religioso_nombre { get; set; }
        //public string medio_frecuencia_canal { get; set; }
        //public string medio_proveedor { get; set; }
        //public string medio_tel_radio { get; set; }
        //public bool? medio_b_televisora { get; set; }
        //public DateTime? acto_fecha_inicio { get; set; }
        //public DateTime? acto_fecha_fin { get; set; }
        public string registro_nregistro { get; set; }
        public string registro_denominacion { get; set; }
        public string estatus_nombre { get; set; }
        public string representante_nombre_completo { get; set; } 
        public string transmision_no_oficio { get; set; } 
        public DateTime? transmision_fecha_autorizacion { get; set; } 
        public int? no_transmisiones { get; set; }
        public int? no_dias { get; set; }
        public int? horarios_transmision { get; set; }
        public int? n_medios_transmision { get; set; }
        public int? total_transmisiones { get; set; }

    }
}
