-- DROP FUNCTION religiosos.sp_consulta_declaratoria_avance(int4);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_declaratoria_avance(p_id_declaratoria integer)
 RETURNS TABLE(i_id integer, id_declaratoria integer, paso1 bit, paso2 bit, paso3 bit, paso4 bit, paso5 bit)
 LANGUAGE plpgsql
AS $function$
DECLARE

BEGIN

	RETURN QUERY 
	SELECT avance.i_id
	, avance.i_id_tbl_declaratoria as id_declaratoria
	, avance.b_paso1 as paso1
	, avance.b_paso2 as paso2
	, avance.b_paso3 as paso3
	, avance.b_paso4 as paso4
	, avance.b_paso5 as paso5
	FROM religiosos."TBL_Declaratoria_Avance_Registro" avance
	WHERE avance.i_id_tbl_declaratoria = p_id_declaratoria;

END;
$function$
;