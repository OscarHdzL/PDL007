CREATE OR REPLACE FUNCTION religiosos.obtener_folio_transmisiones(
	)
    RETURNS TABLE(folio character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
siguiente_folio VARCHAR;							 
BEGIN

SELECT lpad(nextval('religiosos.folio_sequence_transmisiones')::text, 5, '0') || 
CAST( CONCAT('/',EXTRACT(YEAR FROM NOW())) AS CHARACTER VARYING) 
INTO siguiente_folio;

RETURN QUERY 
SELECT siguiente_folio;

END;
$BODY$;