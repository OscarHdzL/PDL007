-- FUNCTION: religiosos.sp_consulta_detalle_atender_toma_nota(integer, integer)

-- DROP FUNCTION IF EXISTS religiosos.sp_consulta_detalle_atender_toma_nota(integer, integer);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_detalle_atender_toma_nota(
	s_id_us integer DEFAULT NULL::integer,
	i_id_c integer DEFAULT NULL::integer)
    RETURNS TABLE(c_tramite integer, c_numero_sgar character varying, c_denominacion character varying, c_existe_escrito_solicitud boolean, c_existe_estatuto_solicitud boolean, c_existe_certificado_reg_solicitud boolean, c_existe_ejemplar_est_solicitud boolean, c_escritura_publica boolean, c_alta_apoderado_doc boolean, c_baja_apoderado_doc boolean, c_cambio_apoderado_doc boolean, c_id_tsol_escrito integer, c_cotejo integer, c_toma_nota integer, c_coment_est character varying, c_n_denom character varying, c_coment_n_denom character varying, c_comentario_tnota character varying, c_trtn integer, c_us integer, status integer) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
asoctramtn constant integer:=(SELECT MAX(i_id) FROM religiosos."ASOC_TramTNota" where i_id_tbl_tnota = i_id_c);
status constant integer:=(SELECT MAX(test.i_id_tbl_estatus) FROM religiosos."ASOC_TramTNota" as asoctn 
						  LEFT join religiosos."ASOC_TramiteEstatus" test on asoctn.i_id_tbl_tramite = test.i_id_tbl_tramite 
						  where asoctn.i_id_tbl_tnota = i_id_c );

tbl_tn_id integer := 0;
asoc_tramtn_id integer := 0;

BEGIN
 RETURN QUERY 
 SELECT 
	tbltram.i_id as c_tramite,
	tbltram.c_nregistro as c_numero_sgar,
	tbltram.c_denominacion as c_denominacion,
	COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
	  ELSE true END
	  FROM religiosos."ASOC_TNotaArchivo" asta 
	  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
	  WHERE asta.i_id_tbl_tnota = tbltn.i_id AND fa.i_id_tbl_archtram = 14 AND i_estatus =1 limit 1),false)
	  AS c_existe_escrito_solicitud,
	COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
	  ELSE true END
	  FROM religiosos."ASOC_TNotaArchivo" asta 
	  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
	  WHERE asta.i_id_tbl_tnota = tbltn.i_id AND fa.i_id_tbl_archtram = 17 AND i_estatus =1 limit 1),false)
	  AS c_existe_estatuto_solicitud,
	COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
	  ELSE true END
	  FROM religiosos."ASOC_TNotaArchivo" asta 
	  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
	  WHERE asta.i_id_tbl_tnota = tbltn.i_id AND fa.i_id_tbl_archtram = 19 AND i_estatus =1 limit 1),false)
	  AS c_existe_certificado_reg_solicitud,
	COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
	  ELSE true END
	  FROM religiosos."ASOC_TNotaArchivo" asta 
	  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
	  WHERE asta.i_id_tbl_tnota = tbltn.i_id AND fa.i_id_tbl_archtram = 20 AND i_estatus =1 limit 1),false)
	  AS c_existe_ejemplar_est_solicitud,
	COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
	  ELSE true END
	  FROM religiosos."ASOC_TNotaArchivo" asta 
	  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
	  WHERE asta.i_id_tbl_tnota = tbltn.i_id AND fa.i_id_tbl_archtram = 21 AND i_estatus =1 limit 1),false)
	  AS c_escritura_publica,
	COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
	  ELSE true END
	  FROM religiosos."ASOC_TNotaArchivo" asta 
	  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
	  WHERE asta.i_id_tbl_tnota = tbltn.i_id AND fa.i_id_tbl_archtram = 22 AND i_estatus =1 limit 1),false)
	  AS c_alta_apoderado_doc,
	COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
	  ELSE true END
	  FROM religiosos."ASOC_TNotaArchivo" asta 
	  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
	  WHERE asta.i_id_tbl_tnota = tbltn.i_id AND fa.i_id_tbl_archtram = 23 AND i_estatus =1 limit 1),false)
	  AS c_baja_apoderado_doc,
	COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
	  ELSE true END
	  FROM religiosos."ASOC_TNotaArchivo" asta 
	  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
	  WHERE asta.i_id_tbl_tnota = tbltn.i_id AND fa.i_id_tbl_archtram = 24 AND i_estatus =1 limit 1),false)
	  AS c_cambio_apoderado_doc,  
	tbltn.i_id_cat_tsol_escrito as c_id_tsol_escrito,
	tblcot.i_idcotejodoc as c_cotejo,
	tbltn.i_id as c_toma_nota,
	tbltn.c_comentario_estatutos as c_coment_est,
	tbltn.c_nueva_denominacion as c_n_denom,
	tbltn.c_comentario_n_denominacion as c_coment_n_denom,
	tbltn.c_comentario_tnota as c_comentario_tnota,
	asoctn.i_id as c_trtn,
	astu.i_id_tbl_usuario  as c_us,
    status
		FROM 
		religiosos."ASOC_TramTNota" as asoctn left join
		religiosos."TBL_Tramite" as tbltram on tbltram.i_id = asoctn.i_id_tbl_tramite LEFT join
		religiosos."TBL_TNota" as tbltn on tbltn.i_id = asoctn.i_id_tbl_tnota LEFT join
		religiosos."TBL_Cotejo" as tblcot on tblcot.i_id = tbltn.i_id_cat_cotejodoc_est LEFT join
		religiosos."CAT_Cotejodoc" as catcot on catcot.i_id = tblcot.i_idcotejodoc LEFT join
		religiosos."CAT_TSol_Escrito" as catsolesc on catsolesc.i_id = tbltn.i_id_cat_tsol_escrito LEFT JOIN

		religiosos."ASOC_Tramite_Usuario" astu ON astu.i_id_tbl_tramite = tbltram.i_id
		
		WHERE tbltn.i_id = i_id_c;
	 
 
 

END;
$BODY$;

ALTER FUNCTION religiosos.sp_consulta_detalle_atender_toma_nota(integer, integer)
    OWNER TO tramitesdgardev_user;
