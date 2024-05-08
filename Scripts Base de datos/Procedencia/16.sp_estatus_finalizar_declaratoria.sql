-- DROP FUNCTION religiosos.sp_estatus_finalizar_declaratoria(int4);

CREATE OR REPLACE FUNCTION religiosos.sp_estatus_finalizar_declaratoria(p_id_declaratoria integer)
 RETURNS TABLE(id_generico integer, mensaje character varying, proceso_existoso boolean)
 LANGUAGE plpgsql
AS $function$
DECLARE

	observacionesEspera integer:= (SELECT i_id FROM religiosos."CAT_Estatus" WHERE nombre = 'Observaciones en Declaratoria de Procedencia - En espera' LIMIT 1);
	observacionesAtendidas integer:= (SELECT i_id FROM religiosos."CAT_Estatus" WHERE nombre = 'Observación atendida' AND b_estatus_declaratoria = '1' LIMIT 1);
	estatusActual integer:= (SELECT i_id_tbl_estatus FROM religiosos."TBL_Declaratoria_Procedencia" WHERE i_id = p_id_declaratoria LIMIT 1);

	aux_estatus integer:= (SELECT i_id FROM religiosos."CAT_Estatus" WHERE nombre = 'En Espera Declaratoria de Procedencia' LIMIT 1);
	
	id_usuario integer := (SELECT i_id FROM religiosos."TBL_Usuario" WHERE i_id_tbl_perfil = 11 LIMIT 1);
  	
	c_consecutivo integer := (SELECT SUBSTRING(c_folio, 0, 5)
								FROM religiosos."TBL_Declaratoria_Procedencia"
								WHERE (c_folio IS NOT NULL OR c_folio<>'')
								ORDER BY c_folio DESC
								LIMIT 1);
	numero character varying := (c_consecutivo + 1) :: character varying;
	
	folio_generado  character varying := '';

BEGIN

	IF(c_consecutivo isnull) THEN
		numero='0001';
	ELSEIF(c_consecutivo <= 0008) THEN
		numero = Concat('000', numero);
	ELSEIF(c_consecutivo >= 0009 AND c_consecutivo <= 0098) THEN 
		numero = Concat('00', numero);
	ELSEIF(c_consecutivo >= 0099 AND c_consecutivo <= 0998 )THEN
		numero = Concat('0', numero );
	ELSEIF(c_consecutivo >= 0999 )THEN
		numero = numero;
	END IF;
       	
	folio_generado =  concat(numero,'/',extract(year from CURRENT_DATE));
	
	IF(estatusActual = observacionesEspera) THEN
		UPDATE religiosos."TBL_Declaratoria_Procedencia"
			SET i_id_tbl_estatus = observacionesAtendidas
		WHERE i_id = p_id_declaratoria;
	
	ELSE
		UPDATE religiosos."TBL_Declaratoria_Procedencia"
			SET i_id_tbl_estatus = aux_estatus
				, d_fecha_envio = current_date
				, c_folio = (SELECT folio FROM religiosos.obtener_folio_declaratoria())--folio_generado
		WHERE i_id = p_id_declaratoria;
	END IF;
	
	RETURN QUERY 
	SELECT id_usuario as id_generico,
		CAST('La información se guardo correctamente.' as varchar) AS mensaje,
		(true) AS proceso_existoso;

END;
$function$
;