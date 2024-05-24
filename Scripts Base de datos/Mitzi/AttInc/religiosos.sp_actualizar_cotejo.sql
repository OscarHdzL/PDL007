-- FUNCTION: religiosos.sp_actualizar_cotejo(integer, integer, integer, integer, character varying, timestamp without time zone, character varying, character varying)

-- DROP FUNCTION IF EXISTS religiosos.sp_actualizar_cotejo(integer, integer, integer, integer, character varying, timestamp without time zone, character varying, character varying);

CREATE OR REPLACE FUNCTION religiosos.sp_actualizar_cotejo(
	cotejo_tipo integer,
	s_id_us integer,
	s_id integer,
	s_estatus integer,
	s_direccion character varying,
	s_fecha timestamp without time zone,
	s_comentarios character varying,
	s_numero_registro character varying)
    RETURNS TABLE(id_gen integer, mensaje character varying, proceso_existoso boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
tbl_tramite_id integer;
status integer;
tbl_cotejo_id integer;
id_us_notificacion integer;
fecha_inicio integer := 0;

      
BEGIN  

SELECT astes.i_id_tbl_estatus, tt.i_id, astu.i_id,asoc_us.i_id_tbl_usuario INTO status,tbl_tramite_id,tbl_cotejo_id,id_us_notificacion FROM religiosos."TBL_Tramite" tt 
								 JOIN religiosos."ASOC_TramiteEstatus" astes ON astes.i_id_tbl_tramite = tt.i_id
								 JOIN religiosos."ASOC_TraDictaminador" astu ON astu.i_id_tbl_tramite = tt.i_id
								 JOIN religiosos."ASOC_Tramite_Usuario" asoc_us ON asoc_us.i_id_tbl_tramite = tt.i_id
								 WHERE   (tt.i_id = s_id) 
								 AND (astu.i_id_tbl_usuariodictam = s_id_us) ORDER BY astes.i_id DESC limit 1;
IF(status in (10,11,14,15,36) AND cotejo_tipo = 7) THEN
	UPDATE religiosos."ASOC_TraDictaminador"
	SET c_comentario=s_comentarios,
	b_cumple=CASE WHEN s_estatus=14 THEN TRUE ELSE FALSE END,
	d_fecha_cotejo= s_fecha,
	c_indicacionescot=s_direccion,
	b_read = false
	WHERE  i_id=tbl_cotejo_id;
	
	DELETE FROM religiosos."ASOC_TramiteEstatus"
	WHERE i_id_tbl_tramite= tbl_tramite_id 
	AND i_id_tbl_estatus =s_estatus;
    INSERT INTO religiosos."ASOC_TramiteEstatus"(
								i_id_tbl_tramite, i_id_tbl_estatus)
								VALUES ( tbl_tramite_id,s_estatus); 
      IF s_estatus=14 THEN
     DELETE FROM  religiosos."ASOC_TramiteEstatus" WHERE i_id_tbl_tramite=tbl_tramite_id
	 AND i_id_tbl_estatus=15;
	 END IF;
	 
	  IF s_estatus=16 THEN
     DELETE FROM  religiosos."ASOC_TramiteEstatus" WHERE i_id_tbl_tramite=tbl_tramite_id
	 AND i_id_tbl_estatus=17;
	 END IF;
   
	  RETURN QUERY SELECT
	  id_us_notificacion as id_tramite,
	  CAST('La notificación ha sido enviada de forma exitosa.' as varchar) AS mensaje,
	  (true) AS proceso_existoso;
	  
ELSEIF(status in (14,16,17,36) AND cotejo_tipo = 8) THEN

    UPDATE religiosos."ASOC_TraDictaminador"
	SET b_cumple_cot_doc=CASE WHEN s_estatus=16 THEN TRUE ELSE FALSE END, 
	c_comentario_cot_doc=s_comentarios,
	b_read = false
	WHERE i_id=tbl_cotejo_id;
      
      DELETE FROM religiosos."ASOC_TramiteEstatus"
	  WHERE i_id_tbl_tramite= tbl_tramite_id 
	   AND i_id_tbl_estatus =s_estatus;
       INSERT INTO religiosos."ASOC_TramiteEstatus"(
								i_id_tbl_tramite, i_id_tbl_estatus)
								VALUES ( tbl_tramite_id,s_estatus); 
     
	 IF s_estatus=14 THEN
     DELETE FROM  religiosos."ASOC_TramiteEstatus" WHERE i_id_tbl_tramite=tbl_tramite_id
	 AND i_id_tbl_estatus=15;
	 END IF;
	 
	   IF s_estatus=16 THEN
     DELETE FROM  religiosos."ASOC_TramiteEstatus" WHERE i_id_tbl_tramite=tbl_tramite_id
	 AND i_id_tbl_estatus=17;
	 END IF;
      RETURN QUERY SELECT
	  id_us_notificacion as id_tramite,
	  CAST('La notificación ha sido enviada de forma exitosa.' as varchar) AS mensaje,
	  (true) AS proceso_existoso;
ELSEIF(status in (16,17,18,36) AND cotejo_tipo = 9) THEN

      UPDATE religiosos."ASOC_TraDictaminador"
	     SET b_cumple_reg_realizado=CASE WHEN s_estatus=18 THEN TRUE ELSE FALSE END, 
	     c_coment_reg_realizado=s_comentarios,
		 b_read = false
	     WHERE i_id=tbl_cotejo_id;
		 
       DELETE FROM religiosos."ASOC_TramiteEstatus"
	   WHERE i_id_tbl_tramite= tbl_tramite_id 
	   AND i_id_tbl_estatus =s_estatus;
	   
       INSERT INTO religiosos."ASOC_TramiteEstatus"(
								i_id_tbl_tramite, i_id_tbl_estatus)
								VALUES ( tbl_tramite_id,s_estatus); 
								
		UPDATE religiosos."TBL_Tramite" SET c_nregistro = s_numero_registro
	  WHERE i_id = tbl_tramite_id;
	  
	  INSERT INTO religiosos."TBL_Fechas" (d_inicio, i_estatus, i_id_cat_fechas) 
		Values (current_timestamp, 1, 4) RETURNING i_id INTO fecha_inicio;
		
		INSERT INTO religiosos."ASOC_Tramite_Fechas"(i_id_tbl_tramite, i_id_tbl_fechas)
		VALUES (tbl_tramite_id, fecha_inicio);
	   
	   IF s_estatus=14 THEN
     DELETE FROM  religiosos."ASOC_TramiteEstatus" WHERE i_id_tbl_tramite=tbl_tramite_id
	 AND i_id_tbl_estatus=15;
	 
	   IF s_estatus=16 THEN
     DELETE FROM  religiosos."ASOC_TramiteEstatus" WHERE i_id_tbl_tramite=tbl_tramite_id
	 AND i_id_tbl_estatus=17;
	 END IF;
	 
	 END IF;
      RETURN QUERY SELECT
	  id_us_notificacion as id_tramite,
	  CAST('La notificación ha sido enviada de forma exitosa.' as varchar) AS mensaje,
	  (true) AS proceso_existoso;
	  
ELSEIF(status in (17,18,19,20) AND cotejo_tipo = 10) THEN

      UPDATE religiosos."ASOC_TraDictaminador"
	  SET b_estatus_reg_concluido=CASE WHEN s_estatus=19 THEN TRUE ELSE FALSE END, 
	  c_coment_reg_concluido=s_comentarios,
	  b_read = false
	  WHERE i_id=tbl_cotejo_id;
		 
       DELETE FROM religiosos."ASOC_TramiteEstatus"
	   WHERE i_id_tbl_tramite= tbl_tramite_id 
	   AND i_id_tbl_estatus =s_estatus;
       INSERT INTO religiosos."ASOC_TramiteEstatus"(
								i_id_tbl_tramite, i_id_tbl_estatus)
								VALUES ( tbl_tramite_id,s_estatus); 
		
     IF s_estatus=14 THEN
     DELETE FROM  religiosos."ASOC_TramiteEstatus" WHERE i_id_tbl_tramite=tbl_tramite_id
	 AND i_id_tbl_estatus=15;
	 
	   IF s_estatus=16 THEN
     DELETE FROM  religiosos."ASOC_TramiteEstatus" WHERE i_id_tbl_tramite=tbl_tramite_id
	 AND i_id_tbl_estatus=17;
	 END IF;
	 
	 END IF;
      RETURN QUERY SELECT
	  id_us_notificacion as id_tramite,
	  CAST('La notificación ha sido enviada de forma exitosa.' as varchar) AS mensaje,
	  (true) AS proceso_existoso;
	  
ELSE
      RETURN QUERY SELECT
	  0 as id_tramite,
	  CAST('La notificación no ha sido enviada de forma exitosa.' as varchar) AS mensaje,
	  (false) AS proceso_existoso;
    RETURN;
END IF;

END;
$BODY$;

ALTER FUNCTION religiosos.sp_actualizar_cotejo(integer, integer, integer, integer, character varying, timestamp without time zone, character varying, character varying)
    OWNER TO tramitesdgardev_user;
