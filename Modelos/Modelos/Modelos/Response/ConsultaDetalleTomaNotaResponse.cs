using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class ConsultaDetalleTomaNotaResponse
    {
        public int c_tramite { get; set; }
        public string c_numero_sgar { get; set; }
        public string c_denominacion { get; set; }
        public bool c_existe_escrito_solicitud { get; set; }
        public bool c_existe_estatuto_solicitud { get; set; }
        public bool c_existe_certificado_reg_solicitud { get; set; }
        public bool c_existe_ejemplar_est_solicitud { get; set; }
        public bool c_escritura_publica { get; set; }
        public bool c_alta_apoderado_doc { get; set; }
        public bool c_baja_apoderado_doc { get; set; }
        public bool c_cambio_apoderado_doc { get; set; }
        public int? c_id_tsol_escrito { get; set; }
        public int? c_cotejo { get; set; }
        public int c_toma_nota { get; set; }
        public string c_coment_est { get; set; }
        public string c_n_denom { get; set; }
        public string c_coment_n_denom { get; set; }
        public int c_trtn { get; set; }
        public int? c_us { get; set; }
        public int? status { get; set; }
    }

    public class ConsultaDetalleTomaNotaInfoResponse
    {
        public int c_tramite { get; set; }
        public string c_numero_sgar { get; set; }
        public string c_denominacion { get; set; }
        public string c_comentario_tnota { get; set; }
        public bool c_existe_escrito_solicitud { get; set; }
        public bool c_existe_estatuto_solicitud { get; set; }
        public bool c_existe_certificado_reg_solicitud { get; set; }
        public bool c_existe_ejemplar_est_solicitud { get; set; }
        public bool c_escritura_publica { get; set; }
        public bool c_alta_apoderado_doc { get; set; }
        public bool c_baja_apoderado_doc { get; set; }
        public bool c_cambio_apoderado_doc { get; set; }
        public int? c_id_tsol_escrito { get; set; }
        public int? c_cotejo { get; set; }
        public int c_toma_nota { get; set; }
        public string c_coment_est { get; set; }
        public string c_n_denom { get; set; }
        public string c_coment_n_denom { get; set; }
        public int c_trtn { get; set; }
        
    }
}
