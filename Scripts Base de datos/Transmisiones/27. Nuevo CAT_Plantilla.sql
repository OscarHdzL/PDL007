

CREATE TABLE IF NOT EXISTS religiosos."CAT_Plantilla"
(
    i_id integer NOT NULL DEFAULT nextval('religiosos."CAT_Plantilla_i_id_seq"'::regclass),
    c_nombre character varying(70) COLLATE pg_catalog."default" NOT NULL,
    c_ruta character varying(500) COLLATE pg_catalog."default" NOT NULL,
    i_estatus integer,
    CONSTRAINT "PK_CAT_Plantilla" PRIMARY KEY (i_id)
);
