
CREATE OR REPLACE FUNCTION religiosos.sp_consulta_estatus_transmision_filtrado(
	id_estatus integer,
	id_dictaminador integer,
	busqueda character varying)
    RETURNS TABLE(i_id_tbl_transmision integer, c_numero_sgar character varying, c_denominacion character varying, c_fecha character varying, i_id_tbl_estatus integer, c_estatus character varying, i_id_tbl_dictaminador integer, nombre_dictaminador character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE

BEGIN

	IF(id_estatus = 29) THEN
		RETURN QUERY 
				SELECT 
			tblt.i_id as i_id_tbl_transmision,
			tblt.c_numero_sgar as c_numero_sgar,
			tblt.c_denominacion as c_denominacion,
			CAST(tblt.d_fecha_solicitud AS character varying) as c_fecha,
			cate.i_id as i_id_tbl_estatus, 
			cate.nombre as c_estatus, 
			asoctd.id_tbl_usuario_dictaminador as i_id_tbl_dictaminador,
			CASE WHEN asoctd.id_tbl_usuario_dictaminador is null THEN CAST('' AS CHARACTER VARYING) 
			ELSE CAST(CONVERT_FROM(DECODE(tblu.c_usuario, 'BASE64'), 'UTF-8') AS CHARACTER VARYING) END AS nombre_dictaminador
		FROM 
			religiosos."TBL_Transmision" tblt INNER JOIN 
			religiosos."ASOC_Transmision_Estatus" asocte ON tblt.i_id = asocte.i_id_tbl_transmision INNER JOIN 
			religiosos."CAT_Estatus" cate ON asocte.i_id_tbl_estatus = cate.i_id LEFT JOIN
			religiosos."ASOC_TransmisionDictaminador" asoctd ON tblt.i_id = asoctd.i_id_tbl_transmision LEFT JOIN
			religiosos."TBL_Usuario" as tblu ON asoctd.id_tbl_usuario_dictaminador = tblu.i_id 	
		WHERE cate.i_id not in (29,32,33,34,38)
		AND 
		(busqueda IS NULL OR lower(tblt.c_numero_sgar) LIKE CONCAT('%',lower(busqueda),'%') OR lower(tblt.c_denominacion) LIKE CONCAT('%',lower(busqueda),'%') 
			OR lower(cate.nombre) LIKE CONCAT('%',lower(busqueda),'%') OR lower(tblu.c_usuario) LIKE CONCAT('%',lower(busqueda),'%')
		); 

	
	ELSEIF (id_dictaminador <> 0) THEN
		RETURN QUERY 
		SELECT CASE WHEN transmision.i_id IS NULL THEN 0 ELSE transmision.i_id END as i_id_tbl_transmision
		, transmision.c_numero_sgar as c_numero_sgar
		, transmision.c_denominacion as c_denominacion  
		, CAST(transmision.d_fecha_solicitud AS character varying) as c_fecha
		, CASE WHEN est.i_id_tbl_estatus IS NULL THEN 0 ELSE est.i_id_tbl_estatus END as i_id_tbl_estatus
		, CASE WHEN est.i_id_tbl_estatus IN (29, 30) THEN CAST('En Proceso' AS character varying)
			   WHEN est.i_id_tbl_estatus IN (31) THEN CAST('En Espera' AS character varying) 
			   WHEN est.i_id_tbl_estatus IN (32) THEN CAST('Autorizado' AS character varying)
			   WHEN est.i_id_tbl_estatus IN (33) THEN CAST('Cancelado' AS character varying)
			   WHEN est.i_id_tbl_estatus IN (34) THEN CAST('Concluida' AS character varying)
				ELSE CAST('Definir etiqueta' AS character varying) END as c_estatus
		, CASE WHEN dic.id_tbl_usuario_dictaminador IS NULL THEN 0 ELSE dic.id_tbl_usuario_dictaminador END as i_id_tbl_dictaminador,
		''::character varying as nombre_dictaminador
		FROM religiosos."TBL_Transmision" transmision
		--LEFT JOIN religiosos."ASOC_TramiteEstatus" est ON est.i_id_tbl_tramite = tbltram.i_id AND est.i_id_tbl_estatus IN(29, 30, 31, 32)
		LEFT JOIN religiosos."ASOC_Transmision_Estatus" est ON est.i_id_tbl_transmision = transmision.i_id
		LEFT JOIN religiosos."ASOC_TransmisionDictaminador" dic ON dic.i_id_tbl_transmision = transmision.i_id
		WHERE (est.i_id_tbl_estatus IN (30, 31, 32,33,34))
			AND (id_dictaminador = 0 OR dic.id_tbl_usuario_dictaminador = id_dictaminador)
			AND 
		(busqueda IS NULL OR lower(transmision.c_numero_sgar) LIKE CONCAT('%',lower(busqueda),'%') OR lower(transmision.c_denominacion) LIKE CONCAT('%',lower(busqueda),'%') );
			
	END IF;

		
END;
$BODY$;