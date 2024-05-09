
CREATE OR REPLACE FUNCTION religiosos.sp_consulta_path_plantilla(
	p_id_plantilla integer)
    RETURNS TABLE(id_plantilla integer, ruta character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE

BEGIN

	RETURN QUERY
		SELECT plantilla.i_id as id_plantilla
			, plantilla.c_ruta as ruta
		FROM religiosos."CAT_Plantilla" plantilla
		WHERE plantilla.i_id = p_id_plantilla
			AND plantilla.i_estatus = 1;

END;
$BODY$;