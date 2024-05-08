-- DROP FUNCTION religiosos.sp_consulta_declaratoria_paso1(int4);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_declaratoria_paso1(p_id_declaratoria integer)
 RETURNS TABLE(id_declaratoria integer, nombre_completo character varying, denominacion_religiosa character varying, numero_sgar character varying, i_id_tbl_cargo integer, declaratoria_verdad bit)
 LANGUAGE plpgsql
AS $function$
DECLARE

BEGIN

	RETURN QUERY 
	SELECT declaratoria.i_id as id_declaratoria
		, declaratoria.c_nombre_completo as nombre_completo
		, declaratoria.c_denominacion_religiosa as denominacion_religiosa
		, declaratoria.c_numero_sgar as numero_sgar
		, declaratoria.i_id_tbl_cargo as i_id_tbl_cargo
		, declaratoria.b_declaratoria_verdad :: bit as declaratoria_verdad
	FROM religiosos."TBL_Declaratoria_Procedencia" declaratoria
	WHERE declaratoria.i_id = p_id_declaratoria;

END;
$function$
;