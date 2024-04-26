using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Modelos.Modelos.Response
{
    [XmlRoot("CURPStruct")]
    public class CURPStruct 
    {
        public string CURP { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string nombres { get; set; }
        public string sexo { get; set; }
        public string fechNac { get; set; }
        public string nacionalidad { get; set; }
        public string docProbatorio { get; set; }
        public string anioReg { get; set; }
        public string numActa { get; set; }
        public string numEntidadReg { get; set; }
        public string cveMunicipioReg { get; set; }
        public string cveEntidadNac { get; set; }
        public string statusCurp { get; set; }
        public string nivelConfiabilidad { get; set; }
        public string curpHistoricas { get; set; }
        public string edad { get; set; }
    }

}
