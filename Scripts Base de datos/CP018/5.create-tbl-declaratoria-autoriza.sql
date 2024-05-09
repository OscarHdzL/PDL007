-- Table: religiosos.TBL_Declaratoria_Autoriza

-- DROP TABLE IF EXISTS religiosos."TBL_Declaratoria_Autoriza";

CREATE TABLE IF NOT EXISTS religiosos."TBL_Declaratoria_Autoriza"
(
    i_id integer NOT NULL DEFAULT nextval('religiosos."TBL_Declaratoria_Autoriza_i_id_seq"'::regclass),
    i_id_tbl_declaratoria integer NOT NULL,
    d_fecha date NOT NULL,
    d_horario character varying COLLATE pg_catalog."default" NOT NULL,
    c_direccion character varying(500) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "PK_Autoriza" PRIMARY KEY (i_id),
    CONSTRAINT fk_autoriza_declaratoria FOREIGN KEY (i_id_tbl_declaratoria)
        REFERENCES religiosos."TBL_Declaratoria_Procedencia" (i_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS religiosos."TBL_Declaratoria_Autoriza"
    OWNER to tramitesdgardev_user;