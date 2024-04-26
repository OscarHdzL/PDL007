using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    /// <summary>
    /// Clase del response para consultar datos
    /// </summary>
    public class ConsultaDetalleTramitePasoCuatroResponse
    {
        public int? s_id { get; set; }
        public string s_superficie { get; set; }
        public string s_medidas { get; set; }
        public string s_usos { get; set; }
        public int? s_aviso_apertura { get; set; }
        public string s_colindancia_text_1 { get; set; }
        public string s_colindancia_text_2 { get; set; }
        public string s_colindancia_text_3 { get; set; }
        public string s_colindancia_text_4 { get; set; }
        public double? s_colindancia_num_1 { get; set; }
        public double? s_colindancia_num_2 { get; set; }
        public double? s_colindancia_num_3 { get; set; }
        public double? s_colindancia_num_4 { get; set; }
        public bool? s_ine_propietario_file { get; set; }
        public bool? s_ine_usuario_file { get; set; }
        public bool? s_titulo_propiedad_file { get; set; }
        public bool? s_aviso_apertura_file { get; set; }
        public bool? s_estatuto_file { get; set; }
    }
}
