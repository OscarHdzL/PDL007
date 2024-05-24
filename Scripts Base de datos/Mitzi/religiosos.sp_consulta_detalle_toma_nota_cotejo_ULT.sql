-- FUNCTION: religiosos.sp_consulta_detalle_toma_nota_cotejo(integer, integer, integer)

-- DROP FUNCTION IF EXISTS religiosos.sp_consulta_detalle_toma_nota_cotejo(integer, integer, integer);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_detalle_toma_nota_cotejo(
	cotejo_tipo integer,
	s_id_us integer,
	c_id integer)
    RETURNS TABLE(s_id integer, s_tipo_cotejo integer, s_estatus integer, s_fecha character varying, s_cumple boolean, s_comentarios character varying, s_direccion character varying, s_numero_registro character varying, s_doc1 boolean, s_doc2 boolean, s_noficio_entrada character varying, s_noficio_salida character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
tbl_tramite_id integer;
status integer;
tbl_cotejo_id integer;

      
BEGIN  
SELECT astes.i_id_tbl_estatus, tn.i_id, astu.i_id INTO status,tbl_tramite_id,tbl_cotejo_id FROM religiosos."TBL_Tramite" tt
  								 JOIN religiosos."ASOC_TramTNota" asttn ON asttn.i_id_tbl_tramite = tt.i_id
								 JOIN religiosos."TBL_TNota" as tn ON tn.i_id = asttn.i_id_tbl_tnota
								 JOIN religiosos."ASOC_TramiteEstatus" astes ON astes.i_id_tbl_tramite = tt.i_id
								 JOIN religiosos."ASOC_TnotaDictaminador" astu ON astu.i_id_tbl_tnota = tn.i_id
								 WHERE   (tn.i_id = c_id) 
								 AND (astu.i_id_tbl_usuariodictam = s_id_us) ORDER BY astes.i_id DESC limit 1;
IF(status in (13,21,22,23,24,25,26,37) AND cotejo_tipo = 9) THEN --24,25,26,

 RETURN QUERY SELECT
		 tn.i_id,
		 tcot.i_idcotejodoc,
		 status,
		 CAST(astu.d_fecha_cotejo AS CHARACTER VARYING), 
		 astu.b_cumple, 
		 astu.c_comentario, 
		 astu.c_indicacionescot,
		 tt.c_nregistro, 
		 false,
		 false,
		 astu.c_noficio_entrada,
		 astu.c_noficio_salida
		 FROM religiosos."TBL_TNota" tn 
		 JOIN religiosos."ASOC_TramTNota" asttn ON asttn.i_id_tbl_tnota = tn.i_id
		 JOIN religiosos."TBL_Tramite" tt ON tt.i_id = asttn.i_id_tbl_tramite
		 JOIN religiosos."ASOC_TnotaDictaminador" astu ON astu.i_id_tbl_tnota = tn.i_id
		 JOIN religiosos."TBL_Cotejo" as tcot ON tcot.i_id = tn.i_id_cat_cotejodoc_est
		 
		 
		 WHERE 
		 (tn.i_id = tbl_tramite_id) limit 1;

ELSEIF(status in (22,24,25,26,37) AND cotejo_tipo = 10) THEN --26,
 RETURN QUERY SELECT
		 tn.i_id,
		 tcot.i_idcotejodoc,
		 status,
		 CAST(astu.d_fecha_cotejo AS CHARACTER VARYING), 
		 astu.b_cumple_cot_doc,
		 astu.c_comentario_cot_doc, 
		 astu.c_indicacionescot,
		 tt.c_nregistro, 
		 false,
		 false,
		 astu.c_noficio_entrada,
		 astu.c_noficio_salida
		 FROM religiosos."TBL_TNota" tn 
		 JOIN religiosos."ASOC_TramTNota" asttn ON asttn.i_id_tbl_tnota = tn.i_id
		 JOIN religiosos."TBL_Tramite" tt ON tt.i_id = asttn.i_id_tbl_tramite
		 JOIN religiosos."ASOC_TnotaDictaminador" astu ON astu.i_id_tbl_tnota = tn.i_id
		 JOIN religiosos."TBL_Cotejo" as tcot ON tcot.i_id = tn.i_id_cat_cotejodoc_est
		 WHERE 
		 (tn.i_id = tbl_tramite_id) limit 1;
ELSEIF(status in (24,25,26) AND cotejo_tipo = 11) THEN--37
 RETURN QUERY SELECT
		 tn.i_id,
		 tcot.i_idcotejodoc,
		 status,
		 CAST(astu.d_fecha_cotejo AS CHARACTER VARYING), 
		 astu.b_cumple_reg_realizado, 
		 astu.c_coment_reg_realizado,  
		 astu.c_indicacionescot,
		 tt.c_nregistro, 
		 COALESCE((SELECT CASE WHEN fa.c_nombre is null THEN false
		  ELSE true END
		  FROM religiosos."ASOC_TNotaArchivo" asta 
		  LEFT JOIN religiosos."TBL_Archivo" fa ON fa.i_id = asta.i_id_tbl_archivo
		  WHERE asta.i_id_tbl_tnota = tn.i_id AND i_estatus =1 
		  AND fa.i_id_tbl_archtram = 18 limit 1),false),
		 false,
		 astu.c_noficio_entrada,
		 astu.c_noficio_salida
		 FROM religiosos."TBL_TNota" tn 
		 JOIN religiosos."ASOC_TramTNota" asttn ON asttn.i_id_tbl_tnota = tn.i_id
		 JOIN religiosos."TBL_Tramite" tt ON tt.i_id = asttn.i_id_tbl_tramite
		 JOIN religiosos."ASOC_TnotaDictaminador" astu ON astu.i_id_tbl_tnota = tn.i_id
		 JOIN religiosos."TBL_Cotejo" as tcot ON tcot.i_id = tn.i_id_cat_cotejodoc_est
		 WHERE 
		 (tn.i_id = tbl_tramite_id) limit 1;
  
	  
ELSEIF(status in (25,26,27,28) AND cotejo_tipo = 12) THEN
 RETURN QUERY SELECT
		 tn.i_id,
		 tcot.i_idcotejodoc,
		 status,
		 CAST(astu.d_fecha_cotejo AS CHARACTER VARYING), 
		 astu.b_estatus_reg_concluido, 
		 astu.c_coment_reg_concluido, 
		 astu.c_indicacionescot,
		 tt.c_nregistro, 
		 false,
		 false,
		 astu.c_noficio_entrada,
		 astu.c_noficio_salida
		 FROM religiosos."TBL_TNota" tn 
		 JOIN religiosos."ASOC_TramTNota" asttn ON asttn.i_id_tbl_tnota = tn.i_id
		 JOIN religiosos."TBL_Tramite" tt ON tt.i_id = asttn.i_id_tbl_tramite
		 JOIN religiosos."ASOC_TnotaDictaminador" astu ON astu.i_id_tbl_tnota = tn.i_id
		 JOIN religiosos."TBL_Cotejo" as tcot ON tcot.i_id = tn.i_id_cat_cotejodoc_est
		 WHERE 
		 (tn.i_id = tbl_tramite_id) limit 1;
ELSE
    RETURN;
END IF;

END;
$BODY$;

ALTER FUNCTION religiosos.sp_consulta_detalle_toma_nota_cotejo(integer, integer, integer)
    OWNER TO tramitesdgardev_user;
