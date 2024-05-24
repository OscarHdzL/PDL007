-- FUNCTION: religiosos.sp_consulta_registros_toma_nota_conteo(integer, character varying, integer, boolean)

-- DROP FUNCTION IF EXISTS religiosos.sp_consulta_registros_toma_nota_conteo(integer, character varying, integer, boolean);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_registros_toma_nota_conteo(
	id_asignador integer,
	keyword character varying,
	c_id_n integer,
	c_activo_n boolean)
    RETURNS TABLE(totalrecords bigint) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$

BEGIN
RETURN QUERY
SELECT 
	count (ids) as conteo
	FROM (SELECT tbltn.i_id AS ids
		FROM
			religiosos."ASOC_TramTNota" as asocttn INNER JOIN 
			religiosos."TBL_Tramite" as tblt ON asocttn.i_id_tbl_tramite = tblt.i_id INNER JOIN
			religiosos."TBL_TNota" as tbltn ON asocttn.i_id_tbl_tnota = tbltn.i_id INNER JOIN
			(SELECT i_id_tbl_fechas, i_id_tbl_tramite FROM religiosos."ASOC_Tramite_Fechas"
				WHERE i_id_tbl_fechas in (SELECT i_id FROM religiosos."TBL_Fechas" where i_id_cat_fechas = 3) 
				order by i_id_tbl_tramite desc) AS asoctf ON tblt.i_id = asoctf.i_id_tbl_tramite INNER JOIN 
			religiosos."TBL_Fechas" as tblf ON asoctf.i_id_tbl_fechas = tblf.i_id INNER JOIN 
			 (SELECT i_id_tbl_estatus, i_id_tbl_tramite FROM religiosos."ASOC_TramiteEstatus"
				WHERE i_id in (SELECT MAX (i_id) FROM religiosos."ASOC_TramiteEstatus" GROUP BY i_id_tbl_tramite) 
				order by i_id_tbl_tramite desc) as tramest on tblt.i_id = tramest.i_id_tbl_tramite INNER JOIN
			religiosos."CAT_Estatus" as cate ON  tramest.i_id_tbl_estatus = cate.i_id AND cate.i_id in (13) LEFT JOIN
			religiosos."ASOC_TnotaDictaminador" as asoctnd ON tbltn.i_id = asoctnd.i_id_tbl_tnota LEFT JOIN
			religiosos."TBL_Usuario" as tblu ON asoctnd.i_id_tbl_usuariodictam = tblu.i_id LEFT JOIN
			religiosos."CAT_TSol_Escrito" as cattse ON  tbltn.i_id_cat_tsol_escrito = cattse.i_id
			WHERE (keyword IS NULL OR tbltn.c_nueva_denominacion LIKE CONCAT('%',keyword,'%') 
						   OR tblt.c_nregistro LIKE keyword OR tbltn.c_nfolio LIKE keyword)
	UNION 
	SELECT tbltn.i_id AS ids
		FROM
			religiosos."ASOC_TramTNota" as asocttn INNER JOIN 
			religiosos."TBL_Tramite" as tblt ON asocttn.i_id_tbl_tramite = tblt.i_id INNER JOIN
			religiosos."TBL_TNota" as tbltn ON asocttn.i_id_tbl_tnota = tbltn.i_id INNER JOIN
			(SELECT i_id_tbl_fechas, i_id_tbl_tramite FROM religiosos."ASOC_Tramite_Fechas"
				WHERE i_id_tbl_fechas in (SELECT i_id FROM religiosos."TBL_Fechas" where i_id_cat_fechas = 3) 
				order by i_id_tbl_tramite desc) AS asoctf ON tblt.i_id = asoctf.i_id_tbl_tramite INNER JOIN 
			religiosos."TBL_Fechas" as tblf ON asoctf.i_id_tbl_fechas = tblf.i_id INNER JOIN 
			 (SELECT i_id_tbl_estatus, i_id_tbl_tramite FROM religiosos."ASOC_TramiteEstatus"
				WHERE i_id in (SELECT MAX (i_id) FROM religiosos."ASOC_TramiteEstatus" GROUP BY i_id_tbl_tramite) 
				order by i_id_tbl_tramite desc) as tramest on tblt.i_id = tramest.i_id_tbl_tramite INNER JOIN
			religiosos."CAT_Estatus" as cate ON  tramest.i_id_tbl_estatus = cate.i_id AND cate.i_id in (21,22,23,24,25,26,27,28,37) LEFT JOIN
			religiosos."ASOC_TnotaDictaminador" as asoctnd ON tbltn.i_id = asoctnd.i_id_tbl_tnota LEFT JOIN
			religiosos."TBL_Usuario" as tblu ON asoctnd.i_id_tbl_usuariodictam = tblu.i_id LEFT JOIN
			religiosos."CAT_TSol_Escrito" as cattse ON  tbltn.i_id_cat_tsol_escrito = cattse.i_id
			WHERE asoctnd.id_tbl_usuarioasigna = id_asignador
		    AND (keyword IS NULL OR tbltn.c_nueva_denominacion LIKE CONCAT('%',keyword,'%') 
				OR tblt.c_nregistro LIKE keyword OR tbltn.c_nfolio LIKE keyword)
	) AS tbl_total;

END;
$BODY$;

ALTER FUNCTION religiosos.sp_consulta_registros_toma_nota_conteo(integer, character varying, integer, boolean)
    OWNER TO tramitesdgardev_user;
