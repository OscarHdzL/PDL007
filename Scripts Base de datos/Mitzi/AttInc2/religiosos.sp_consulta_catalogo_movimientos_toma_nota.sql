-- FUNCTION: religiosos.sp_consulta_catalogo_movimientos_toma_nota(boolean)

-- DROP FUNCTION IF EXISTS religiosos.sp_consulta_catalogo_movimientos_toma_nota(boolean);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_catalogo_movimientos_toma_nota(
	p_activos boolean)
    RETURNS TABLE(s_id integer, s_movimiento character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$

BEGIN
RETURN QUERY
	SELECT
		i_id, nombre
	FROM 
		religiosos."CAT_TNotaMovimientos"
	WHERE 
		b_activo = p_activos OR p_activos IS NULL
	ORDER BY i_id;

END;
$BODY$;

ALTER FUNCTION religiosos.sp_consulta_catalogo_movimientos_toma_nota(boolean)
    OWNER TO tramitesdgardev_user;
