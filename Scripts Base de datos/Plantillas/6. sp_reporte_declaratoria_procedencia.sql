
CREATE OR REPLACE FUNCTION religiosos.sp_reporte_declaratoria_procedencia(
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
		SELECT declaratoria.i_id as id_tramite
		, 'Declaratoria de Procedencia'::character varying as tramite
		, declaratoria.i_id_tbl_estatus as id_estatus
		, estatus.nombre as estatus
		, declaratoria.c_numero_sgar
		, declaratoria.c_denominacion_religiosa as denominacion
		, declaratoria.d_fecha_envio:: character varying as fecha_solicitud
		FROM religiosos."TBL_Declaratoria_Procedencia" declaratoria
		INNER JOIN religiosos."CAT_Estatus" estatus ON estatus.i_id = declaratoria.i_id_tbl_estatus
			WHERE (p_id_estatus = 0 OR declaratoria.i_id_tbl_estatus = p_id_estatus)
			AND ((p_fecha_inicio = '' AND p_fecha_fin = '') OR declaratoria.d_fecha_envio BETWEEN TO_DATE(p_fecha_inicio,'YYYY-MM-DD') AND TO_DATE(p_fecha_fin,'YYYY-MM-DD'));

END;
$BODY$;
