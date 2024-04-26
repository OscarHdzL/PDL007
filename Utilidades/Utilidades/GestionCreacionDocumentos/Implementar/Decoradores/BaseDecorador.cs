using DocumentFormat.OpenXml.Wordprocessing;
using Utilidades.GestionCreacionDocumentos.POCOs;
using Utilidades.GestionCreacionDocumentos.POCOs.Propiedades;

namespace Utilidades.GestionCreacionDocumentos.Implementar.Decoradores
{
    /// <summary>
    /// Clase base del decorador de un parrafo mediante XML
    /// </summary>
    public abstract class BaseDecorador
    {
        #region Propiedades Basicas creacion etiquetas XML
        /// <summary>
        /// Propiedad para saber el tipo de letra
        /// </summary>
        private string TipoLetra { get; set; }

        /// <summary>
        /// Propiedad de la justificacion por renglon
        /// </summary>
        private JustificationValues JustificationGlobal { get; set; }

        /// <summary>
        /// Propiedad generica
        /// </summary>
        private RunProperties RunPropiedadesGlobal
            => new(new RunFonts()
            {
                Ascii = TipoLetra
                //Ascii = ""
            });
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor de iniciar el parrafo
        /// </summary>
        /// <param name="propiedad">Propiedades que tendran las configuraciones de los XML Globales</param>
        public BaseDecorador(PropiedadesDocumentoBase propiedad)
        {
            JustificationGlobal = propiedad.TipoJustificacion;
            TipoLetra = propiedad.TipoLetra;
        }
        #endregion

        #region Métodos publicos para los parrafos
        /// <summary>
        /// Métod encargado de iniciar un parrafo
        /// </summary>
        protected ParagraphProperties IniciarPropiedadesParrafo()
        {
            var PropiedadesParrafo = new ParagraphProperties();
            PropiedadesParrafo.Append(new ParagraphStyleId { Val = "Normal" });
            PropiedadesParrafo.Append(new Justification { Val = JustificationGlobal });
            PropiedadesParrafo.Append(new ParagraphMarkRunProperties());

            return PropiedadesParrafo;
        }

        /// <summary>
        /// Método de decorar una unica palabra
        /// </summary>
        /// <param name="texto">Texto en XML</param>
        /// <param name="negrita">Negritas en XML</param>
        /// <returns></returns>
        protected Run DecoradorUnicoRenglon(Text texto, Bold negrita)
        {
            Run RenderizadoRapido = new();
            var PropiedadRapida = RunPropiedadesGlobal;

            if (negrita != null)
                PropiedadRapida.AppendChild(negrita);

            RenderizadoRapido.Append(PropiedadRapida);
            RenderizadoRapido.Append(texto);

            return RenderizadoRapido;
        }

        /// <summary>
        /// Método de decorar una unica palabra
        /// </summary>
        /// <returns></returns>
        protected Run DecoradorUnicoSalto()
        {
            Run RenderizadoRapido = new();
            RenderizadoRapido.Append(new Break());

            return RenderizadoRapido;
        }

        /// <summary>
        /// Método de decorar una unica palabra
        /// </summary>
        /// <returns></returns>
        protected Paragraph SaltoDeLinea()
        {
            Paragraph parrafo = new();
            Run RenderizadoRapido = new();
            RenderizadoRapido.Append(new Break());
            parrafo.Append(RenderizadoRapido);
            return parrafo;
        }

        #endregion

        #region Métodos publicos para las tablas
        /// <summary>
        /// Método encargado de obtener los bordes de las tablas en XML
        /// </summary>
        /// <returns></returns>
        protected TableProperties ObtenerPropiedadesTabla()
        {
            return  new( new TableBorders(
                new TopBorder
                {
                    Val = new(BorderValues.Single),
                    Size = 12
                },
                new BottomBorder
                {
                    Val = new(BorderValues.Single),
                    Size = 12
                },
                new LeftBorder
                {
                    Val = new(BorderValues.Single),
                    Size = 12
                },
                new RightBorder
                {
                    Val = new(BorderValues.Single),
                    Size = 12
                },
                new InsideHorizontalBorder
                {
                    Val = new(BorderValues.Single),
                    Size = 12
                },
                new InsideVerticalBorder
                {
                    Val = new(BorderValues.Single),
                    Size = 12
                })
             );
        }

        /// <summary>
        /// Método encargado de crear las celdas de las tablas
        /// </summary>
        /// <param name="texto">Texto en XML</param>
        /// <param name="negrita">Negritas en XML</param>
        /// <returns></returns>
        protected TableCell DecorarCelda(Text texto, Bold negrita)
        {
            TableCell celdaRapida = new();
            Paragraph parrafoRapido = new();
            parrafoRapido.Append(IniciarPropiedadesParrafo());
            parrafoRapido.Append(DecoradorUnicoRenglon(texto, negrita));
            celdaRapida.Append(parrafoRapido);

            return celdaRapida;
        }
        #endregion
    }
}
