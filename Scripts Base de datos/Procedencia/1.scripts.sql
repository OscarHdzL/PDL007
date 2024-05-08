
CREATE TABLE religiosos."CAT_Declaratoria_Cargo" (
	i_id serial4 NOT NULL,
	c_nombre varchar(50) NOT NULL,
	i_estatus int4 NOT NULL,
	CONSTRAINT "CAT_Declaratoria_Cargo_pkey" PRIMARY KEY (i_id)
);


-- religiosos."TBL_Declaratoria_Procedencia" definition

-- Drop table

-- DROP TABLE religiosos."TBL_Declaratoria_Procedencia";

CREATE TABLE religiosos."TBL_Declaratoria_Procedencia" (
	c_nombre_completo varchar(200) NOT NULL,
	c_denominacion_religiosa varchar(200) NOT NULL,
	c_numero_sgar varchar(20) NOT NULL,
	i_id_tbl_cargo int4 NOT NULL,
	i_id serial4 NOT NULL,
	b_declaratoria_verdad bit(1) NULL,
	c_folio varchar NULL,
	i_id_tbl_usuario int4 NOT NULL,
	i_id_tbl_estatus int4 NOT NULL,
	d_fecha_envio date NULL,
	d_fecha_autorizacion date NULL,
	b_activo bit(1) NOT NULL,
	c_observaciones varchar(250) NULL,
	b_genera_oficio bit(1) NULL,
	CONSTRAINT "PK_Declaratoria_Procedencia" PRIMARY KEY (i_id),
	CONSTRAINT "FK_Tbl_Cargo" FOREIGN KEY (i_id_tbl_cargo) REFERENCES religiosos."CAT_Declaratoria_Cargo"(i_id),
	CONSTRAINT "FK_id_Usuario" FOREIGN KEY (i_id_tbl_usuario) REFERENCES religiosos."TBL_Usuario"(i_id)
);
CREATE TABLE religiosos."TBL_Declaratoria_Avance_Registro" (
	i_id serial4 NOT NULL,
	i_id_tbl_declaratoria int4 NOT NULL,
	b_paso1 bit(1) NULL,
	b_paso2 bit(1) NULL,
	b_paso3 bit(1) NULL,
	b_paso4 bit(1) NULL,
	b_paso5 bit(1) NULL,
	CONSTRAINT "TBL_Declaratoria_Avance_Registro_pkey" PRIMARY KEY (i_id_tbl_declaratoria),
	CONSTRAINT "FK_Declaratoria_Autpriza" FOREIGN KEY (i_id_tbl_declaratoria) REFERENCES religiosos."TBL_Declaratoria_Procedencia"(i_id)
);

-- religiosos."TBL_Declaratoria_Domicilio_Adicionales" definition

-- Drop table

-- DROP TABLE religiosos."TBL_Declaratoria_Domicilio_Adicionales";

CREATE TABLE religiosos."TBL_Declaratoria_Domicilio_Adicionales" (
	i_id serial4 NOT NULL,
	c_lote varchar(20) NULL,
	c_manzana varchar(20) NULL,
	c_super_manzana varchar(20) NULL,
	c_delegacion varchar(20) NULL,
	c_sector varchar(20) NULL,
	c_zona varchar(20) NULL,
	c_region varchar(20) NULL,
	i_id_tbl_domicilio int4 NOT NULL,
	i_id_tbl_declaratoria int4 NOT NULL,
	CONSTRAINT "PK_Declaratoria_Domicilio_Extra" PRIMARY KEY (i_id),
	CONSTRAINT "FK_Domicilio_Declaratoria" FOREIGN KEY (i_id_tbl_declaratoria) REFERENCES religiosos."TBL_Declaratoria_Procedencia"(i_id),
	CONSTRAINT "FK_Domicilio_Declaratoria_Adic" FOREIGN KEY (i_id_tbl_domicilio) REFERENCES religiosos."TBL_Domicilio"(i_id)
);

-- religiosos."ASOC_Declaratoria_Personas_Autorizadas" definition

-- Drop table

-- DROP TABLE religiosos."ASOC_Declaratoria_Personas_Autorizadas";

CREATE TABLE religiosos."ASOC_Declaratoria_Personas_Autorizadas" (
	i_id serial4 NOT NULL,
	i_id_tbl_declaratoria int4 NOT NULL,
	c_nombre varchar(150) NOT NULL,
	c_correo_electronico varchar(70) NOT NULL,
	c_numero_tel int8 NULL,
	CONSTRAINT "FK_Declaratoria" FOREIGN KEY (i_id_tbl_declaratoria) REFERENCES religiosos."TBL_Declaratoria_Procedencia"(i_id)
);
-- religiosos."CAT_Declaratoria_Uso_Inmueble" definition

-- Drop table

-- DROP TABLE religiosos."CAT_Declaratoria_Uso_Inmueble";

CREATE TABLE religiosos."CAT_Declaratoria_Uso_Inmueble" (
	i_id serial4 NOT NULL,
	c_nombre varchar NOT NULL,
	i_estatus int4 NOT NULL,
	CONSTRAINT "PK_Declaratoria_Uso_Inmueble" PRIMARY KEY (i_id)
);

-- religiosos."TBL_Declaratoria_Regular_i_id_seq" definition

-- DROP SEQUENCE religiosos."TBL_Declaratoria_Regular_i_id_seq";

CREATE SEQUENCE religiosos."TBL_Declaratoria_Regular_i_id_seq"
	INCREMENT BY 1
	MINVALUE 1
	MAXVALUE 2147483647
	START 1
	CACHE 1
	NO CYCLE;

-- religiosos."CAT_Declaratoria_Unidad_Superficie" definition

-- Drop table

-- DROP TABLE religiosos."CAT_Declaratoria_Unidad_Superficie";

CREATE TABLE religiosos."CAT_Declaratoria_Unidad_Superficie" (
	i_id serial4 NOT NULL,
	c_nombre varchar NOT NULL,
	i_estatus int4 NOT NULL,
	CONSTRAINT "PK_Declaratoria_US" PRIMARY KEY (i_id)
);
-- religiosos."TBL_Declaratoria_Ubicacion" definition

-- Drop table

-- DROP TABLE religiosos."TBL_Declaratoria_Ubicacion";

CREATE TABLE religiosos."TBL_Declaratoria_Ubicacion" (
	i_id int4 DEFAULT nextval('religiosos."TBL_Declaratoria_Regular_i_id_seq"'::regclass) NOT NULL,
	i_id_tbl_declaratoria int4 NOT NULL,
	c_superficie varchar(20) NOT NULL,
	i_id_t_tipo_unidad int4 NOT NULL,
	c_ubicacion varchar(1000) NOT NULL,
	d_inicio_actividades date NULL,
	i_id_tipo_uso_inmueble int4 NOT NULL,
	i_norte float8 NULL,
	i_noreste float8 NULL,
	i_noroeste float8 NULL,
	i_sur float8 NULL,
	i_sureste float8 NULL,
	i_suroeste float8 NULL,
	i_oriente float8 NULL,
	i_poniente float8 NULL,
	c_otro varchar NULL,
	c_colindancia varchar NULL,
	c_descripcion_salida varchar NULL,
	b_regular bit(1) NULL,
	b_culto_publico bit(1) NULL,
	CONSTRAINT "PK_Declaratoria_Ubicacion" PRIMARY KEY (i_id),
	CONSTRAINT "FK_Declaratoria" FOREIGN KEY (i_id_tbl_declaratoria) REFERENCES religiosos."TBL_Declaratoria_Procedencia"(i_id),
	CONSTRAINT "FK_Unidad" FOREIGN KEY (i_id_t_tipo_unidad) REFERENCES religiosos."CAT_Declaratoria_Unidad_Superficie"(i_id),
	CONSTRAINT "FK_Uson_Inmueble" FOREIGN KEY (i_id_tipo_uso_inmueble) REFERENCES religiosos."CAT_Declaratoria_Uso_Inmueble"(i_id)
);

-- religiosos."ASOC_DeclaratoriaDictaminador_id_seq" definition

-- DROP SEQUENCE religiosos."ASOC_DeclaratoriaDictaminador_id_seq";

CREATE SEQUENCE religiosos."ASOC_DeclaratoriaDictaminador_id_seq"
	INCREMENT BY 1
	MINVALUE 1
	MAXVALUE 9223372036854775807
	START 1
	CACHE 1
	NO CYCLE;
-- religiosos."ASOC_DeclaratoriaDictaminador" definition

-- Drop table

-- DROP TABLE religiosos."ASOC_DeclaratoriaDictaminador";

CREATE TABLE religiosos."ASOC_DeclaratoriaDictaminador" (
	i_id int4 DEFAULT nextval('religiosos."ASOC_DeclaratoriaDictaminador_id_seq"'::regclass) NOT NULL,
	i_id_tbl_declaratoria int4 NULL,
	id_tbl_dictaminador int4 NULL,
	id_tbl_asignador int4 NULL,
	c_comentarios varchar NULL,
	CONSTRAINT "PK_Declaratoria_Dictaminador" PRIMARY KEY (i_id),
	CONSTRAINT "FK_Declaratoria_Procedencia_d" FOREIGN KEY (i_id) REFERENCES religiosos."TBL_Declaratoria_Procedencia"(i_id)
);

-- religiosos."ASOC_Declaratoria_Archivos_id_seq" definition

-- DROP SEQUENCE religiosos."ASOC_Declaratoria_Archivos_id_seq";

CREATE SEQUENCE religiosos."ASOC_Declaratoria_Archivos_id_seq"
	INCREMENT BY 1
	MINVALUE 1
	MAXVALUE 9223372036854775807
	START 1
	CACHE 1
	NO CYCLE;
-- religiosos."ASOC_Declaratoria_Archivos" definition

-- Drop table

-- DROP TABLE religiosos."ASOC_Declaratoria_Archivos";

CREATE TABLE religiosos."ASOC_Declaratoria_Archivos" (
	i_id int4 DEFAULT nextval('religiosos."ASOC_Declaratoria_Archivos_id_seq"'::regclass) NOT NULL,
	i_id_tbl_declaratoria int4 NULL,
	i_id_tbl_archivo int4 NULL,
	CONSTRAINT id_declaratoria_archivo PRIMARY KEY (i_id),
	CONSTRAINT id_tbl_archivo_fkey FOREIGN KEY (i_id_tbl_archivo) REFERENCES religiosos."TBL_Archivo"(i_id),
	CONSTRAINT id_tbl_declaratoria_fkey FOREIGN KEY (i_id_tbl_declaratoria) REFERENCES religiosos."TBL_Declaratoria_Procedencia"(i_id)
);

CREATE SEQUENCE religiosos.folio_sequence_declaratoria
	INCREMENT BY 1
	MINVALUE 1
	MAXVALUE 9223372036854775807
	START 1
	CACHE 1
	NO CYCLE;


INSERT INTO religiosos."CAT_Declaratoria_Cargo" (c_nombre,i_estatus) VALUES
	 ('Representante Legal',1),
	 ('Apoderado Legal',2);

update religiosos."CAT_Estatus" set nombre ='Declaratoria de Procedencia registrada',descripcion='Cuando el perfil público guarda una solicitud de declaratoria de procedencia en cualquier paso sin enviarla.',
b_activo =true,b_estatus_declaratoria ='1'
where i_id=39;

update religiosos."CAT_Estatus" set nombre ='En Espera Declaratoria de Procedencia',descripcion='cuando el perfil público envía la solicitud al perfil Asignador',
b_activo =true,b_estatus_declaratoria ='1'
where i_id=40;

INSERT INTO religiosos."CAT_Declaratoria_Uso_Inmueble" (c_nombre,i_estatus) VALUES
	 ('Templo',1),
	 ('Casa Pastoral',1),
	 ('Seminario',1),
	 ('Casa habitación',1),
	 ('Casa de formación',1),
	 ('Auditorio',1),
	 ('Campamentos',1),
	 ('Instituto bíblico',1),
	 ('Institución de salud',1),
	 ('Área para el resguardo gratuito de vehículos',1);
INSERT INTO religiosos."CAT_Declaratoria_Uso_Inmueble" (c_nombre,i_estatus) VALUES
	 ('Otro',1);
	 INSERT INTO religiosos."CAT_Declaratoria_Unidad_Superficie" (c_nombre,i_estatus) VALUES
	 ('Metros (m)',1),
	 ('Hectáreas (Ha)',1);
insert into religiosos."CAT_Archivo_Tramite" (i_id,nombre,descripcion,b_activo)
values(30,'Mapa de ubicación','Imagen de la ubicación del inmueble',true);
insert into religiosos."CAT_Archivo_Tramite" (i_id,nombre,descripcion,b_activo)
values(31,'Petición Declaratoria','Petición Declaratoria',true);
insert into religiosos."CAT_Archivo_Tramite" (i_id,nombre,descripcion,b_activo)
values(32,'Documento Anexo 1','Documento Anexo 1',true);
insert into religiosos."CAT_Archivo_Tramite" (i_id,nombre,descripcion,b_activo)
values(33,'Documento Anexo 2','Documento Anexo 2',true);
insert into religiosos."CAT_Archivo_Tramite" (i_id,nombre,descripcion,b_activo)
values(34,'Oficio Declaratoria','Oficio Declaratoria',true);
update religiosos."TBL_Persona" set c_correo ='c2VyZ2lvY0BzZWdvYi5nb2IubXg='
where i_id=3201;

	 
