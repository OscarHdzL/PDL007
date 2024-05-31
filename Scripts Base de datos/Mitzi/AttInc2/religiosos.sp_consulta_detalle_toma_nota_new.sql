-- FUNCTION: religiosos.sp_consulta_detalle_toma_nota_new(integer, integer, integer)

-- DROP FUNCTION IF EXISTS religiosos.sp_consulta_detalle_toma_nota_new(integer, integer, integer);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_detalle_toma_nota_new(
	s_id_us integer,
	id_tramite integer,
	i_id_c integer DEFAULT NULL::integer)
    RETURNS TABLE(c_tramite integer, c_numero_sgar character varying, c_denominacion character varying, c_comentario_tnota character varying, c_existe_escrito_solicitud boolean, c_existe_estatuto_solicitud boolean, c_existe_certificado_reg_solicitud boolean, c_existe_ejemplar_est_solicitud boolean, c_escritura_publica boolean, c_alta_apoderado_doc boolean, c_baja_apoderado_doc boolean, c_cambio_apoderado_doc boolean, c_id_tsol_escrito integer, c_cotejo integer, c_toma_nota integer, c_coment_est character varying, c_n_denom character varying, c_coment_n_denom character varying, c_trtn integer, c_us integer) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$

DECLARE
tramite integer;
tbl_tn_id integer := 0;
asoc_tramtn_id integer := 0;
fecha_inicio integer := 0;
id_tramiteNota integer := 0;
id_aux integer := 0;
denominacion_aux character varying;
numero_aux character varying;

BEGIN
	if( id_tramite = 0)then
	
		id_tramiteNota = (SELECT MAX(i_id) 
						  FROM religiosos."TBL_TNota");
						  
		id_aux = (SELECT i_id_cat_tsol_escrito 
				  FROM religiosos."TBL_TNota" 
				  WHERE i_id = id_tramiteNota );
				  
	    denominacion_aux = (SELECT tra.c_denominacion
							FROM religiosos."TBL_Tramite" tra
							INNER JOIN religiosos."ASOC_TramTNota" asoc ON asoc.i_id_tbl_tramite = tra.i_id
							INNER JOIN religiosos."TBL_TNota" nota ON nota.i_id = asoc.i_id_tbl_tnota
							WHERE asoc.i_id_tbl_tnota = id_tramiteNota);
							
		numero_aux = (SELECT tra.c_nregistro
					  FROM religiosos."TBL_Tramite" tra
					  INNER JOIN religiosos."ASOC_TramTNota" asoc ON asoc.i_id_tbl_tramite = tra.i_id
					  INNER JOIN religiosos."TBL_TNota" nota ON nota.i_id = asoc.i_id_tbl_tnota
					  WHERE asoc.i_id_tbl_tnota = id_tramiteNota);
		
	
		IF( denominacion_aux = '' AND numero_aux = '' )THEN
	
			tramite = (SELECT i_id_tbl_tramite 
					   FROM religiosos."ASOC_TramTNota" 
					   WHERE i_id_tbl_tnota = id_tramiteNota);
					   
			UPDATE religiosos."ASOC_Tramite_Usuario"
			SET i_id_tbl_usuario = s_id_us
			WHERE i_id_tbl_tramite = tramite;
					   	   
		ELSE
			INSERT INTO religiosos."TBL_Tramite"(c_nregistro,c_denominacion,c_nfolio)
			VALUES ('','','') RETURNING i_id INTO tramite;

			INSERT INTO religiosos."ASOC_TramiteEstatus"(i_id_tbl_tramite,i_id_tbl_estatus)
			VALUES (tramite,19);

			INSERT INTO religiosos."ASOC_TramiteEstatus"(i_id_tbl_tramite,i_id_tbl_estatus)
			VALUES (tramite,12);

			INSERT INTO religiosos."ASOC_Tramite_Usuario"(i_id_tbl_tramite,i_id_tbl_usuario)
			VALUES (tramite, s_id_us);

			--INSERT INTO religiosos."TBL_Fechas"(d_inicio, i_estatus, i_id_cat_fechas) 
			--VALUES (current_timestamp, 1, 3) RETURNING i_id INTO fecha_inicio;

			--INSERT INTO religiosos."ASOC_Tramite_Fechas"(i_id_tbl_tramite, i_id_tbl_fechas)
			--VALUES (tramite, fecha_inicio);

			INSERT INTO religiosos."TBL_TNota"(c_comentario_estatutos,c_nueva_denominacion,c_comentario_n_denominacion)
			VALUES ('','','') RETURNING i_id INTO tbl_tn_id;

			INSERT INTO religiosos."ASOC_TramTNota"(i_id_tbl_tramite,i_id_tbl_tnota)
				VALUES (tramite,tbl_tn_id)
			RETURNING i_id INTO asoc_tramtn_id;
		END IF;
	
 RETURN QUERY 
 SELECT 
	tbltram.i_id as c_tramite,
	tbltram.c_nregistro as c_numero_sgar,
	tbltram.c_denominacion as c_denominacion,
	tbltn.c_comentario_tnota as c_comentario_tnota,
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
	asoctn.i_id as c_trtn,
	astu.i_id_tbl_usuario as c_us	
		FROM 
		religiosos."ASOC_TramTNota" as asoctn LEFT join
		religiosos."TBL_Tramite" as tbltram on tbltram.i_id = asoctn.i_id_tbl_tramite LEFT join
		religiosos."TBL_TNota" as tbltn on tbltn.i_id = asoctn.i_id_tbl_tnota LEFT join
		religiosos."TBL_Cotejo" as tblcot on tblcot.i_id = tbltn.i_id_cat_cotejodoc_est LEFT join
		religiosos."CAT_Cotejodoc" as catcot on catcot.i_id = tblcot.i_idcotejodoc LEFT join
		religiosos."CAT_TSol_Escrito" as catsolesc on catsolesc.i_id = tbltn.i_id_cat_tsol_escrito LEFT JOIN
		religiosos."ASOC_Tramite_Usuario" astu ON astu.i_id_tbl_tramite = tbltram.i_id
		
		WHERE astu.i_id_tbl_usuario = s_id_us and tbltram.i_id = tramite
		ORDER BY tbltram.i_id desc
		LIMIT 1;
	end if;
	
if( id_tramite != 0)then
 return query
 SELECT 
	tbltram.i_id as c_tramite,
	tbltram.c_nregistro as c_numero_sgar,
	tbltram.c_denominacion as c_denominacion,
	tbltn.c_comentario_tnota as c_comentario_tnota,
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
	asoctn.i_id as c_trtn,
	astu.i_id_tbl_usuario as c_us	
		FROM 
		religiosos."ASOC_TramTNota" as asoctn LEFT join
		religiosos."TBL_Tramite" as tbltram on tbltram.i_id = asoctn.i_id_tbl_tramite LEFT join
		religiosos."TBL_TNota" as tbltn on tbltn.i_id = asoctn.i_id_tbl_tnota LEFT join
		religiosos."TBL_Cotejo" as tblcot on tblcot.i_id = tbltn.i_id_cat_cotejodoc_est LEFT join
		religiosos."CAT_Cotejodoc" as catcot on catcot.i_id = tblcot.i_idcotejodoc LEFT join
		religiosos."CAT_TSol_Escrito" as catsolesc on catsolesc.i_id = tbltn.i_id_cat_tsol_escrito LEFT JOIN
		religiosos."ASOC_Tramite_Usuario" astu ON astu.i_id_tbl_tramite = tbltram.i_id	
		WHERE astu.i_id_tbl_usuario = s_id_us and tbltram.i_id = id_tramite;
	end if;

END;
$BODY$;

ALTER FUNCTION religiosos.sp_consulta_detalle_toma_nota_new(integer, integer, integer)
    OWNER TO tramitesdgardev_user;
