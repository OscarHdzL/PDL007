-- SEQUENCE: religiosos.TBL_Declaratoria_Autoriza_i_id_seq

-- DROP SEQUENCE IF EXISTS religiosos."TBL_Declaratoria_Autoriza_i_id_seq";

CREATE SEQUENCE IF NOT EXISTS religiosos."TBL_Declaratoria_Autoriza_i_id_seq"
    INCREMENT 1
    START 1
    MINVALUE 1
    MAXVALUE 2147483647
    CACHE 1;
    --OWNED BY "TBL_Declaratoria_Autoriza".i_id;

ALTER SEQUENCE religiosos."TBL_Declaratoria_Autoriza_i_id_seq"
    OWNER TO tramitesdgardev_user;