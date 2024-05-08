CREATE OR REPLACE FUNCTION religiosos.sp_consulta_lista_registros_tramite_i(
	id_usuario integer,
	numero_sgar character varying,
	denominacion_desc character varying,
	estatus_desc integer,
	credo_desc integer)
    RETURNS TABLE(id_registro integer, folio character varying, nombre character varying, denominacion character varying, fecha_registro date, fecha_autorizacion date, credo character varying, sol_registro character varying, id_estatus integer, estatus character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE fecha character varying;
BEGIN

IF estatus_desc= 19 OR estatus_desc=20 THEN
	RETURN QUERY
	SELECT 
	DISTINCT tblt.i_id AS id_registro,
	tblt.c_nfolio AS folio,
 	tblt.c_nregistro AS nombre, 
 	tblt.c_denominacion AS denominacion,
	--tblf.d_inicio AS fecha_registro,
	(SELECT tblf.d_inicio FROM religiosos."ASOC_Tramite_Fechas" asoctf 
	 	INNER JOIN religiosos."TBL_Fechas" AS tblf ON asoctf.i_id_tbl_fechas = tblf.i_id
	 WHERE asoctf.i_id_tbl_tramite = tblt.i_id and tblf.i_id_cat_fechas = 3 LIMIT 1) AS fecha_registro,
	(SELECT tblf.d_inicio FROM religiosos."ASOC_Tramite_Fechas" asoctf 
	 	INNER JOIN religiosos."TBL_Fechas" AS tblf ON asoctf.i_id_tbl_fechas = tblf.i_id
	 WHERE asoctf.i_id_tbl_tramite = tblt.i_id and tblf.i_id_cat_fechas = 4 LIMIT 1) AS fecha_autorizacion,
	catc.nombre AS credo,
	cattsr.c_nombre AS sol_registro,
	cats.i_id AS id_estatus,
 	cats.nombre AS estatus
FROM
	religiosos."TBL_Tramite" AS tblt 
	INNER JOIN religiosos."ASOC_Tramite_Usuario" AS asoctu ON tblt.i_id = asoctu.i_id_tbl_tramite
	LEFT JOIN religiosos."CAT_Credo" AS catc ON tblt.i_id_tbl_credo = catc.i_id
	LEFT JOIN religiosos."CAT_TSolReg" AS cattsr ON tblt.i_id_tbl_tsolreg = cattsr.i_id
	LEFT JOIN (SELECT i_id_tbl_estatus, i_id_tbl_tramite FROM religiosos."ASOC_TramiteEstatus"
				WHERE i_id in (SELECT MAX (i_id) FROM religiosos."ASOC_TramiteEstatus" GROUP BY i_id_tbl_tramite) 
				order by i_id_tbl_tramite desc) as tramest on tblt.i_id = tramest.i_id_tbl_tramite
	LEFT JOIN religiosos."CAT_Estatus" AS cats on tramest.i_id_tbl_estatus = cats.i_id
	LEFT JOIN religiosos."ASOC_Tramite_Fechas" AS asoctf on tblt.i_id = asoctf.i_id_tbl_tramite  
	LEFT JOIN religiosos."TBL_Fechas" AS tblf ON asoctf.i_id_tbl_fechas = tblf.i_id AND tblf.i_id_cat_fechas = 3
WHERE 
	cats.i_id in (19,20) 
	AND tblf.i_id_cat_fechas not in (1) 
	AND asoctu.i_id_tbl_usuario = id_usuario 
	AND ((denominacion_desc IS null) OR (LOWER(tblt.c_denominacion) LIKE ('%'||LOWER(denominacion_desc)||'%'))) 
	AND ((numero_sgar IS null) OR (LOWER(tblt.c_nfolio) LIKE ('%'||LOWER(numero_sgar)||'%'))) 
	AND ((estatus_desc IS null ) OR ( cats.i_id=estatus_desc ))
	AND ((credo_desc IS null ) OR (catc.i_id=credo_desc ))
ORDER BY tblt.i_id ASC;
ELSE 

RETURN QUERY
	SELECT 
	DISTINCT tblt.i_id AS id_registro,
	tblt.c_nfolio AS folio,
 	tblt.c_nregistro AS nombre, 
 	tblt.c_denominacion AS denominacion,
	(SELECT tblf.d_inicio FROM religiosos."ASOC_Tramite_Fechas" asoctf 
	 	INNER JOIN religiosos."TBL_Fechas" AS tblf ON asoctf.i_id_tbl_fechas = tblf.i_id
	 WHERE asoctf.i_id_tbl_tramite = tblt.i_id and tblf.i_id_cat_fechas = 3 LIMIT 1) AS fecha_registro,
	(SELECT tblf.d_inicio FROM religiosos."ASOC_Tramite_Fechas" asoctf 
	 	INNER JOIN religiosos."TBL_Fechas" AS tblf ON asoctf.i_id_tbl_fechas = tblf.i_id
	 WHERE asoctf.i_id_tbl_tramite = tblt.i_id and tblf.i_id_cat_fechas = 4 LIMIT 1) AS fecha_autorizacion,
	catc.nombre AS credo,
	cattsr.c_nombre AS sol_registro,
	cats.i_id AS id_estatus,
 	cats.nombre AS estatus
FROM
	religiosos."TBL_Tramite" AS tblt 
	INNER JOIN religiosos."ASOC_Tramite_Usuario" AS asoctu ON tblt.i_id = asoctu.i_id_tbl_tramite
	LEFT JOIN religiosos."CAT_Credo" AS catc ON tblt.i_id_tbl_credo = catc.i_id
	LEFT JOIN religiosos."CAT_TSolReg" AS cattsr ON tblt.i_id_tbl_tsolreg = cattsr.i_id
	LEFT JOIN (SELECT i_id_tbl_estatus, i_id_tbl_tramite FROM religiosos."ASOC_TramiteEstatus"
				WHERE i_id in (SELECT MAX (i_id) FROM religiosos."ASOC_TramiteEstatus" GROUP BY i_id_tbl_tramite) 
				order by i_id_tbl_tramite desc) as tramest on tblt.i_id = tramest.i_id_tbl_tramite
	LEFT JOIN religiosos."CAT_Estatus" AS cats on tramest.i_id_tbl_estatus = cats.i_id
	LEFT JOIN religiosos."ASOC_Tramite_Fechas" AS asoctf on tblt.i_id = asoctf.i_id_tbl_tramite  
	LEFT JOIN religiosos."TBL_Fechas" AS tblf ON asoctf.i_id_tbl_fechas = tblf.i_id --AND tblf.i_id_cat_fechas = 3
WHERE 
	cats.i_id in (1,2,3,4,5,6,7,8,9,10,11,14,15,16,17,18,36, 19, 20) 
	--AND tblf.i_id_cat_fechas not in (1)
	AND tblf.i_estatus NOT IN (2)
	AND asoctu.i_id_tbl_usuario = id_usuario 
	AND ((denominacion_desc IS null) OR (LOWER(tblt.c_denominacion) LIKE ('%'||LOWER(denominacion_desc)||'%'))) 
	AND ((numero_sgar IS null) OR (LOWER(tblt.c_nfolio) LIKE ('%'||LOWER(numero_sgar)||'%'))) 
	AND ((estatus_desc IS null ) OR ( cats.i_id=estatus_desc ))
	AND ((credo_desc IS null ) OR (catc.i_id=credo_desc ))
ORDER BY tblt.i_id ASC;
END IF;

END
$BODY$;
