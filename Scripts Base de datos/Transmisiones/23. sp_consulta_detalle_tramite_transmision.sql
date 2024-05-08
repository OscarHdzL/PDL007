DROP FUNCTION IF EXISTS religiosos.sp_consulta_detalle_tramite_transmision(integer, integer);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_detalle_tramite_transmision(
	s_id_us integer,
	i_id_transmision integer)
    RETURNS TABLE(c_us integer, i_id_tbl_transmision integer, numero_sgar character varying, denominacion character varying, numero_tel character varying, correo_electronico character varying, domicilio character varying, rep_nombre_completo character varying, b_identificacion integer, b_solicitudtrans integer, estatus integer) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
						  						 
BEGIN

	IF(i_id_transmision <> 0 OR s_id_us = 0 ) THEN
	
		RETURN QUERY 
			SELECT  asoc_usuario.i_id_tbl_usuario  AS c_us
			, CASE WHEN transmision.i_id IS NULL THEN 0 ELSE transmision.i_id END AS i_id_tbl_transmision
			, transmision.c_numero_sgar AS c_numero_sgar
			, transmision.c_denominacion AS c_denominacion
			, transmision.c_numero_tel AS c_numero_tel
			, transmision.c_correo_electronico AS c_correo_electronico
			, transmision.c_domicilio AS c_domicilio
			, CAST(CONCAT(CAST(CONVERT_FROM(DECODE(persona.c_nombre, 'BASE64'), 'UTF-8')AS character varying),' ',
						CAST(CONVERT_FROM(DECODE(persona.c_apaterno, 'BASE64'), 'UTF-8')AS character varying),' ',
						CAST(CONVERT_FROM(DECODE(persona.c_amaterno, 'BASE64'), 'UTF-8')AS character varying)) AS character varying)
			AS rep_nombre_completo
			, (SELECT ta.i_id
				FROM religiosos."ASOC_Transmision_Archivos" asoc
			   	INNER JOIN religiosos."TBL_Archivo" ta ON asoc.i_id_tbl_archivo = ta.i_id
			   	WHERE asoc.i_id_tbl_transmision = i_id_transmision
			    AND ta.i_id_tbl_archtram = 27
			   	AND ta.i_estatus = 1 -- ACTIVO
			   	LIMIT 1) AS b_identificacion
			, (SELECT ta.i_id
				FROM religiosos."ASOC_Transmision_Archivos" asoc
			   	INNER JOIN religiosos."TBL_Archivo" ta ON asoc.i_id_tbl_archivo = ta.i_id
			   	WHERE asoc.i_id_tbl_transmision = i_id_transmision
			    AND ta.i_id_tbl_archtram = 28
			   	AND ta.i_estatus = 1 -- ACTIVO
			   	LIMIT 1) AS b_solicitudTrans
			, estatus.i_id_tbl_estatus as estatus 
			FROM religiosos."TBL_Transmision" transmision
			INNER JOIN religiosos."ASOC_Transmision_Usuario" asoc_usuario ON asoc_usuario.i_id_tbl_transmision = transmision.i_id
			INNER JOIN religiosos."TBL_Usuario" usuario ON usuario.i_id = asoc_usuario.i_id_tbl_usuario
			INNER JOIN religiosos."TBL_Persona" persona ON persona.i_id = usuario.i_id_tbl_persona
			--LEFT JOIN religiosos."ASOC_Transmision_Estatus" estatus ON estatus.i_id_tbl_transmision = transmision.i_id --AND est.i_id_tbl_estatus IN(29, 30, 31, 32, 33, 34)
			LEFT JOIN (SELECT ate.i_id_tbl_estatus, ate.i_id_tbl_transmision FROM religiosos."ASOC_Transmision_Estatus" ate
				WHERE ate.i_id in (SELECT MAX (aste.i_id) FROM religiosos."ASOC_Transmision_Estatus" as aste GROUP BY aste.i_id_tbl_transmision) 
				order by ate.i_id_tbl_transmision desc) as estatus on transmision.i_id = estatus.i_id_tbl_transmision
			WHERE transmision.i_id = i_id_transmision
			AND (s_id_us = 0 OR asoc_usuario.i_id_tbl_usuario = s_id_us);

	ELSE
		
		RETURN QUERY 
			SELECT  us.i_id  as c_us
			, 0 as i_id_tbl_transmision
			, CAST('' as character varying) as c_numero_sgar
			, CAST('' as character varying) as c_denominacion
			, CAST('' as character varying) as c_numero_tel
			, CAST('' as character varying) as c_correo_electronico
			, CAST('' as character varying) as c_domicilio
			, CAST(CONCAT(CAST(CONVERT_FROM(DECODE(tp.c_nombre, 'BASE64'), 'UTF-8')AS character varying),' ',
						CAST(CONVERT_FROM(DECODE(tp.c_apaterno, 'BASE64'), 'UTF-8')AS character varying),' ',
						CAST(CONVERT_FROM(DECODE(tp.c_amaterno, 'BASE64'), 'UTF-8')AS character varying)) AS character varying) 
			AS rep_nombre_completo
			, 0 as b_identificacion
			, 0 as b_solicitudTrans
			, 0 as estatus
			FROM religiosos."TBL_Usuario" us
			INNER JOIN religiosos."TBL_Persona" tp ON tp.i_id = us.i_id_tbl_persona
			WHERE us.i_id = s_id_us;
	
	END IF;

		
END;
$BODY$;
