using Modelos.Modelos.Response;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Utilidades.GestionCreacionDocumentos.POCOs.Modelos
{
    public class ModeloEncabezadoOficioDeclaratoria
    {
        #region Propiedades
        public ModeloValor Fecha { get; private set; }
        public List<ModeloValor> DatosDetalle { get; private set; }
        public List<ModeloValor> DatosReferencia { get; private set; }
        #endregion
    
    
        public ModeloEncabezadoOficioDeclaratoria(ConsultarTramiteDeclaratoriaPaso1 response, string domicilio) {
            
            Fecha = new ModeloValor($"Ciudad de México a { DateTime.Now.Day } de {DateTime.Now.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-MX"))} de {DateTime.Now.Year}", esTitulo: true);
            DatosDetalle = new List<ModeloValor>
            {
                new ModeloValor($"C. {response.nombre_completo.ToUpper()}", esTitulo: true),
                new ModeloValor($"Representante legal de {response.denominacion_religiosa}", esTitulo: true),
                new ModeloValor(response.numero_sgar),
                new ModeloValor(domicilio)
            };
            DatosReferencia = new List<ModeloValor>
            {
                new ModeloValor("OFICIO: ", esTitulo: true),
                new ModeloValor($"{response.numero_sgar}", esTitulo: false),
                new ModeloValor("REFERENCIA: ", esTitulo: true),
                new ModeloValor("FOLIO: ", esTitulo: true)
            };
        }
        
        /*public ModeloEncabezadoOficioDeclaratoria(ConsultaDatosOficio response) {
            
            DatosDetalle = new List<ModeloValor>
            {
                new ModeloValor("OFICIO",     ':', response.oficio),
                new ModeloValor("", ':', response.sgar),
                new ModeloValor("REFERENCIA", ':', response.referencia),
                new ModeloValor("FOLIO",     ':', response.oficio)
            };
        }*/
    }
}
