using System.Collections.Generic;

namespace Modelos.Modelos.Response
{
    public class ConsultarTramiteDeclaratoriaPaso1
    {
        public int id_declaratoria { get; set; }
        public string nombre_completo { get; set; }
        public string denominacion_religiosa { get; set; }
        public string numero_sgar { get; set; }
        public int i_id_tbl_cargo { get; set; }
        public bool declaratoria_verdad { get; set; }
        // 1 -Representante Legal
        // 2 -Apoderado Legal
    }
    
    public class ConsultarTramiteDeclaratoriaPaso2
    {
        public int id_declaratoria { get; set; }
        public int tipo_domicilio { get; set; }  // 2 -Domicilio Notificación // 3 -Domicilio Inmueble
        public string codigo_postal { get; set; }
        public int id_estado { get; set; }
        public string estado { get; set; }
        public int id_ciudad { get; set; }
        public string ciudad { get; set; }
        public string colonia { get; set; }
        public int clave_municipio { get; set; }
        public int id_domicilio { get; set; }
        public string calle { get; set; }
        public string numeroe { get; set; }
        public string numeroi { get; set; }
        public int id_colonia { get; set; }
        public string lote { get; set; }
        public string manzana { get; set; }
        public string super_manzana { get; set; }
        public string delegacion { get; set; }
        public string sector { get; set; }
        public string zona { get; set; }
        public string region { get; set; }
        public string personas_autorizadas { get; set; }
    }

    public class ConsultarTramiteDeclaratoriaPaso4
    {
        public int id_declaratoria { get; set; }
        public bool regular { get; set; }
        public int id_ubicacion { get; set; }
        public int unidad { get; set; }
        public string superficie { get; set; }
        public string colindancia { get; set; }
        public string descripcion_salida { get; set; }
        public double norte { get; set; }
        public double noreste { get; set; }
        public double noroeste { get; set; }
        public double sur { get; set; }
        public double sureste { get; set; }
        public double suroeste { get; set; }
        public double oriente { get; set; }
        public double poniente { get; set; }
        public string ubicacion { get; set; }
        public bool culto_publico { get; set; }
        public string inicio_actividades { get; set; }
        public int uso { get; set; }
        public string otro { get; set; }
        public int? imagen_ubicacion { get; set; }
    }
    
    public class ConsultarTramiteDeclaratoriaPaso5
    {
        public int id_declaratoria { get; set; }
        public bool declaratoria_verdad { get; set; }
        public bool? genera_oficio { get; set; }
        public int? peticion { get; set; }
        public int? anexo1 { get; set; }
        public int? anexo2 { get; set; }
    }
    
    public class ConsultarTramiteDeclaratoriaAvance
    {
        public int i_id { get; set; }
        public int id_declaratoria { get; set; }
        public bool paso1 { get; set; }
        public bool paso2 { get; set; }
        public bool paso3 { get; set; }
        public bool paso4 { get; set; }
        public bool paso5 { get; set; }
    }
    
    public class ConsultarTramiteDeclaratoriaLista
    {
        public int id_declaratoria { get; set; }
        public string folio { get; set; }
        public string denominacion_religiosa { get; set; }
        public int id_estatus { get; set; }
        public string estatus { get; set; }
        public string fecha_envio { get; set; }
        public string fecha_autorizacion { get; set; }
        public string comentarios { get; set; }
        public int? id_dictaminador { get; set; }
        public string correo_dictaminador { get; set; }
        public string nombre_dictaminador { get; set; }
    }

    public class ConsultaDatosOficio
    {
        public string oficio { get; set; }
        public string sgar { get; set; }
        public string referencia { get; set; }
        public string folio { get; set; }
    }

    public class ObtenerInfoOficio
    {
        public ConsultarTramiteDeclaratoriaPaso1 informacionPrincipal { get; set; }
        public string notificaciones { get; set; }
        public string domicilio { get; set; }
        public string uso { get; set; }
        public ConsultarTramiteDeclaratoriaPaso4 colindancia { get; set; }
        public string rutaPlantilla{ get; set; }
    }


}
