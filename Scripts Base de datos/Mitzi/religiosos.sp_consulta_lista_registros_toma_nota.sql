-- FUNCTION: religiosos.sp_consulta_lista_registros_toma_nota(character varying, integer, boolean, character varying, character varying, integer, integer)

-- DROP FUNCTION IF EXISTS religiosos.sp_consulta_lista_registros_toma_nota(character varying, integer, boolean, character varying, character varying, integer, integer);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_lista_registros_toma_nota(
	keyword character varying,
	c_id_n integer,
	c_activo_n boolean,
	d_column character varying,
	d_order character varying,
	d_start integer,
	d_length integer)
    RETURNS TABLE(reg_id integer, reg_idtn integer, reg_numero_folio character varying, reg_numero_registro character varying, reg_cat_denominacion character varying, reg_cat_n_denominacion character varying, reg_cat_solicitud_escrito character varying, reg_fecha character varying, reg_estatus character varying, es_id integer, correo_dic character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
RETURN QUERY

	SELECT s_id_tr, s_id_tn, s_tn_folio, s_numero_registro, s_cat_denominacion, s_cat_n_denominacion,  s_cat_solicitud_escrito, d_inicio
	, nombre, i_id, correo  FROM (
	SELECT tblt.i_id as s_id_tr, --1
	tbltn.i_id as s_id_tn, --2
	tbltn.c_nfolio as s_tn_folio, --3
	CAST(tblt.c_nregistro AS CHARACTER VARYING) as s_numero_registro,--4
	CAST(tblt.c_denominacion AS CHARACTER VARYING) as s_cat_denominacion, --5
	CAST(tbltn.c_nueva_denominacion AS CHARACTER VARYING) as s_cat_n_denominacion, --6
	CAST(cattse.nombre AS CHARACTER VARYING) as s_cat_solicitud_escrito, --7
	CAST((SELECT tblf.d_inicio FROM religiosos."ASOC_Tramite_Fechas" asoctf 
				INNER JOIN religiosos."TBL_Fechas" AS tblf ON asoctf.i_id_tbl_fechas = tblf.i_id
				WHERE asoctf.i_id_tbl_tramite = tblt.i_id and tblf.i_id_cat_fechas = 3 limit 1)AS CHARACTER VARYING)AS d_inicio, --8
	cate.nombre, --9
	cate.i_id, --10
	(CASE WHEN cate.i_id =13 THEN CAST('' AS CHARACTER VARYING) 
	ELSE CAST(CONVERT_FROM(DECODE(tblu.c_usuario, 'BASE64'), 'UTF-8') AS CHARACTER VARYING) END) AS correo--11
FROM religiosos."ASOC_TramTNota" as asocttn INNER JOIN --
	religiosos."TBL_Tramite" as tblt ON asocttn.i_id_tbl_tramite = tblt.i_id INNER JOIN --
	religiosos."TBL_TNota" as tbltn ON asocttn.i_id_tbl_tnota = tbltn.i_id INNER JOIN 

	 (SELECT i_id_tbl_estatus, i_id_tbl_tramite FROM religiosos."ASOC_TramiteEstatus"
		WHERE i_id in (SELECT MAX (i_id) FROM religiosos."ASOC_TramiteEstatus" GROUP BY i_id_tbl_tramite) 
		order by i_id_tbl_tramite desc) as tramest on tblt.i_id = tramest.i_id_tbl_tramite INNER JOIN

	religiosos."CAT_Estatus" as cate ON  tramest.i_id_tbl_estatus = cate.i_id AND cate.i_id in (13,21, 27, 28) LEFT JOIN
	religiosos."ASOC_TnotaDictaminador" as asoctnd ON tbltn.i_id = asoctnd.i_id_tbl_tnota LEFT JOIN
	religiosos."TBL_Usuario" as tblu ON asoctnd.i_id_tbl_usuariodictam = tblu.i_id LEFT JOIN
	religiosos."CAT_TSol_Escrito" as cattse ON  tbltn.i_id_cat_tsol_escrito = cattse.i_id
	WHERE (keyword IS NULL OR tblt.c_denominacion LIKE CONCAT('%',keyword,'%') 
	OR tblt.c_nregistro LIKE keyword OR tbltn.c_nfolio LIKE keyword)) AS main
	
	ORDER BY
				CASE WHEN d_column ='reg_cat_solicitud_escrito' and d_order ='asc' then main.s_cat_solicitud_escrito else '1' end ASC,
                CASE WHEN d_column ='reg_cat_solicitud_escrito' and d_order ='desc' then main.s_cat_solicitud_escrito else '1' end DESC,
                CASE WHEN d_column ='reg_cat_denominacion' and d_order ='asc' then main.s_cat_denominacion else '1' end ASC,
                CASE WHEN d_column ='reg_cat_denominacion' and d_order ='desc' then main.s_cat_denominacion else '1' end DESC,
				CASE WHEN d_column ='reg_numero_registro' and d_order ='asc' then main.s_numero_registro else '1' end ASC,
                CASE WHEN d_column ='reg_numero_registro' and d_order ='desc' then main.s_numero_registro else '1' end DESC,
				CASE WHEN d_column ='reg_numero_folio' and d_order ='asc' then main.s_tn_folio else '1' end ASC,
                CASE WHEN d_column ='reg_numero_folio' and d_order ='desc' then main.s_tn_folio else '1' end DESC,
				CASE WHEN d_column ='reg_fecha' and d_order ='asc' then CAST(main.d_inicio AS DATE) else CAST(NOW() AS DATE) end ASC,
                CASE WHEN d_column ='reg_fecha' and d_order ='desc' then CAST(main.d_inicio AS DATE) else CAST(NOW() AS DATE) end DESC
-- 				CASE WHEN d_column ='reg_estatus' and d_order ='asc' then est.nombre else '1' end ASC,
--                 CASE WHEN d_column ='reg_estatus' and d_order ='desc' then est.nombre else '1' end DESC
                limit d_length OFFSET d_start;
END
$BODY$;

ALTER FUNCTION religiosos.sp_consulta_lista_registros_toma_nota(character varying, integer, boolean, character varying, character varying, integer, integer)
    OWNER TO postgres;
