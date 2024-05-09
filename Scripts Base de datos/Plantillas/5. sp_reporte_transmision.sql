

CREATE OR REPLACE FUNCTION religiosos.sp_reporte_transmision(
	p_fecha_inicio character varying,
	p_fecha_fin character varying,
	p_id_estatus integer DEFAULT 0)
    RETURNS TABLE(id_tramite integer, tramite character varying, id_estatus integer, estatus character varying, numero_sgar character varying, denominacion character varying, fecha_solicitud character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE

BEGIN

	RETURN QUERY
		SELECT  CASE WHEN transmision.i_id IS NULL THEN 0 ELSE transmision.i_id END as id_tramite
			, 'Transmisiones'::character varying as tramite
			, estatus.i_id_tbl_estatus as id_estatus
			, cat.nombre as estatus
			, transmision.c_numero_sgar as numero_sgar
			, transmision.c_denominacion as denominacion
			, transmision.d_fecha_solicitud::character varying as fecha_solicitud
			FROM religiosos."TBL_Transmision" transmision
			INNER JOIN (SELECT ate.i_id_tbl_estatus, ate.i_id_tbl_transmision FROM religiosos."ASOC_Transmision_Estatus" ate
				WHERE ate.i_id in (SELECT MAX (aste.i_id) FROM religiosos."ASOC_Transmision_Estatus" as aste GROUP BY aste.i_id_tbl_transmision) 
				order by ate.i_id_tbl_transmision desc) as estatus ON transmision.i_id = estatus.i_id_tbl_transmision
			LEFT JOIN religiosos."CAT_Estatus" cat ON cat.i_id = estatus.i_id_tbl_estatus
			WHERE (p_id_estatus = 0 OR estatus.i_id_tbl_estatus = p_id_estatus)
			--AND (p_fecha_inicio = 0 OR transmision.d_fecha_solicitud = TO_DATE(p_fecha_inicio,'YYYY-MM-DD'))
			AND ((p_fecha_inicio = '' AND p_fecha_fin = '') OR transmision.d_fecha_solicitud BETWEEN TO_DATE(p_fecha_inicio,'YYYY-MM-DD') AND TO_DATE(p_fecha_fin,'YYYY-MM-DD'));

END;
$BODY$;