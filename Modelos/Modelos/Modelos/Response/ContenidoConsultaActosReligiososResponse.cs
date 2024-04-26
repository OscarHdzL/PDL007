using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class ContenidoConsultaActosReligiososResponse
    {
        #region Propiedades
        public List<ConsultaActosReligiososResponse> ConsultaActosReligiosos { get; set; }
        public List<ConsultaActosMediosTrasmisionResponse> ConsultaActosMediosTrasmision { get; set; }
        public List<ConsultaActosFechasResponse> ConsultaActosFechas { get; set; }
        public List<ConsultaActosFechasResponse> ConsultaActosFrecuencia { get; set; }
        public ConsultaDetalleTramiteTransmisionResponse ConsultaDetalleTramiteTransmisions { get; set; }
        public ConsultaOficioTransmisionResponse ConsultaOficioTransmisions { get; set; }
        public string RutaDocumento { get; set; }
        #endregion

        #region Constructor
        public ContenidoConsultaActosReligiososResponse()
        {
            ConsultaActosReligiosos = new();
            ConsultaActosMediosTrasmision = new();
            ConsultaActosFechas = new();
            ConsultaActosFrecuencia = new();
            ConsultaDetalleTramiteTransmisions = new();
            ConsultaOficioTransmisions = new();
        }
        #endregion
    }
}
