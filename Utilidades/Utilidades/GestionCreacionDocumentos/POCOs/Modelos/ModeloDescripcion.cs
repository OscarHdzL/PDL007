using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades.GestionCreacionDocumentos.POCOs.Modelos
{
    /// <summary>
    /// Clase de modelo de datos en duros
    /// </summary>
    public static class ModeloDescripcion
    {
        #region Prpopiedades
        /// <summary>
        /// Propiedad para obtener el msj de refiero dirección general
        /// </summary>
        public static ModeloValor DatosRefiero
            => new ("Me refiero a su escrito presentando a esta Dirección General, mediante el cual solicita autorización para transmitir los actos de culto religioso que a continuación se indican:", esTitulo: false);

        /// <summary>
        /// Propiedad para obtener la descripcion de las asociones religiosas
        /// </summary>
        public static ModeloValor DatosDescripcionAsociacionReligiosa
            => new ("En esta tesitura y toda vez que como asociación religiosa debidamente registrada, tiene derecho a transmitir o difundir actos de culto religioso a través de medios masivos de " +
                   "comunicación no impresos, en tanto cumplan con las normas aplicables a la materia con fundamento en los artículos 9, fracción III, 21, párrafo segundo, de la Ley de Asociaciones Religiosas y " +
                   "Culto Público; 30 y 31 del Reglamento de dicha Ley; 86, fracción IX, del Reglamento Interior de la Secretaría de Gobernación, esta Dirección General de Asuntos Religiosos, a través de la Dirección de Normatividad, atendiendo " +
                   "las medidas de prevención emitidas por las autoridades del sector salud, derivado de la contingencia provocada por COVID-19 (coronavirus), y con la intención de evitar la aglomeración de personas en cualquier tipo de espacio, determina autorizar " +
                   "las transmisiones que han sido enunciadas.", esTitulo: false);

        /// <summary>
        /// Propiedad para obtener descripcion de los articulos.
        /// </summary>
        public static ModeloValor DatosDescripcionArticulos
            => new ("Ahora bien, resulta indispensable puntualizar que de conformidad con los artículos 21, párrafo segundo, de la Ley de Asociaciones Religiosas y Culto Público y su diverso 30, párrafo segundo de su Reglamente; la transmisión o difusión de actos de culto" +
                  " religioso en ningún caso podrán difundirse en los tiempos de radio y televisión destinados al Estado.", esTitulo: false);

        /// <summary>
        /// Propiedad para obtener descripcion de los Fraccion.
        /// </summary>
        public static ModeloValor DatosDescripcionFraccion
            => new ("Lo anterior con fundamento en los artículos 2, apartado B, fracción XI, 9 y 86, fracción IX, todos del Reglamento Interior de la Secretaría de Gobernación; y en el Acuerdo por el que se da a conocer al público en general el medio de difusión de los trámites y servicios " +
                  " que se reactivan en la Secretaría de Gobernación a través de medios electrónicos, con motivo de la emergencia sanitaria generada por el coronavirus SARS- CoV2(COVID-19), publicado en el Diario Oficial de la Federación el día 26 de agosto de 2020.", esTitulo: false);

        /// <summary>
        /// Propiedad para obtener descripcion de los Considereaciones.
        /// </summary>
        public static ModeloValor DatosDescripcionConsidereaciones
            => new ("Hago propia la ocasión para reiterarle la más distinguida de mis consideraciones.", esTitulo: false);

        /// <summary>
        /// Propiedad para obtener descripcion de los atentamente.
        /// </summary>
        public static ModeloValor DatosDescripcionAtentamente
            => new ("Atentamente", esTitulo: true);


        /// <summary>
        /// Propiedad para obtener descripcion de los puesto.
        /// </summary>
        public static ModeloValor AgregarPuesto(string puesto)
            => new($"{puesto}", esTitulo: true);


        /// <summary>
        /// Propiedad para obtener descripcion de los CopiaCopiaPara.
        /// </summary>
        public static ModeloValor AgregarCopiaCopiaPara(string paratal)
            => new($"C.c.p. {paratal}", esTitulo: false);

        /// <summary>
        /// Propiedad para obtener descripcion de los Salto.
        /// </summary>
        public static ModeloValor Salto
           => new(" ", esTitulo: false);

        /// <summary>
        /// Propiedad para texto del encabezado del oficio.
        /// </summary>
        /// <returns></returns>
        public static List<ModeloValor> DatosEncabezadoDerecho
            => new ()
            {
                new ModeloValor("Secretaría de Gobernación", esTitulo: true),
                new ModeloValor("Subsecretaría de Desarrollo Democrático, Participación Social y Asuntos Religiosos", esTitulo: true),
                new ModeloValor("Unidad de Asuntos Religiosos, Prevensión y la Reconstrucción del tejido Social", esTitulo: true),
                new ModeloValor("Dirección General de Asuntos Religiosos", esTitulo: true),
                new ModeloValor("Dirección General Adjunta de Registro, Certificación y Normatividad de las Asociaciones Religiosas", esTitulo: true),
            };
        #endregion
    }
}
