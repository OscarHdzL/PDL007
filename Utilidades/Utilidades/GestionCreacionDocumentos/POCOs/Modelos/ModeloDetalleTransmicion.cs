using Modelos.Modelos.Response;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Utilidades.GestionCreacionDocumentos.POCOs.Modelos
{
    /// <summary>
    /// Clase de convertir el texto.
    /// </summary>
    public class ModeloDetalleTransmicion
    {
        #region Propiedades
        public ModeloValor Fecha { get; private set; }
        public List<ModeloValor> DatosDetalle { get; private set; }
        #endregion

        #region Constructor
        public ModeloDetalleTransmicion(ConsultaDetalleTramiteTransmisionResponse response)
        {
            Fecha = new ModeloValor($"Ciudad de México a { DateTime.Now.Day } de {DateTime.Now.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-MX"))} de {DateTime.Now.Year}", esTitulo: true);
            DatosDetalle = new List<ModeloValor>
            {
                new ModeloValor($"C. {response.rep_nombre_completo.ToUpper()}", esTitulo: true),
                new ModeloValor($"Representante legal de {response.denominacion}", esTitulo: true),
                new ModeloValor(response.numero_sgar),
            };
        }
        #endregion
    }
}
