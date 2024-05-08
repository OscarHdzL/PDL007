-- DROP FUNCTION religiosos.sp_consulta_cat_estatus_declaratoria();

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_cat_estatus_declaratoria()
 RETURNS TABLE(i_id integer, c_nombre character varying, i_estatus integer)
 LANGUAGE plpgsql
AS $function$
BEGIN
RETURN QUERY
 
	SELECT estatus.i_id
		, estatus.nombre AS c_nombre
		, estatus.b_activo:: integer AS i_estatus
	FROM religiosos."CAT_Estatus" estatus
	WHERE estatus.b_estatus_declaratoria = '1';
 
END
$function$
;