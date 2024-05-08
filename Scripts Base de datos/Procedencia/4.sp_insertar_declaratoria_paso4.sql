-- DROP FUNCTION religiosos.sp_insertar_declaratoria_paso4(int4, varchar, int4, varchar, bit, varchar, int4, float8, float8, float8, float8, float8, float8, float8, float8, varchar, varchar, varchar, bit);

CREATE OR REPLACE FUNCTION religiosos.sp_insertar_declaratoria_paso4(p_id_declaratoria integer, p_superficie character varying, p_unidad integer, p_ubicacion character varying, p_culto_publico bit, p_inicio_actividades character varying, p_uso integer, p_norte double precision, p_noreste double precision, p_noroeste double precision, p_sur double precision, p_sureste double precision, p_suroeste double precision, p_oriente double precision, p_poniente double precision, p_otro character varying, p_colindancia character varying, p_descripcion_salida character varying, p_regular bit)
 RETURNS TABLE(id_generico integer, mensaje character varying, proceso_existoso boolean)
 LANGUAGE plpgsql
AS $function$
DECLARE

aux integer := (SELECT i_id 
				FROM religiosos."TBL_Declaratoria_Ubicacion" 
				WHERE i_id_tbl_declaratoria = p_id_declaratoria);

BEGIN

	IF (aux ISNULL) THEN
	
		INSERT INTO religiosos."TBL_Declaratoria_Ubicacion"(i_id_tbl_declaratoria, c_superficie, i_id_t_tipo_unidad, c_ubicacion, b_culto_publico, d_inicio_actividades, i_id_tipo_uso_inmueble, i_norte, i_noreste, i_noroeste, i_sur, i_sureste, i_suroeste, i_oriente, i_poniente, c_otro, c_colindancia, c_descripcion_salida, b_regular)
		VALUES (p_id_declaratoria, p_superficie, p_unidad, p_ubicacion, p_culto_publico, TO_DATE(p_inicio_actividades,'YYYY-MM-DD'), p_uso, p_norte, p_noreste, p_noroeste, p_sur, p_sureste, p_suroeste, p_oriente, p_poniente, p_otro, p_colindancia, p_descripcion_salida, p_regular);
		
		UPDATE religiosos."TBL_Declaratoria_Avance_Registro"
		SET b_paso4 = '1'
		WHERE i_id_tbl_declaratoria = p_id_declaratoria;
	
	ELSE
	
		UPDATE religiosos."TBL_Declaratoria_Ubicacion"
			SET c_superficie = p_superficie
			, i_id_t_tipo_unidad = p_unidad
			, c_ubicacion = p_ubicacion
			, b_culto_publico = p_culto_publico
			, d_inicio_actividades = TO_DATE(p_inicio_actividades,'YYYY-MM-DD')
			, i_id_tipo_uso_inmueble = p_uso
			, i_norte = p_norte
			, i_noreste = p_noreste
			, i_noroeste = p_noroeste
			, i_sur = p_sur
			, i_sureste = p_sureste
			, i_suroeste = p_suroeste
			, i_oriente = p_oriente
			, i_poniente = p_poniente
			, c_otro = p_otro
			, c_colindancia = p_colindancia
			, c_descripcion_salida = p_descripcion_salida
			, b_regular = p_regular
		WHERE i_id_tbl_declaratoria = p_id_declaratoria;
	
	END IF;

	RETURN QUERY 
	SELECT p_id_declaratoria as id_generico,
		CAST('La información se guardo correctamente.' as varchar) AS mensaje,
		(true) AS proceso_existoso;

END;
$function$
;