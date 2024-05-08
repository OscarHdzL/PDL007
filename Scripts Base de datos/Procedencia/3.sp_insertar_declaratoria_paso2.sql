-- DROP FUNCTION religiosos.sp_insertar_declaratoria_paso2(int4, varchar, varchar, varchar, int4, int4, varchar, varchar, varchar, varchar, varchar, varchar, varchar, text);

CREATE OR REPLACE FUNCTION religiosos.sp_insertar_declaratoria_paso2(p_id_declaratoria integer, p_calle character varying, p_numeroe character varying, p_numeroi character varying, p_i_id_tbl_colonia integer, p_tipo_domicilio integer, p_lote character varying, p_manzana character varying, p_super_manzana character varying, p_delegacion character varying, p_sector character varying, p_zona character varying, p_region character varying, p_personas_aut text)
 RETURNS TABLE(id_generico integer, mensaje character varying, proceso_existoso boolean)
 LANGUAGE plpgsql
AS $function$
DECLARE

aux integer := (SELECT dom.i_id
				FROM religiosos."TBL_Declaratoria_Domicilio_Adicionales" ad
				INNER JOIN religiosos."TBL_Domicilio" dom 
					ON dom.i_id = ad.i_id_tbl_domicilio
				WHERE i_id_tbl_declaratoria = p_id_declaratoria
					AND dom.i_id_tbl_tdomicilio = p_tipo_domicilio);
					
tbl_domicilio_id integer;

BEGIN

	IF (aux ISNULL) THEN
	
		INSERT INTO religiosos."TBL_Domicilio"(c_calle, c_numeroe, c_numeroi, i_id_tbl_colonia, i_id_tbl_tdomicilio)
		VALUES (p_calle, p_numeroe, p_numeroi, p_i_id_tbl_colonia, p_tipo_domicilio)
		RETURNING i_id INTO tbl_domicilio_id;

		--INSERT INTO religiosos."TBL_Declaratoria_Domicilios" (i_id_tbl_domicilio, i_id_tbl_declaratoria)
		--VALUES (tbl_domicilio_id, p_id_declaratoria);

		INSERT INTO religiosos."TBL_Declaratoria_Domicilio_Adicionales"(c_lote, c_manzana, c_super_manzana, c_delegacion, c_sector, c_zona, c_region, i_id_tbl_domicilio, i_id_tbl_declaratoria)
		VALUES (p_lote, p_manzana, p_super_manzana, p_delegacion, p_sector, p_zona, p_region, tbl_domicilio_id, p_id_declaratoria);
		
		IF(p_tipo_domicilio = 2) THEN
			UPDATE religiosos."TBL_Declaratoria_Avance_Registro"
			SET b_paso2 = '1'
			WHERE i_id_tbl_declaratoria = p_id_declaratoria;
		ELSEIF (p_tipo_domicilio = 3) THEN
			UPDATE religiosos."TBL_Declaratoria_Avance_Registro"
			SET b_paso3 = '1'
			WHERE i_id_tbl_declaratoria = p_id_declaratoria;
		END IF;
		
		IF (p_personas_aut <> '[]') THEN
			INSERT INTO religiosos."ASOC_Declaratoria_Personas_Autorizadas"(i_id_tbl_declaratoria, c_nombre, c_correo_electronico, c_numero_tel)
			SELECT
			(t ->>'i_id_tbl_declaratoria')::integer i_id_tbl_declaratoria,
			t ->>'c_nombre' c_nombre,
			t ->>'c_correo_electronico' c_correo_electronico,
			(t ->>'c_numero_tel')::bigint c_numero_tel
			FROM jsonb_array_elements(to_jsonb(p_personas_aut::json)) t;
		END IF;
		
	ELSE
		
		tbl_domicilio_id = aux;
		
		UPDATE religiosos."TBL_Domicilio"
		SET c_calle = p_calle
			, c_numeroe = p_numeroe
			, c_numeroi = p_numeroi
			, i_id_tbl_colonia = p_i_id_tbl_colonia
		WHERE i_id = tbl_domicilio_id;
		
		UPDATE religiosos."TBL_Declaratoria_Domicilio_Adicionales"
		SET c_lote = p_lote
			, c_manzana = p_manzana
			, c_super_manzana = p_super_manzana
			, c_delegacion = p_delegacion
			, c_sector = p_sector
			, c_zona = p_zona
			, c_region = p_region
		WHERE i_id_tbl_domicilio = tbl_domicilio_id AND i_id_tbl_declaratoria = p_id_declaratoria;
		
		IF (p_personas_aut <> '[]') THEN
			DELETE FROM religiosos."ASOC_Declaratoria_Personas_Autorizadas" WHERE i_id_tbl_declaratoria = p_id_declaratoria;

			INSERT INTO religiosos."ASOC_Declaratoria_Personas_Autorizadas"(i_id_tbl_declaratoria, c_nombre, c_correo_electronico, c_numero_tel)
				SELECT
				(t ->>'i_id_tbl_declaratoria')::integer i_id_tbl_declaratoria,
				t ->>'c_nombre' c_nombre,
				t ->>'c_correo_electronico' c_correo_electronico,
				(t ->>'c_numero_tel')::bigint c_numero_tel
				FROM jsonb_array_elements(to_jsonb(p_personas_aut::json)) t;
		END IF;
	
	END IF;

	RETURN QUERY 
	SELECT tbl_domicilio_id as id_generico,
		CAST('La información se guardo correctamente.' as varchar) AS mensaje,
		(true) AS proceso_existoso;

END;
$function$
;