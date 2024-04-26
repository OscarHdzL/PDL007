using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class ConsultaListaPerfilesResponse
    {
        public int? id_perfil { get; set; }
        public string nombre_perfil { get; set; }
        public string descripcion_perfil { get; set; }
        public DateTime? f_ini_vig_perfil{ get; set; }
        public DateTime? f_fin_vig_perfil { get; set; }
        public Boolean? activo_perfil { get; set; }
    }
}
