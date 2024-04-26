using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Response
{
   public  class ReporteContactosResponse
    {
		  public int? id { get; set; }
		  public int? id_tbl_direcciones { get; set; }
		  public string estado { get; set; }
		  public string municipio { get; set; }
		  public int? id_tipo_usuarios { get; set; }
		  public int? id_tbl_escuelas { get; set; }
		  public string escuela { get; set; }
		  public int? id_ca_tipo_escuelas { get; set; }
		  public string tipo_escuela { get; set; }
		  public string nombre { get; set; }
		  public string apellido_paterno { get; set; }
		  public string apellido_materno { get; set; }
		  public string curp { get; set; }
		  public DateTime? fecha_nacimiento { get; set; }
		  public int? edad { get; set; }
		  public string sexo { get; set; }
		  public int? id_padre_tutor { get; set; }
		  public string  nombretutor{ get; set; }
	      public string  appaterno{ get; set; }
          public string  apmaterno{ get; set; }
          public string correo_electronico { get; set; }
		  public string telefono_movil { get; set; }
		  public string telefono_fijo { get; set; }
		  public string nacionalidad { get; set; }

	}
}
