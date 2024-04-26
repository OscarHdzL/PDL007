namespace Modelos.Modelos.Request
{
    public class InsertarTramiteDeclaratoriaPaso1
    {
        public int p_id_declaratoria { get; set; }
        public string p_nombre_completo { get; set; }
        public string p_denominacion_religiosa { get; set; }
        public string p_numero_sgar { get; set; }
        public int p_i_id_tbl_cargo { get; set; }  // 1 -Representante Legal // 2 -Apoderado Legal
        public int p_i_id_tbl_usuario { get; set; }
    }
    
    public class InsertarTramiteDeclaratoriaPaso2
    {
        public int p_id_declaratoria { get; set; }
        public string p_calle { get; set; }
        public string p_numeroe { get; set; }
        public string p_numeroi { get; set; }
        public int p_i_id_tbl_colonia { get; set; }
        public int p_tipo_domicilio { get; set; } 
         // 1 -Domicilio Legal
         // 2 -Domicilio Notificación
        public string p_lote { get; set; }
        public string p_manzana { get; set; }
        public string p_super_manzana { get; set; }
        public string p_delegacion { get; set; }
        public string p_sector { get; set; }
        public string p_zona { get; set; }
        public string p_region { get; set; }
        
        public string p_personas_aut { get; set; }
        
        //public string p_numero_telefono { get; set; }
        //public string p_correo_electronico { get; set; }
    }

    public class InsertarTramiteDeclaratoriaPaso4
    {
        public int p_id_declaratoria { get; set; }
        public string p_superficie { get; set; }
        public int p_unidad { get; set; }
        public string p_ubicacion { get; set; }
        public bool p_culto_publico { get; set; }
        public string p_inicio_actividades { get; set; }
        public int p_uso { get; set; }
        public double p_norte { get; set; }
        public double p_noreste { get; set; }
        public double p_noroeste { get; set; }
        public double p_sur { get; set; }
        public double p_sureste { get; set; }
        public double p_suroeste { get; set; }
        public double p_oriente { get; set; }
        public double p_poniente { get; set; }
        public string p_otro { get; set; }
        public string p_colindancia { get; set; }
        public string p_descripcion_salida { get; set; }
        public bool p_regular { get; set; }
    }
    
    public class InsertarTramiteDeclaratoriaPaso5
    {
        public int p_id_declaratoria { get; set; }
        public bool p_manifiesto_verdad { get; set; }
    }
}