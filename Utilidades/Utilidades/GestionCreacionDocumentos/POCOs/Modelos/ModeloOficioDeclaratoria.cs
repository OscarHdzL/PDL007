using System.Collections.Generic;

namespace Utilidades.GestionCreacionDocumentos.POCOs.Modelos
{
    public static class ModeloOficioDeclaratoria
    {
        public static ModeloValor contenido_parrafo_1(string domicilio)
            => new ("En contestación a su escrito recibido el pasado 01 de agosto, mediante el cual expone la necesidad que tiene su representada de adquirir en propiedad el inmueble ubicado en " + 
            $"{domicilio}" + ", para el cumplimiento de su objeto; me permito comunicarle que se ha acordado favorablemente la expedición de la correspondiente declaratoria de procedencia.", esTitulo: false);
        
        public static ModeloValor contenido_parrafo_2
            => new ("Asimismo, le informo que anexo al presente se acompaña la declaratoria de procedencia respecto del inmueble citado.", esTitulo: false);
        
        public static ModeloValor contenido_parrafo_3
            => new ("No omito señalar, que la citada declaratoria de procedencia se expide en consideración a su manifestación, bajo protesta de decir verdad, en el sentido de que dicho inmueble le resulta indispensable para el cumplimiento del " + 
                    "objeto propuesto por parte de la asociación religiosa, así como también de que no existe ningún tipo de conflicto, en cuanto al uso y posesión del inmueble.", esTitulo: false);
        
        public static ModeloValor contenido_parrafo_4
            => new ("Finalmente, le reitero que la asociación religiosa se encuentra obligada a dar cumplimiento a los resolutivos de la citada declaratoria de procedencia. " + 
                    "Además, de que dicho documento no exime a la asociación religiosa que representa, de cumplir con la normatividad federal, estatal y municipal conducente, en especial, en materia de traslado de dominio, construcción, " +
                    "uso del suelo, licencias de funcionamiento, protección civil, desarrollo urbano, decibeles permitidos, y demás disposiciones aplicables", esTitulo: false);
        
        public static ModeloValor articulo_seccion_1
            => new( "Lo anterior, con fundamento en los artículos 1, 3, 5, 24, 27, fracción II, 130 y Decimoséptimo Transitorio de la Constitución Política de los Estados Unidos Mexicanos; " + 
                    "1, 14, 16 y 27, fracciones I, IV, VII y XXIV, de la Ley Orgánica de la Administración Pública Federal; 1, 2, 3, 8, 12, 13, 14 y 16 de la Ley Federal de Procedimiento Administrativo; " + 
                    "1, 2, 4, 5, 8, 9, 15, 16, 17, 18, 25, 26 y 29 de la Ley de Asociaciones Religiosas y Culto Público; 1, 3, 4, 16, 20 y 24 del Reglamento de la Ley de Asociaciones Religiosas y Culto Público; " + 
                    "1, 2 apartado A), fracción III, apartado B), fracciones XI y XXXVII, 9, 10, 11, 86, fracciones I y VI, 87, fracción II, del Reglamento Interior de la Secretaría de Gobernación.", esTitulo: false);
        public static ModeloValor articulo_seccion_2
            => new("Finalmente, me permito comunicarle que los trámites que se realizan en esta Dirección General no causan ningún tipo de costo, por lo que el presente documento se expide de manera gratuita.", esTitulo: false);
        public static ModeloValor articulo_seccion_3
            => new("Sin más por el momento, expreso a usted la seguridad de mi consideración distinguida.", esTitulo: false);
        
        public static ModeloValor contenido2
            => new("Declaratoria de Procedencia que emite la Dirección General de Asuntos Religiosos, dependiente de la Subsecretaría de Desarrollo Democrático, Participación Social y Asuntos Religiosos, de la Secretaría de Gobernación, "+ 
                   "por conducto del Director General Adjunto de Registro, Certificación y Normatividad de las Asociaciones Religiosas, Lic. Jorge Lee Galindo, con fundamento en los artículos 1, 3, 5, 24, 27, fracción II, 130 y Decimoséptimo Transitorio de la Constitución Política de los Estados Unidos Mexicanos; "+ 
                   "1, 14, 16 y 27, fracciones I, IV, VII y XXIV, de la Ley Orgánica de la Administración Pública Federal; 1, 2, 3, 8, 12, 13, 14 y 16 de la Ley Federal de Procedimiento Administrativo; 1, 2, 4, 5, 8, 9, 15, 16, 17, 18, 25, 26 y 29 de la Ley de Asociaciones Religiosas y Culto Público; "+ 
                   "1, 3, 4, 16, 20 y 24 del Reglamento de la Ley de Asociaciones Religiosas y Culto Público; 1, 2 apartado A), fracción III, apartado B), fracciones XI y XXXVII, 9, 10, 11, 86, fracciones I y VI, 87, fracción II, del Reglamento Interior de la Secretaría de Gobernación, y, ", esTitulo: false);
        
        public static ModeloValor tituloConsiderando
            => new("C O N S I D E R A N D O", esTitulo: true);
        
        public static ModeloValor contenidoConsideraciones(string numSgar, string denominacion)
            => new("<B>PRIMERO.-</B> Que la Secretaría de Gobernación es competente para resolver sobre el carácter indispensable de los bienes inmuebles que pretendan adquirir las asociaciones religiosas." + 
                   "SEGUNDO.- Que la solicitante de la presente declaratoria, es una asociación religiosa legalmente constituida para lo cual cuenta con el Registro Constitutivo número "+ $"{numSgar}" +", denominada " + $"{denominacion}" + ";" +
                   "TERCERO.- Que el representante legal de la citada asociación religiosa ha manifestado, bajo protesta de decir verdad, que para el cumplimiento de los fines propuestos en su objeto, le es indispensable adquirir en propiedad el bien inmueble cuyo nombre, ubicación y superficie, "+ 
                   "se especifican en el resolutivo primero de este documento, del cual dicho representante, ha expresado que no se tiene ningún tipo de conflicto en cuanto a su uso y posesión;" + 
                   "CUARTO.- Que tomando en cuenta que las manifestaciones realizadas por el representante de la asociación religiosa, se realizaron con el pleno conocimiento de que la simple promesa de decir verdad y de cumplir las obligaciones que se contraen, sujeta al que la hace, en caso de que faltare a ella, "+ 
                   "a las sanciones que por tal motivo establecen las leyes; la Secretaría de Gobernación, a través de la Dirección General de Asuntos Religiosos, dependiente de la Subsecretaría de Desarrollo Democrático, Participación Social y Asuntos Religiosos;", esTitulo: false);
        
        public static ModeloValor tituloResuelve
            => new("R E S U E L V E", esTitulo: true);
        
        public static ModeloValor contenidoResuelveUno(string denominacion)
            => new("PRIMERO.- Se expide la presente declaratoria de procedencia a la asociación religiosa "+  $"{denominacion}" + "; para que con base en la misma, realice los trámites relativos al traslado de dominio del bien inmueble que enseguida se detalla, de acuerdo con la información proporcionada bajo la más estricta y absoluta responsabilidad de la propia solicitante:", esTitulo: false);
        
        public static ModeloValor contenidoResuelveDos
            => new("SEGUNDO.- La asociación religiosa recibe la declaratoria de procedencia en el entendido de que la misma cuenta con el consentimiento del legítimo propietario del inmueble para la tramitación del presente documento. "+ 
                   "Destacando, que la declaratoria de procedencia no obliga al titular de los derechos del predio a realizar ninguna operación en favor de la asociación religiosa en mención; por lo tanto, la presente autorización quedará extinguida si el primero determinara no transmitirlos en favor de la asociación religiosa, "+ 
                   "así como en el caso de que la asociación religiosa no haga uso del citado documento. Puntualizando, que el presente documento no prejuzga sobre los derechos de propiedad del inmueble y deja a salvo los derechos de terceros, para hacerlos valer ante en la vía y forma que a su derecho convenga." + 
                   "TERCERO.- La autorización contenida en la presente declaratoria se basa en las manifestaciones expresadas por la solicitante, por lo que en caso de que dichas manifestaciones carezcan de veracidad, la presente autorización quedará extinguida, "+ 
                   "en especial si el inmueble descrito en el resolutivo primero se encontrare dentro del supuesto de ser un inmueble nacionalizado en los términos de la fracción II del artículo 27 de la Constitución Política de los Estados Unidos Mexicanos, "+ 
                   "antes de sus reformas del 28 de enero de 1992, actualmente Decimoséptimo Transitorio de la misma, así como del artículo Cuarto Transitorio de la Ley de Asociaciones Religiosas y Culto Público." + 
                   "CUARTO.- La asociación religiosa deberá realizar las gestiones necesarias para regularizar el inmueble de mérito en su favor, aclarando que la presente resolución únicamente tiene efectos declarativos y no constitutivos de un derecho de propiedad o titularidad de un inmueble, "+ 
                   "por lo que queda estrictamente bajo la responsabilidad de la asociación religiosa perfeccionar el acto jurídico para la adquisición del bien que nos ocupa. En el supuesto que dicho bien se encuentre sujeto al régimen de propiedad ejidal o comunal, "+ 
                   "deberá realizar los trámites correspondientes en términos de la Ley Agraria, para que las autoridades agrarias y los órganos de autoridad interno del ejido resuelvan sobre la procedencia de formalizar la adquisición de dicho inmueble a favor de la asociación religiosa, "+ 
                   "así como que el uso manifestado no contraviene la delimitación y destino de las tierras del núcleo de población." + 
                   "QUINTO.- En cumplimiento a lo dispuesto en la Ley de Asociaciones Religiosas y Culto Público y el Reglamento de la misma, la asociación religiosa no deberá utilizar el inmueble motivo de la presente declaratoria para actividades que persigan fines de lucro o preponderantemente económicos." + 
                   "SEXTO.- El destino del inmueble detallado, será única y exclusivamente el señalado en el resolutivo primero del presente documento, el cual estará condicionado a que la asociación religiosa cumpla con las disposiciones locales en materia de desarrollo urbano, construcción y uso de suelo, "+ 
                   "además, de que dicho documento no exime a la asociación religiosa que representa, de cumplir con la normatividad federal, estatal y municipal conducente. Destacando, que los representantes, apoderados legales de la asociación religiosa o el notario público que pretendan realizar operación alguna que modifique la ubicación, "+ 
                   "medidas, superficie, destino o propiedad del inmueble descrito en la presente declaratoria de procedencia, deberán, previamente, contar con la autorización de esta dependencia del Ejecutivo Federal." + 
                   "SÉPTIMO.- De igual manera, el fedatario público deberá constatar que los datos proporcionados por la asociación religiosa respecto del inmueble objeto de la presente declaratoria de procedencia, son compatibles con la normatividad federal, estatal y municipal." + 
                   "OCTAVO.- La presente declaratoria no exime a la autoridad o fedatario público que intervenga en el acto jurídico mediante el cual se formalice la adquisición del inmueble descrito en el resolutivo primero, de su obligación de verificar de que se cumplan con todas las demás disposiciones aplicables y que no se lesionen derechos de terceros. "+ 
                   "Asimismo, deberá hacer constar que tuvo a la vista el presente documento, consignando en el título respectivo el destino autorizado del bien por adquirir, así como los principales datos de la declaratoria, como son: número de oficio, fecha, referencia y folio." + 
                   "NOVENO.- En los casos de los resolutivos cuarto y octavo, la asociación religiosa deberá enviar a la Secretaría de Gobernación copia certificada del primer testimonio, debidamente inscrito en el Registro Público de la Propiedad que por ubicación del inmueble corresponda, para efecto de su toma de nota en el Registro de Bienes Inmuebles a cargo de esta Dirección General." + 
                   "La presente Declaratoria de Procedencia se expide de manera gratuita en la Ciudad de México, a los dieciocho días del mes de agosto de dos mil veintitrés.", esTitulo: false);
        
        public static ModeloValor atentamente
            => new ("A T E N T A M E N T E", esTitulo: true);
        
        public static ModeloValor nombreAtentamente(string nombre)
            => new($"{nombre}", esTitulo: true);
        public static ModeloValor puestoAtentamente(string puesto)
            => new($"{puesto}", esTitulo: true);
        public static ModeloValor concopia(string para)
            => new($"C.c.p.- Mtro. Jorge Eduardo Basaldúa Silva.- Director General de Asuntos Religiosos.-Para su conocimiento", esTitulo: false);
        public static ModeloValor iniciales
            => new("SAS/SMA", esTitulo: false);
        
        public static List<ModeloValor> DatosEncabezadoDerecho
            => new ()
            {
                new ModeloValor("Secretaría de Gobernación", esTitulo: true),
                new ModeloValor("Subsecretaría de Desarrollo Democrático, Participación Social y Asuntos Religiosos", esTitulo: true),
                new ModeloValor("Unidad de Asuntos Religiosos, Prevensión y la Reconstrucción del tejido Social", esTitulo: true),
                new ModeloValor("Dirección General de Asuntos Religiosos", esTitulo: true),
                new ModeloValor("Dirección General Adjunta de Registro, Certificación y Normatividad de las Asociaciones Religiosas", esTitulo: true),
            };
    }
}
