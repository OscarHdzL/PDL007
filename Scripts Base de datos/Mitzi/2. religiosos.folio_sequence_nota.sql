-- SEQUENCE: religiosos.folio_sequence_nota

-- DROP SEQUENCE IF EXISTS religiosos.folio_sequence_nota;

CREATE SEQUENCE IF NOT EXISTS religiosos.folio_sequence_nota
    INCREMENT 1
    START 1
    MINVALUE 1
    MAXVALUE 9223372036854775807
    CACHE 1;

ALTER SEQUENCE religiosos.folio_sequence_nota
    OWNER TO tramitesdgardev_user;