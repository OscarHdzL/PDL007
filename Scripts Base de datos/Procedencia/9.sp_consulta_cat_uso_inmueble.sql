
CREATE OR REPLACE FUNCTION religiosos.sp_consulta_cat_uso_inmueble()
 RETURNS TABLE(i_id integer, c_nombre character varying, i_estatus integer)
 LANGUAGE plpgsql
AS $function$
BEGIN
RETURN QUERY

 SELECT inmueble.i_id
 		, inmueble.c_nombre
		, inmueble.i_estatus 
 FROM religiosos."CAT_Declaratoria_Uso_Inmueble" inmueble
 WHERE inmueble.i_estatus = 1;
 
END
$function$
;