namespace Modelos.Modelos.Response
{
    public class ReporteTramitesResponse
    {
        public int id_tramite { get; set; }
        public string tramite { get; set; }
        public int id_estatus { get; set; }
        public string estatus { get; set; }
        public string numero_sgar { get; set; }
        public string denominacion { get; set; }
        public string fecha_solicitud { get; set; }
    }
}
