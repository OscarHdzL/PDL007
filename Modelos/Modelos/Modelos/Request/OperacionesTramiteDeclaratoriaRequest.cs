namespace Modelos.Modelos.Request
{
    public class ActivarTramiteDeclaratoria
    {
        public int p_id_declaratoria { get; set; }
        public bool p_activo { get; set; }
    }
    public class AsignarDeclaratoria
    {
        public int p_id_declaratoria { get; set; }
        public int p_id_dictaminador { get; set; }
        public int p_id_asignador { get; set; }
    }
    
    public class ComentariosDeclaratoria
    {
        public int p_id_declaratoria { get; set; }
        public string p_comentarios { get; set; }
        
    }
    
    public class ActualizarEstatusDeclaratoria
    {
        public int p_id_declaratoria { get; set; }
    }
    
    public class AutorizarDeclaratoria
    {
        public int p_id_declaratoria { get; set; }
        public string p_fecha { get; set; }
        public string p_horario { get; set; }
        public string p_direccion { get; set; }
    }

    public class ConcluirDeclaratoria
    {
        public int p_id_declaratoria { get; set; } 
        public int p_estatus { get; set; }
        public string p_comentarios { get; set; }
    }
}