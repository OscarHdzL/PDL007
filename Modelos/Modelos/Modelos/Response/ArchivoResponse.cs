using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class ArchivoResponse
    {
        public long id { get; set; }
        public string ruta { get; set; }
        public bool proceso_exitoso { get; set; }
        public string ext { get; set; }
    }
    
    public class PlantillaResponse
    {
        public int id_plantilla { get; set; }
        public string ruta { get; set; }
    }
    
    public class PlantillaBaseResponse
    {
        public string extension { get; set; }
        public string base64 { get; set; }
    }
}
