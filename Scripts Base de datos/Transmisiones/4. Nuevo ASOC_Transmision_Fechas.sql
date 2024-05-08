
CREATE TABLE IF NOT EXISTS religiosos."ASOC_Transmision_Fechas"
(
    i_id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    i_id_tbl_transmision integer,
    i_id_tbl_fechas integer,
    CONSTRAINT "ASOC_Transmision_Fechas_pkey" PRIMARY KEY (i_id),
    CONSTRAINT "ASOC_Transmision_Fechas_i_id_tbl_fechas_fkey" FOREIGN KEY (i_id_tbl_fechas)
        REFERENCES religiosos."TBL_Fechas" (i_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID,
    CONSTRAINT "ASOC_Transmision_Fechas_i_id_tbl_transmision_fkey" FOREIGN KEY (i_id_tbl_transmision)
        REFERENCES religiosos."TBL_Transmision" (i_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)
