
CREATE OR REPLACE FUNCTION religiosos.sp_consulta_detalle_transmision_cotejo_publico_new(
	s_id_us integer,
	id_trans integer)
    RETURNS TABLE(s_id_tramite integer, s_estatus integer, s_fecha text, s_direccion character varying, s_comentarios character varying, usuario text) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
status integer;
BEGIN

SELECT asocte.i_id_tbl_estatus
INTO status
FROM religiosos."ASOC_Transmision_Usuario" asoctu 
INNER JOIN religiosos."ASOC_Transmision_Estatus" asocte ON asoctu.i_id_tbl_transmision = asocte.i_id_tbl_transmision 
INNER JOIN religiosos."TBL_Transmision" tblt ON asocte.i_id_tbl_transmision = tblt.i_id
WHERE asoctu.i_id_tbl_usuario = s_id_us
ORDER BY asocte.i_id DESC limit 1;
	
	IF(status in (29,30,31,32,33, 34)) THEN
		RETURN QUERY 
			SELECT 
				tblt.i_id,
				asocte.i_id_tbl_estatus,
				to_char(tblt.d_fecha_cotejo, 'DD/MM/YYYY') || ' ' || CAST(tblt.c_horario_cotejo AS CHARACTER VARYING) AS fecha_cotejo,
				tblt.c_direccion,
				tblot.c_observacion,
				CONVERT_FROM(DECODE(tblp.c_nombre, 'BASE64'), 'UTF-8') ||' '|| CONVERT_FROM(DECODE(tblp.c_apaterno, 'BASE64'), 'UTF-8')  AS Usuario
			FROM religiosos."ASOC_Transmision_Usuario" asoctu 
				--INNER JOIN religiosos."ASOC_TramiteEstatus" asocte ON asoctu.i_id_tbl_tramite = asocte.i_id_tbl_tramite
				--INNER JOIN religiosos."TBL_Tramite" tra ON tra.i_id = asoctu.i_id_tbl_tramite
				INNER JOIN religiosos."TBL_Transmision" tblt ON tblt.i_id = asoctu.i_id_tbl_transmision
				INNER JOIN religiosos."ASOC_Transmision_Estatus" asocte ON tblt.i_id = asocte.i_id_tbl_transmision
				LEFT JOIN religiosos."TBL_Observaciones_Transmision" tblot ON tblt.i_id = tblot.i_id_tbl_transmision 
				LEFT JOIN religiosos."ASOC_TransmisionDictaminador" asoctd ON tblt.i_id = asoctd.i_id_tbl_transmision 
				LEFT JOIN religiosos."TBL_Usuario" tblu ON asoctd.id_tbl_usuario_dictaminador = tblu.i_id 
				LEFT JOIN religiosos."TBL_Persona" tblp ON tblu.i_id_tbl_persona = tblp.i_id
			WHERE 
				asoctu.i_id_tbl_usuario = s_id_us and tblt.i_id = id_trans
			ORDER BY 
				tblt.i_id DESC, asocte.i_id DESC, tblot.i_id DESC limit 1;
	ELSE
	    RETURN;
	END IF;
END;
$BODY$;
