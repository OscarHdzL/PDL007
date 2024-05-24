-- FUNCTION: religiosos.sp_consulta_detalle_toma_nota_cotejo_publico_new(integer, integer)

-- DROP FUNCTION IF EXISTS religiosos.sp_consulta_detalle_toma_nota_cotejo_publico_new(integer, integer);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_detalle_toma_nota_cotejo_publico_new(
	s_id_us integer,
	id_tramite integer)
    RETURNS TABLE(s_id integer, s_tipo_cotejo integer, s_fecha character varying, s_comentarios character varying, s_cotejo_v boolean, s_direccion character varying, s_cotejo_f boolean, s_comentario_f character varying, s_cotejo_r boolean, s_comentario_r character varying, s_cotejo_c boolean, s_comentario_c character varying, s_doc1 boolean, s_doc2 boolean, usuario text, s_noficio_entrada character varying, s_noficio_salida character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
c_id integer;
status integer;
c_tomanota integer;
BEGIN
SELECT 
i_id_tbl_estatus,tt.i_id INTO  
status, c_id
FROM 
religiosos."TBL_Tramite" tt JOIN 
religiosos."ASOC_TramiteEstatus" astes ON astes.i_id_tbl_tramite = tt.i_id JOIN 
religiosos."ASOC_Tramite_Usuario" astu ON astu.i_id_tbl_tramite = tt.i_id
WHERE (astu.i_id_tbl_usuario = s_id_us and tt.i_id = id_tramite) ORDER BY astes.i_id DESC limit 1;

SELECT 
asttn.i_id_tbl_tnota INTO 
c_tomanota 
FROM 
religiosos."TBL_Tramite" tt JOIN 
religiosos."ASOC_TramiteEstatus" astes ON astes.i_id_tbl_tramite = tt.i_id JOIN 
religiosos."ASOC_Tramite_Usuario" astu ON astu.i_id_tbl_tramite = tt.i_id JOIN
religiosos."ASOC_TramTNota" asttn ON asttn.i_id_tbl_tramite = tt.i_id
WHERE (asttn.i_id_tbl_tramite = id_tramite) ORDER BY asttn.i_id DESC limit 1;

IF(status in (22,23,24,25,26,27,28)) THEN
         RETURN QUERY SELECT
		 tn.i_id,
		 tc.i_idcotejodoc,
		 CAST(astu.d_fecha_cotejo AS CHARACTER VARYING), 
		 astu.c_comentario, 
		 astu.b_cumple, 
		 astu.c_indicacionescot,
		 astu.b_cumple_cot_doc,
		 astu.c_comentario_cot_doc, 
		 astu.b_cumple_reg_realizado, 
		 astu.c_coment_reg_realizado, 
		 astu.b_estatus_reg_concluido, 
		 astu.c_coment_reg_concluido, 
		 false,
		 false,
 		 CONVERT_FROM(DECODE(tblp.c_nombre, 'BASE64'), 'UTF-8') ||' '|| CONVERT_FROM(DECODE(tblp.c_apaterno, 'BASE64'), 'UTF-8')  AS Usuario,
		 astu.c_noficio_entrada,
		 astu.c_noficio_salida
		 FROM religiosos."TBL_Tramite" tt 
		 JOIN religiosos."ASOC_TramTNota" asttn ON asttn.i_id_tbl_tramite = tt.i_id
		 JOIN religiosos."TBL_TNota" tn on asttn.i_id_tbl_tnota = tn.i_id
		 JOIN religiosos."TBL_Cotejo" tc on tc.i_id = tn.i_id_cat_cotejodoc_est
		 JOIN religiosos."ASOC_TnotaDictaminador" astu ON astu.i_id_tbl_tnota = tn.i_id
		 JOIN religiosos."TBL_Usuario" tblu ON astu.i_id_tbl_usuariodictam = tblu.i_id
		 JOIN religiosos."TBL_Persona" tblp ON tblu.i_id_tbl_persona = tblp.i_id
		 WHERE 
		 (tn.i_id = c_tomanota); --limit 1;

ELSE
    RETURN;
END IF;
END;
$BODY$;

ALTER FUNCTION religiosos.sp_consulta_detalle_toma_nota_cotejo_publico_new(integer, integer)
    OWNER TO postgres;
