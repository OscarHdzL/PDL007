
CREATE OR REPLACE FUNCTION religiosos.sp_consulta_detalle_cotejo_publico_new(
	s_id_us integer,
	id_tramite integer)
    RETURNS TABLE(s_id integer, s_tipo_cotejo integer, s_fecha character varying, s_comentarios character varying, s_cotejo_v boolean, s_direccion character varying, s_cotejo_f boolean, s_comentario_f character varying, s_cotejo_r boolean, s_comentario_r character varying, s_cotejo_c boolean, s_comentario_c character varying, s_doc1 boolean, s_doc2 boolean, usuario text, s_noficio_entrada character varying, s_noficio_salida character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
status integer:=(SELECT 
						asocte.i_id_tbl_estatus 
					FROM 
						religiosos."ASOC_TramiteEstatus" asocte
						INNER JOIN religiosos."ASOC_Tramite_Usuario" asoctu ON asoctu.i_id_tbl_tramite = asocte.i_id_tbl_tramite
					WHERE 
						asoctu.i_id_tbl_usuario = s_id_us and asoctu.i_id_tbl_tramite = id_tramite ORDER BY asocte.i_id desc 
					LIMIT 1);
BEGIN
IF(status in (3,4,5,6,7,8,14,15,16,17,18,19,20)) THEN
         RETURN QUERY SELECT
		 tt.i_id,
		 tt.i_id_tbl_cotejodoc,
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
		 JOIN religiosos."ASOC_TraDictaminador" astu ON astu.i_id_tbl_tramite = tt.i_id
		 JOIN religiosos."TBL_Usuario" tblu ON astu.i_id_tbl_usuariodictam = tblu.i_id
		 JOIN religiosos."TBL_Persona" tblp ON tblu.i_id_tbl_persona = tblp.i_id
		 WHERE 
		 (tt.i_id = id_tramite) limit 1;

ELSE
    RETURN;
END IF;
END;
$BODY$;
