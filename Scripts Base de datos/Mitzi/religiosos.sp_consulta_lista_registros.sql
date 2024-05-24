-- FUNCTION: religiosos.sp_consulta_lista_registros(boolean)

-- DROP FUNCTION IF EXISTS religiosos.sp_consulta_lista_registros(boolean);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_lista_registros(
	c_activo_n boolean)
    RETURNS TABLE(reg_id integer, reg_cat_solicitud_escrito character varying, reg_cat_denominacion character varying, reg_numero_registro character varying, reg_pais_origen character varying, reg_fecha date, reg_estatus character varying, es_id integer, correo_dic character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$

BEGIN
        RETURN QUERY SELECT
         tt.i_id AS s_id,
         CAST(MAX(tsr.c_nombre) AS CHARACTER VARYING) AS s_cat_solicitud_escrito , 
         CAST(MAX(tt.c_denominacion)AS CHARACTER VARYING) AS s_cat_denominacion,
         CAST(MAX(tt.c_nfolio)AS CHARACTER VARYING) AS s_numero_registro, 
         CAST(MAX(po.nombre)AS CHARACTER VARYING) AS s_pais_origen,
         tf.d_inicio,
		 (SELECT nombre FROM religiosos."CAT_Estatus" WHERE i_id=MAX(es.i_id )),
		 Max(es.i_id),
		 CASE WHEN Max(es.i_id)=9 THEN CAST('' AS CHARACTER VARYING) 
		 ELSE CAST(MAX(CONVERT_FROM(DECODE(usdic.c_usuario, 'BASE64'), 'UTF-8')) AS CHARACTER VARYING) END
         FROM religiosos."TBL_Tramite" tt JOIN religiosos."CAT_TSolReg" tsr ON tsr.i_id = tt.i_id_tbl_tsolreg
		 JOIN religiosos."ASOC_TramiteEstatus" astes ON astes.i_id_tbl_tramite = tt.i_id
		 JOIN religiosos."CAT_Estatus" es ON es.i_id= astes.i_id_tbl_estatus
         JOIN religiosos."CAT_Paiso" po ON po.i_id = tt.i_id_tbl_paiso
         JOIN religiosos."ASOC_Tramite_Fechas" asoctf ON asoctf.i_id_tbl_tramite = tt.i_id
         JOIN religiosos."TBL_Fechas" tf ON tf.i_id = asoctf.i_id_tbl_fechas AND tf.i_id_cat_fechas = 3
		 LEFT JOIN religiosos."ASOC_TraDictaminador" astd ON astd.i_id_tbl_tramite =tt.i_id
		 LEFT JOIN religiosos."TBL_Usuario" usdic ON astd.i_id_tbl_usuariodictam =usdic.i_id
         WHERE tt.i_id_cat_ttramite=1 AND es.i_id = (SELECT i_id_tbl_estatus 
													 FROM religiosos."ASOC_TramiteEstatus"
													WHERE tt.i_id= i_id_tbl_tramite 
													 ORDER BY i_id DESC limit 1) 
													 --AND es.i_id in(9,10)
													 AND es.i_id in (9,10, 14, 15, 16, 17, 18, 19, 20, 36, 39)
          GROUP BY tt.i_id,tf.d_inicio;
	
 END;
$BODY$;

ALTER FUNCTION religiosos.sp_consulta_lista_registros(boolean)
    OWNER TO tramitesdgardev_user;
