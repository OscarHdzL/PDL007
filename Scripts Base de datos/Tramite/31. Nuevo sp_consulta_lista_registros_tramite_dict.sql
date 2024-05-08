CREATE OR REPLACE FUNCTION religiosos.sp_consulta_lista_registros_tramite_dict(
	id_usuario integer,
	numero_sgar character varying,
	denominacion_desc character varying,
	estatus_desc integer,
	credo_desc integer)
    RETURNS TABLE(id_registro integer, nombre character varying, folio character varying, denominacion character varying, credo character varying, sol_registro character varying, fecha_registro date, id_estatus integer, estatus character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
IF estatus_desc= 19 OR estatus_desc=20 THEN
   	RETURN QUERY
	SELECT 
				DISTINCT tbl_tramite.i_id AS id_registro,
				tbl_tramite.c_nregistro AS nombre, 
				tbl_tramite.c_nfolio AS folio,
				tbl_tramite.c_denominacion AS denominacion, 
				tbl_credo.nombre AS credo, 
				tbl_sol_registro.c_nombre AS sol_registro,
				tbl_fechas.d_inicio AS sol_registro,
				tbl_estatus.i_id AS id_estatus,
				tbl_estatus.nombre AS estatus
				
			    FROM religiosos."TBL_Tramite" AS tbl_tramite 
		 	     LEFT JOIN religiosos."CAT_Credo" AS tbl_credo ON tbl_tramite.i_id_tbl_credo = tbl_credo.i_id
			     LEFT JOIN religiosos."CAT_TSolReg" AS tbl_sol_registro ON  tbl_tramite.i_id_tbl_tsolreg = tbl_sol_registro.i_id
				
				LEFT JOIN religiosos."ASOC_TraDictaminador" AS tbl_asoc_tramite ON tbl_tramite.i_id = tbl_asoc_tramite.i_id_tbl_tramite
			    
				LEFT JOIN (SELECT i_id_tbl_estatus, i_id_tbl_tramite FROM religiosos."ASOC_TramiteEstatus"
				WHERE i_id in (SELECT MAX (i_id) FROM religiosos."ASOC_TramiteEstatus" GROUP BY i_id_tbl_tramite) 
				order by i_id_tbl_tramite desc) as tramest on tbl_tramite.i_id = tramest.i_id_tbl_tramite
				
				LEFT JOIN  religiosos."CAT_Estatus" AS tbl_estatus ON tramest.i_id_tbl_estatus = tbl_estatus.i_id
				LEFT JOIN  religiosos."ASOC_Tramite_Fechas" AS tbl_fechas_asoc ON tbl_tramite.i_id= tbl_fechas_asoc.i_id_tbl_tramite
				LEFT JOIN  religiosos."TBL_Fechas" AS tbl_fechas ON tbl_fechas.i_id =  tbl_fechas_asoc.i_id_tbl_fechas
				 WHERE  tbl_fechas.i_id_cat_fechas=3  
				  AND tbl_asoc_tramite.i_id_tbl_usuariodictam = id_usuario 
				  AND ((denominacion_desc IS null) OR (LOWER(tbl_tramite.c_denominacion) LIKE ('%'||LOWER(denominacion_desc)||'%'))) 
				  AND ((numero_sgar IS null) OR (LOWER(tbl_tramite.c_nregistro) LIKE ('%'||LOWER(numero_sgar)||'%'))) 
		          AND ((estatus_desc IS null ) OR ( tbl_estatus.i_id=estatus_desc ))
				  AND ((credo_desc IS null ) OR (tbl_credo.i_id=credo_desc ))
				  AND tbl_estatus.i_id in (19,20);
ELSE
  	  RETURN QUERY
     	SELECT 
				DISTINCT tbl_tramite.i_id AS id_registro,
				tbl_tramite.c_nregistro AS nombre, 
				tbl_tramite.c_nfolio AS folio,
				tbl_tramite.c_denominacion AS denominacion, 
				tbl_credo.nombre AS credo, 
				tbl_sol_registro.c_nombre AS sol_registro,
				tbl_fechas.d_inicio AS sol_registro,
				tbl_estatus.i_id AS id_estatus,
				tbl_estatus.nombre AS estatus
				
			    FROM religiosos."TBL_Tramite" AS tbl_tramite 
		 	     LEFT JOIN religiosos."CAT_Credo" AS tbl_credo ON tbl_tramite.i_id_tbl_credo = tbl_credo.i_id
			     LEFT JOIN religiosos."CAT_TSolReg" AS tbl_sol_registro ON  tbl_tramite.i_id_tbl_tsolreg = tbl_sol_registro.i_id
				 LEFT JOIN religiosos."ASOC_TraDictaminador" AS tbl_asoc_tramite ON tbl_tramite.i_id = tbl_asoc_tramite.i_id_tbl_tramite
			       
				LEFT JOIN (SELECT i_id_tbl_estatus, i_id_tbl_tramite FROM religiosos."ASOC_TramiteEstatus"
				WHERE i_id in (SELECT MAX (i_id) FROM religiosos."ASOC_TramiteEstatus" GROUP BY i_id_tbl_tramite) 
				order by i_id_tbl_tramite desc) as tramest on tbl_tramite.i_id = tramest.i_id_tbl_tramite
				
				LEFT JOIN  religiosos."CAT_Estatus" AS tbl_estatus ON tramest.i_id_tbl_estatus = tbl_estatus.i_id
				LEFT JOIN  religiosos."ASOC_Tramite_Fechas" AS tbl_fechas_asoc ON tbl_tramite.i_id= tbl_fechas_asoc.i_id_tbl_tramite
				LEFT JOIN  religiosos."TBL_Fechas" AS tbl_fechas ON tbl_fechas.i_id =  tbl_fechas_asoc.i_id_tbl_fechas
				 WHERE  tbl_fechas.i_id_cat_fechas=3  
				  AND tbl_asoc_tramite.i_id_tbl_usuariodictam = id_usuario 
				  AND ((denominacion_desc IS null) OR (LOWER(tbl_tramite.c_denominacion) LIKE ('%'||LOWER(denominacion_desc)||'%'))) 
				  AND ((numero_sgar IS null) OR (LOWER(tbl_tramite.c_nregistro) LIKE ('%'||LOWER(numero_sgar)||'%'))) 
		          AND ((estatus_desc IS null ) OR ( tbl_estatus.i_id=estatus_desc ))
				  AND ((credo_desc IS null ) OR (tbl_credo.i_id=credo_desc ))
				  AND tbl_estatus.i_id in (10,11,14,15,16,17,18,36, 19, 20);
			END IF;

END
$BODY$;