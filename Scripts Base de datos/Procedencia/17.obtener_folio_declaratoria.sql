-- DROP FUNCTION religiosos.obtener_folio_declaratoria();

CREATE OR REPLACE FUNCTION religiosos.obtener_folio_declaratoria()
 RETURNS TABLE(folio character varying)
 LANGUAGE plpgsql
AS $function$
DECLARE
siguiente_folio VARCHAR;							 
BEGIN

SELECT lpad(nextval('religiosos.folio_sequence_declaratoria')::text, 5, '0') || 
CAST( CONCAT('/',EXTRACT(YEAR FROM NOW())) AS CHARACTER VARYING) 
INTO siguiente_folio;

RETURN QUERY 
SELECT siguiente_folio;

END;
$function$
;
