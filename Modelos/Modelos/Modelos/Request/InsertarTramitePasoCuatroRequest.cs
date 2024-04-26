using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{
    /// <summary>
    /// Clase del request para el registro paso uno tramite
    /// </summary>
    public class InsertarTramitePasoCuatroRequest
    {
        public int s_superficie { get; set; }
        public int s_medidas { get; set; }
        public string s_colindancia_text_1 { get; set; }
        public string s_colindancia_text_2 { get; set; }
        public string s_colindancia_text_3 { get; set; }
        public string s_colindancia_text_4 { get; set; }
        public int s_aviso_apertura { get; set; }


    }
}
