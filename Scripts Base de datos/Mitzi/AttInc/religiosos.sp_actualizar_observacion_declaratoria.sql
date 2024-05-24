-- FUNCTION: religiosos.sp_actualizar_observacion_declaratoria(integer)

-- DROP FUNCTION IF EXISTS religiosos.sp_actualizar_observacion_declaratoria(integer);

CREATE OR REPLACE FUNCTION religiosos.sp_actualizar_observacion_declaratoria(
	p_id_declaratoria integer)
    RETURNS TABLE(id_generico integer, mensaje character varying, proceso_existoso boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$

DECLARE

BEGIN

	UPDATE religiosos."ASOC_DeclaratoriaDictaminador"
		SET b_read = true
	WHERE i_id_tbl_declaratoria = p_id_declaratoria;
	
	RETURN QUERY 
		SELECT p_id_declaratoria as id_generico,
		CAST('Actualizacion correcta'  as character varying) AS mensaje,
		(true) AS proceso_existoso;

END;
$BODY$;

ALTER FUNCTION religiosos.sp_actualizar_observacion_declaratoria(integer)
    OWNER TO postgres;
