-- FUNCTION: religiosos.sp_consulta_lista_registros_tnota_dict(integer, character varying, character varying, integer)

-- DROP FUNCTION IF EXISTS religiosos.sp_consulta_lista_registros_tnota_dict(integer, character varying, character varying, integer);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_lista_registros_tnota_dict(
	id_usuario integer,
	denominacion_desc character varying,
	nombre_desc character varying,
	estatus_desc integer)
    RETURNS TABLE(id_tramite integer, id_tnota integer, nombre character varying, denominacion character varying, folio character varying, tramite_solicitado character varying, fecha_registro date, id_estatus integer, estatus character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN

IF estatus_desc= 27 OR estatus_desc=28 THEN
	RETURN QUERY
	select tbl_tramite.i_id AS id_tramite,
				tbl_tnota.i_id AS id_tnota,
				tbl_tramite.c_nregistro AS nombre,
				tbl_tramite.c_denominacion AS denominacion, 
				tbl_tnota.c_nfolio AS folio, 
				tbl_sol_registro.nombre AS tramite_solicitado, 
				(SELECT tblf.d_inicio FROM religiosos."ASOC_Tramite_Fechas" asoctf 
				INNER JOIN religiosos."TBL_Fechas" AS tblf ON asoctf.i_id_tbl_fechas = tblf.i_id
				WHERE asoctf.i_id_tbl_tramite = tbl_tramite.i_id and tblf.i_id_cat_fechas = 3 limit 1) AS fecha_registro,
				tbl_estatus.i_id AS id_estatus,
				tbl_estatus.nombre AS estatus
				
			    FROM religiosos."TBL_Tramite" AS tbl_tramite 
			     LEFT JOIN religiosos."ASOC_TramTNota" AS tbl_tnota_tramite ON  tbl_tramite.i_id = tbl_tnota_tramite.i_id_tbl_tramite
			     INNER JOIN religiosos."TBL_TNota" AS tbl_tnota ON  tbl_tnota.i_id = tbl_tnota_tramite.i_id_tbl_tnota
				 LEFT JOIN religiosos."CAT_TSol_Escrito" AS tbl_sol_registro ON  tbl_tnota.i_id_cat_tsol_escrito = tbl_sol_registro.i_id
				 LEFT JOIN religiosos."ASOC_TnotaDictaminador" AS tbl_asoc_tramite ON tbl_tnota.i_id = tbl_asoc_tramite.i_id_tbl_tnota
			    
				LEFT JOIN (SELECT i_id_tbl_estatus, i_id_tbl_tramite FROM religiosos."ASOC_TramiteEstatus"
				WHERE i_id in (SELECT MAX (i_id) FROM religiosos."ASOC_TramiteEstatus" GROUP BY i_id_tbl_tramite) 
				order by i_id_tbl_tramite desc) as tramest on tbl_tramite.i_id = tramest.i_id_tbl_tramite
				LEFT JOIN  religiosos."CAT_Estatus" AS tbl_estatus ON tramest.i_id_tbl_estatus = tbl_estatus.i_id
				 WHERE tbl_asoc_tramite.i_id_tbl_usuariodictam = id_usuario
				  AND ((denominacion_desc IS null) OR (LOWER(tbl_tramite.c_denominacion) LIKE ('%'||LOWER(denominacion_desc)||'%'))) 
				  AND ((nombre_desc IS null) OR (LOWER(tbl_tramite.c_nregistro) LIKE ('%'||LOWER(nombre_desc)||'%'))) 
		          AND ((estatus_desc IS null ) OR ( tbl_estatus.i_id=estatus_desc ))
				  AND tbl_estatus.i_id in (27,28);
		ELSE
		
			RETURN QUERY
	SELECT tbl_tramite.i_id AS id_tramite,
				tbl_tnota.i_id AS id_tnota,
				tbl_tramite.c_nregistro AS nombre,
				tbl_tramite.c_denominacion AS denominacion, 
				tbl_tnota.c_nfolio AS folio, 
				tbl_sol_registro.nombre AS tramite_solicitado, 
				(SELECT tblf.d_inicio FROM religiosos."ASOC_Tramite_Fechas" asoctf 
				INNER JOIN religiosos."TBL_Fechas" AS tblf ON asoctf.i_id_tbl_fechas = tblf.i_id
				WHERE asoctf.i_id_tbl_tramite = tbl_tramite.i_id and tblf.i_id_cat_fechas = 3 limit 1) AS fecha_registro,
				tbl_estatus.i_id AS id_estatus,
				tbl_estatus.nombre AS estatus
				
			    FROM religiosos."TBL_Tramite" AS tbl_tramite 
			     LEFT JOIN religiosos."ASOC_TramTNota" AS tbl_tnota_tramite ON  tbl_tramite.i_id = tbl_tnota_tramite.i_id_tbl_tramite
			     INNER JOIN religiosos."TBL_TNota" AS tbl_tnota ON  tbl_tnota.i_id = tbl_tnota_tramite.i_id_tbl_tnota
				  LEFT JOIN religiosos."CAT_TSol_Escrito" AS tbl_sol_registro ON  tbl_tnota.i_id_cat_tsol_escrito = tbl_sol_registro.i_id
				 LEFT JOIN religiosos."ASOC_TnotaDictaminador" AS tbl_asoc_tramite ON tbl_tnota.i_id = tbl_asoc_tramite.i_id_tbl_tnota
			    
				LEFT JOIN (SELECT i_id_tbl_estatus, i_id_tbl_tramite FROM religiosos."ASOC_TramiteEstatus"
				WHERE i_id in (SELECT MAX (i_id) FROM religiosos."ASOC_TramiteEstatus" GROUP BY i_id_tbl_tramite) 
				order by i_id_tbl_tramite desc) as tramest on tbl_tramite.i_id = tramest.i_id_tbl_tramite
				LEFT JOIN  religiosos."CAT_Estatus" AS tbl_estatus ON tramest.i_id_tbl_estatus = tbl_estatus.i_id
				 WHERE  tbl_asoc_tramite.i_id_tbl_usuariodictam = id_usuario
				  AND ((denominacion_desc IS null) OR (LOWER(tbl_tramite.c_denominacion) LIKE ('%'||LOWER(denominacion_desc)||'%'))) 
				  AND ((nombre_desc IS null) OR (LOWER(tbl_tramite.c_nregistro) LIKE ('%'||LOWER(nombre_desc)||'%'))) 
		          AND ((estatus_desc IS null ) OR ( tbl_estatus.i_id=estatus_desc ))
				  AND tbl_estatus.i_id in (21,22,23,24,25,26,37, 27, 28);
			END IF;

END
$BODY$;

ALTER FUNCTION religiosos.sp_consulta_lista_registros_tnota_dict(integer, character varying, character varying, integer)
    OWNER TO postgres;
