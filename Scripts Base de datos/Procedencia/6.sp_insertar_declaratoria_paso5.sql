-- DROP FUNCTION religiosos.sp_insertar_declaratoria_paso5(int4, bit);

CREATE OR REPLACE FUNCTION religiosos.sp_insertar_declaratoria_paso5(p_id_declaratoria integer, p_manifiesto_verdad bit)
 RETURNS TABLE(id_generico integer, mensaje character varying, proceso_existoso boolean)
 LANGUAGE plpgsql
AS $function$
DECLARE

generaOficio bit := (SELECT b_genera_oficio FROM religiosos."TBL_Declaratoria_Procedencia" where i_id= p_id_declaratoria);

BEGIN

	UPDATE religiosos."TBL_Declaratoria_Procedencia"
	SET b_declaratoria_verdad = p_manifiesto_verdad
	WHERE i_id = p_id_declaratoria;
	
	UPDATE religiosos."TBL_Declaratoria_Avance_Registro"
	SET b_paso5 = '1'
	WHERE i_id_tbl_declaratoria = p_id_declaratoria;
	
	IF(generaOficio = '1') THEN
		UPDATE religiosos."TBL_Declaratoria_Procedencia"
		SET i_id_tbl_estatus = 44
		WHERE i_id = p_id_declaratoria;
	END IF;

	RETURN QUERY 
	SELECT p_id_declaratoria as id_generico,
		CAST('La información se guardo correctamente.' as varchar) AS mensaje,
		(true) AS proceso_existoso;

END;
$function$
;