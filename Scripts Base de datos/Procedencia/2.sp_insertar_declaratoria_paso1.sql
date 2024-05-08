-- DROP FUNCTION religiosos.sp_insertar_declaratoria_paso1(int4, varchar, varchar, varchar, int4, int4);

CREATE OR REPLACE FUNCTION religiosos.sp_insertar_declaratoria_paso1(p_id_declaratoria integer, p_nombre_completo character varying, p_denominacion_religiosa character varying, p_numero_sgar character varying, p_i_id_tbl_cargo integer, p_i_id_tbl_usuario integer)
 RETURNS TABLE(id_generico integer, mensaje character varying, proceso_existoso boolean)
 LANGUAGE plpgsql
AS $function$
DECLARE
aux_estatus integer:= (SELECT i_id FROM religiosos."CAT_Estatus" WHERE nombre = 'Declaratoria de Procedencia registrada' LIMIT 1);

id_tramite_declaratoria integer;

BEGIN

	IF(p_id_declaratoria = 0) THEN
	
		INSERT INTO religiosos."TBL_Declaratoria_Procedencia"(c_nombre_completo, c_denominacion_religiosa, c_numero_sgar, i_id_tbl_cargo, i_id_tbl_usuario, b_declaratoria_verdad, i_id_tbl_estatus, b_activo)
		VALUES (p_nombre_completo, p_denominacion_religiosa, p_numero_sgar, p_i_id_tbl_cargo, p_i_id_tbl_usuario, '0', aux_estatus, '1')
		RETURNING i_id INTO id_tramite_declaratoria;
			
		INSERT INTO religiosos."TBL_Declaratoria_Avance_Registro"(i_id_tbl_declaratoria, b_paso1, b_paso2, b_paso3, b_paso4, b_paso5)
		VALUES (id_tramite_declaratoria, '1', '0', '0', '0', '0');
	
	ELSE
	
		id_tramite_declaratoria = p_id_declaratoria;
	
		UPDATE religiosos."TBL_Declaratoria_Procedencia"
		SET c_nombre_completo = p_nombre_completo
			, c_denominacion_religiosa = p_denominacion_religiosa
			, c_numero_sgar = p_numero_sgar
			, i_id_tbl_cargo = p_i_id_tbl_cargo
		WHERE i_id = p_id_declaratoria;
		
	END IF;

	RETURN QUERY 
		SELECT id_tramite_declaratoria as id_generico,
			CAST('La información se guardo correctamente.' as varchar) AS mensaje,
			(true) AS proceso_existoso;

END;
$function$
;