using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using Utilidades.GestionCreacionDocumentos.POCOs.Propiedades;

namespace Utilidades.GestionCreacionDocumentos.Implementar.Decoradores
{
    /// <summary>
    /// Clase encargada de creacion de una tabla en XML
    /// </summary>
    public class DecoradorTabla : BaseDecorador, IDisposable
    {
        #region Propiedades
        /// <summary>
        /// Propiedad para la creacion de una tabla
        /// </summary>
        public Table Tabla { get => ObtenerTabla(); }

        /// <summary>
        /// Collecion de datos
        /// </summary>
        private List<ConvertirPropiedades> CabezerasTabla { get; set; }

        /// <summary>
        /// Collecion de datos
        /// </summary>
        private List<List<ConvertirPropiedades>> DatosTabla { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor inicial
        /// </summary>
        /// <param name="propiedades"></param>
        public DecoradorTabla(PropiedadesDocumentoTabla propiedades)
            : base(propiedades)
        {
            CabezerasTabla = propiedades.CabezerasTabla;
            DatosTabla = propiedades.ContenidoTablas;
        }
        #endregion

        #region Métodos privados
        /// <summary>
        /// Método encargado de crear una tabla con los datos correspondiente
        /// </summary>
        /// <returns></returns>
        private Table ObtenerTabla()
        {
            var miTabla = new Table();

            try
            {
                miTabla.Append(ObtenerPropiedadesTabla());

                TableRow collecionCabezera = new();

                foreach (var item in CabezerasTabla)
                         collecionCabezera.Append(DecorarCelda(item.Texto, item.Negritas != null ? item.NegritasBase : null));

                miTabla.Append(collecionCabezera);

                foreach (var item in DatosTabla)
                {
                    List<TableCell> lstCeldas = new();
                    foreach (var celdaRapida in item)
                    {
                        TableCell celdaActual = DecorarCelda(celdaRapida.Texto, celdaRapida.Negritas != null ? celdaRapida.NegritasBase : null);
                        lstCeldas.Add(celdaActual);
                    }

                    miTabla.Append(new TableRow(lstCeldas));
                }

            }
            finally 
            {
                Dispose();
            }
           
            return miTabla;
        }

        /// <summary>
        /// Método encargado de liberar la memoria de recuros
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
