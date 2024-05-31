using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
    public class ConsultaDetalleMovimientosTomaNotaResponse
    {
        public int? s_id { get; set; }
        public int? s_cat_mov { get; set; }
        public int? s_cat_tnota { get; set; }
        public string s_movimiento { get; set; }
        public bool s_activo { get; set; }
    }

    public class ConsultaCatalogoMovimientosTomaNotaResponse
    {
        public int s_id { get; set; }
        public string s_movimiento { get; set; }
    }
}
