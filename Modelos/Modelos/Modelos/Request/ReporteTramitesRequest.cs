namespace Modelos.Modelos.Request
{
    public class ReporteTramitesRequest
    {
        public string p_fecha_inicio { get; set; } 
        public string p_fecha_fin { get; set; } 
        public int? p_id_estatus { get; set; }
    }
}
