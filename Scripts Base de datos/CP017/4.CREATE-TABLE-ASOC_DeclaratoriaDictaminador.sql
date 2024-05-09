CREATE TABLE IF NOT EXISTS religiosos."ASOC_DeclaratoriaDictaminador"
(
    i_id integer NOT NULL DEFAULT nextval('religiosos."ASOC_DeclaratoriaDictaminador_id_seq"'::regclass),
    i_id_tbl_declaratoria integer,
    id_tbl_dictaminador integer,
    id_tbl_asignador integer,
    c_comentarios character varying COLLATE pg_catalog."default",
    CONSTRAINT "PK_Declaratoria_Dictaminador" PRIMARY KEY (i_id),
    CONSTRAINT "FK_Declaratoria_Procedencia_d" FOREIGN KEY (i_id_tbl_declaratoria)
        REFERENCES religiosos."TBL_Declaratoria_Procedencia" (i_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS religiosos."ASOC_DeclaratoriaDictaminador"
    OWNER to tramitesdgardev_user;