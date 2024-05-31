-- FUNCTION: religiosos.sp_consulta_detalle_movimientos_toma_nota_new(integer)

-- DROP FUNCTION IF EXISTS religiosos.sp_consulta_detalle_movimientos_toma_nota_new(integer);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_detalle_movimientos_toma_nota_new(
	i_id_c integer)
    RETURNS TABLE(s_id integer, s_cat_mov integer, s_cat_tnota integer, s_movimiento character varying, s_activo boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$

BEGIN
RETURN QUERY
	SELECT
		astnm.i_id, astnm.i_id_cat_tnotamovimientos,astnm.i_id_tbl_tnota, ctnm.nombre
		, case when astnm.i_id > 0 then true else false end as s_activo
	FROM 
		religiosos."CAT_TNotaMovimientos" AS ctnm 
	LEFT JOIN religiosos."ASOC_TNotaMovimientos" AS astnm
		ON astnm.i_id_tbl_tnota = i_id_c  AND ctnm.i_id = astnm.i_id_cat_tnotamovimientos
	WHERE 
		ctnm.b_activo= true 
	ORDER BY ctnm.i_id;

END;
$BODY$;

ALTER FUNCTION religiosos.sp_consulta_detalle_movimientos_toma_nota_new(integer)
    OWNER TO tramitesdgardev_user;
