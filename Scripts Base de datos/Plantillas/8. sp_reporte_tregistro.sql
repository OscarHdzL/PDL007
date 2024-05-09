
CREATE OR REPLACE FUNCTION religiosos.sp_reporte_tregistro(
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
		SELECT DISTINCT tblt.i_id as id_tramite,
			'Trámite de Registro'::character varying as tramite,
			cats.i_id as id_estatus,
			cats.nombre as estatus,
			tblt.c_nregistro as c_numero_sgar,
			tblt.c_denominacion as denominacion,
			tblf.d_inicio::character varying as fecha_solicitud
		FROM religiosos."TBL_Tramite" as tblt
		INNER JOIN(SELECT i_id_tbl_estatus, i_id_tbl_tramite 
				   FROM religiosos."ASOC_TramiteEstatus"
			 		WHERE i_id in (SELECT MAX (i_id) FROM religiosos."ASOC_TramiteEstatus" GROUP BY i_id_tbl_tramite) 
			 		order by i_id_tbl_tramite desc) as tramest on tblt.i_id = tramest.i_id_tbl_tramite 
		INNER JOIN religiosos."CAT_Estatus" as cats on tramest.i_id_tbl_estatus = cats.i_id 
		INNER JOIN religiosos."ASOC_Tramite_Fechas" as asoctf on tblt.i_id = asoctf.i_id_tbl_tramite 
		INNER JOIN religiosos."TBL_Fechas" as tblf on asoctf.i_id_tbl_fechas = tblf.i_id AND tblf.i_id_cat_fechas isnull
		WHERE cats.b_estatus_registro = '1'
		and (p_id_estatus = 0 OR cats.i_id = p_id_estatus)
		AND ((p_fecha_inicio = '' AND p_fecha_fin = '') OR tblf.d_inicio BETWEEN TO_DATE(p_fecha_inicio,'YYYY-MM-DD') AND TO_DATE(p_fecha_fin,'YYYY-MM-DD'));
END;
$BODY$;
