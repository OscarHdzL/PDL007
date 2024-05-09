
CREATE OR REPLACE FUNCTION religiosos.sp_reporte_tnota(
	p_fecha_inicio character varying,
	p_fecha_fin character varying,
	p_id_estatus integer)
    RETURNS TABLE(id_tramite integer, tramite character varying, id_estatus integer, estatus character varying, numero_sgar character varying, denominacion character varying, fecha_solicitud character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE

BEGIN

	RETURN QUERY
		SELECT id_tramite_, tramite_, id_estatus_, estatus_, c_numero_sgar_, denominacion_, fecha_solicitud_ FROM (
				SELECT tram.i_id as id_tramite_,
				'Trámite de Nota'::character varying as tramite_,
				catsta.i_id as id_estatus_,
				catsta.nombre as estatus_,
				tram.c_nregistro as c_numero_sgar_,
				tram.c_denominacion as denominacion_,
				CAST((SELECT tblf.d_inicio FROM religiosos."ASOC_Tramite_Fechas" asoctf 
					  INNER JOIN religiosos."TBL_Fechas" AS tblf ON asoctf.i_id_tbl_fechas = tblf.i_id
					  WHERE asoctf.i_id_tbl_tramite = tram.i_id and tblf.i_id_cat_fechas = 3 limit 1)AS CHARACTER VARYING)AS fecha_solicitud_
				FROM religiosos."TBL_Tramite" as tram
				LEFT JOIN religiosos."ASOC_TramTNota" as asocttn on tram.i_id = asocttn.i_id_tbl_tramite
				LEFT JOIN (SELECT i_id_tbl_estatus, i_id_tbl_tramite 
						  FROM religiosos."ASOC_TramiteEstatus"
						  WHERE i_id in (SELECT MAX (i_id) FROM religiosos."ASOC_TramiteEstatus" GROUP BY i_id_tbl_tramite) 
						  order by i_id_tbl_tramite desc) as tramest on tram.i_id = tramest.i_id_tbl_tramite 
				LEFT JOIN religiosos."CAT_Estatus" as catsta on tramest.i_id_tbl_estatus = catsta.i_id 
				WHERE (tram.c_denominacion <> '')
				AND catsta.b_estatus_tnota = '1'
				ORDER BY tram.i_id
			) AS main
			WHERE (p_id_estatus = 0 OR main.id_estatus_ = p_id_estatus)
		     AND ((p_fecha_inicio = '' AND p_fecha_fin = '') OR CAST(main.fecha_solicitud_ AS DATE) BETWEEN TO_DATE(p_fecha_inicio,'YYYY-MM-DD') AND TO_DATE(p_fecha_fin,'YYYY-MM-DD'));
END;
$BODY$;
