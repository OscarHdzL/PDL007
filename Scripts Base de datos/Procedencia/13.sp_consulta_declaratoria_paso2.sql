-- DROP FUNCTION religiosos.sp_consulta_declaratoria_paso2(int4, int4);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_declaratoria_paso2(p_id_declaratoria integer, p_tipo_domicilio integer)
 RETURNS TABLE(id_declaratoria integer, tipo_domicilio integer, codigo_postal character varying, id_estado integer, estado character varying, id_ciudad integer, ciudad character varying, colonia character varying, clave_municipio integer, id_domicilio integer, calle character varying, numeroe character varying, numeroi character varying, id_colonia integer, lote character varying, manzana character varying, super_manzana character varying, delegacion character varying, sector character varying, zona character varying, region character varying, personas_autorizadas text)
 LANGUAGE plpgsql
AS $function$
DECLARE

BEGIN

	RETURN QUERY
		SELECT adicional.i_id_tbl_declaratoria as id_declaratoria
			, domicilio.i_id_tbl_tdomicilio as tipo_domicilio
			, colonia.c_cpostal as codigo_postal
			, estado.i_id as id_estado
			, estado.nombre as estado
			, municipio.i_id as id_ciudad
			, municipio.nombre as ciudad
			, colonia.nombre as colonia
			, colonia.clave_municipio
			, domicilio.i_id as id_domicilio
			, domicilio.c_calle as calle
			, domicilio.c_numeroe as numeroe
			, domicilio.c_numeroi as numeroi
			, domicilio.i_id_tbl_colonia as id_colonia
			, adicional.c_lote as lote
			, adicional.c_manzana as manzana
			, adicional.c_super_manzana as c_super_manzana
			, adicional.c_delegacion as delegacion
			, adicional.c_sector as sector
			, adicional.c_zona as zona
			, adicional.c_region as region
			, CASE WHEN p_tipo_domicilio = 2 THEN
			'[ ' || array_to_string(ARRAY(
				SELECT ('{ "i_id": ' || persona.i_id || ', "c_nombre": "' || persona.c_nombre || '", "c_correo_electronico": "' || persona.c_correo_electronico || '", "c_numero_tel": ' || CAST(persona.c_numero_tel AS character varying) || ' }')
				FROM religiosos."ASOC_Declaratoria_Personas_Autorizadas" persona
				WHERE persona.i_id_tbl_declaratoria = p_id_declaratoria),
				', ' ) || ' ]'
			 	WHEN p_tipo_domicilio = 3 THEN '[]' END personas_autorizadas
		FROM religiosos."TBL_Domicilio" domicilio
		INNER JOIN religiosos."TBL_Declaratoria_Domicilio_Adicionales" adicional
			ON adicional.i_id_tbl_domicilio = domicilio.i_id
		INNER JOIN religiosos."CAT_Colonia" colonia
			ON colonia.i_id = domicilio.i_id_tbl_colonia
		INNER JOIN religiosos."CAT_Municipio" municipio
			ON municipio.clave = colonia.clave_municipio
		INNER JOIN religiosos."CAT_Estado" estado
			ON estado.i_id = municipio.i_id_tbl_estado
		WHERE adicional.i_id_tbl_declaratoria = p_id_declaratoria
			AND domicilio.i_id_tbl_tdomicilio = p_tipo_domicilio
		LIMIT 1;

END;
$function$
;