-- FUNCTION: religiosos.sp_actualizar_atender_cotejo_toma_nota(integer, integer, integer, integer, character varying, timestamp with time zone, character varying, character varying, character varying)

-- DROP FUNCTION IF EXISTS religiosos.sp_actualizar_atender_cotejo_toma_nota(integer, integer, integer, integer, character varying, timestamp with time zone, character varying, character varying, character varying);

CREATE OR REPLACE FUNCTION religiosos.sp_actualizar_atender_cotejo_toma_nota(
	cotejo_tipo integer,
	s_id_us integer,
	s_id integer,
	s_estatus integer,
	s_direccion character varying,
	s_fecha timestamp with time zone,
	s_comentarios character varying,
	s_noficio_entrada character varying,
	s_noficio_salida character varying)
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
tramite integer;
fecha_inicio integer := 0;
   
BEGIN  
SELECT astes.i_id_tbl_estatus,tn.i_id,astu.i_id,asoc_us.i_id_tbl_usuario,tt.i_id
INTO   status,tbl_tramite_id,tbl_cotejo_id,id_us_notificacion,tramite
FROM religiosos."TBL_Tramite" as tt JOIN 
religiosos."ASOC_TramTNota" as asttn ON asttn.i_id_tbl_tramite = tt.i_id JOIN 
religiosos."TBL_TNota"	as tn ON asttn.i_id_tbl_tnota = tn.i_id JOIN 
religiosos."ASOC_TramiteEstatus" as astes ON astes.i_id_tbl_tramite = tt.i_id JOIN 
religiosos."ASOC_TnotaDictaminador" as astu ON astu.i_id_tbl_tnota = tn.i_id JOIN 
religiosos."ASOC_Tramite_Usuario" as asoc_us ON asoc_us.i_id_tbl_tramite = tt.i_id
WHERE   (tn.i_id = s_id) 
AND (astu.i_id_tbl_usuariodictam = s_id_us) ORDER BY astes.i_id DESC limit 1;
IF(status in (21,22,23,37) AND cotejo_tipo = 9) THEN
	UPDATE religiosos."ASOC_TnotaDictaminador"
	SET c_comentario=s_comentarios,
	b_cumple=CASE WHEN s_estatus=22 THEN TRUE ELSE FALSE END,
	d_fecha_cotejo= s_fecha,
	c_indicacionescot=s_direccion
	WHERE  i_id=tbl_cotejo_id;
	
	DELETE FROM religiosos."ASOC_TramiteEstatus"
	WHERE i_id_tbl_tramite= tramite 
	AND i_id_tbl_estatus =s_estatus;
    INSERT INTO religiosos."ASOC_TramiteEstatus"(
								i_id_tbl_tramite, i_id_tbl_estatus)
								VALUES ( tramite,s_estatus); 
      RETURN QUERY SELECT
	  id_us_notificacion as id_tramite,
	  CAST('La notificación ha sido enviada de forma exitosa.' as varchar) AS mensaje,
	  (true) AS proceso_existoso;
	  
ELSEIF(status in (22,24,25,37) AND cotejo_tipo = 10) THEN

    UPDATE religiosos."ASOC_TnotaDictaminador"
	SET b_cumple_cot_doc=CASE WHEN s_estatus=24 THEN TRUE ELSE FALSE END, 
	c_comentario_cot_doc=s_comentarios
	WHERE i_id=tbl_cotejo_id;
      
      DELETE FROM religiosos."ASOC_TramiteEstatus"
	  WHERE i_id_tbl_tramite= tramite 
	   AND i_id_tbl_estatus =s_estatus;
       INSERT INTO religiosos."ASOC_TramiteEstatus"(
								i_id_tbl_tramite, i_id_tbl_estatus)
								VALUES ( tramite,s_estatus); 
      RETURN QUERY SELECT
	  id_us_notificacion as id_tramite,
	  CAST('La notificación ha sido enviada de forma exitosa.' as varchar) AS mensaje,
	  (true) AS proceso_existoso;
ELSEIF(status in (24,25,26,37) AND cotejo_tipo = 11) THEN

      UPDATE religiosos."ASOC_TnotaDictaminador"
	     SET b_cumple_reg_realizado=CASE WHEN s_estatus=26 THEN TRUE ELSE FALSE END, 
	     c_coment_reg_realizado=s_comentarios,
		 c_noficio_entrada = s_noficio_entrada,
		 c_noficio_salida = s_noficio_salida
	     WHERE i_id=tbl_cotejo_id;
		 
       DELETE FROM religiosos."ASOC_TramiteEstatus"
	   WHERE i_id_tbl_tramite= tramite 
	   AND i_id_tbl_estatus =s_estatus;
       INSERT INTO religiosos."ASOC_TramiteEstatus"(
								i_id_tbl_tramite, i_id_tbl_estatus)
								VALUES ( tramite,s_estatus); 
								
	  INSERT INTO religiosos."TBL_Fechas" (d_inicio, i_estatus, i_id_cat_fechas) 
		Values (current_timestamp, 1, 5) RETURNING i_id INTO fecha_inicio;
		
		INSERT INTO religiosos."ASOC_Tramite_Fechas"(i_id_tbl_tramite, i_id_tbl_fechas)
		VALUES (tramite, fecha_inicio);
	
      RETURN QUERY SELECT
	  id_us_notificacion as id_tramite,
	  CAST('La notificación ha sido enviada de forma exitosa.' as varchar) AS mensaje,
	  (true) AS proceso_existoso;
	  
ELSEIF(status in (25,26,27,28) AND cotejo_tipo = 12) THEN

      UPDATE religiosos."ASOC_TnotaDictaminador"
	  SET b_estatus_reg_concluido=CASE WHEN s_estatus=27 THEN TRUE ELSE FALSE END, 
	  c_coment_reg_concluido=s_comentarios
	  WHERE i_id=tbl_cotejo_id;
		 
       DELETE FROM religiosos."ASOC_TramiteEstatus"
	   WHERE i_id_tbl_tramite= tramite 
	   AND i_id_tbl_estatus =s_estatus;
       INSERT INTO religiosos."ASOC_TramiteEstatus"(
								i_id_tbl_tramite, i_id_tbl_estatus)
								VALUES ( tramite,s_estatus); 
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

ALTER FUNCTION religiosos.sp_actualizar_atender_cotejo_toma_nota(integer, integer, integer, integer, character varying, timestamp with time zone, character varying, character varying, character varying)
    OWNER TO tramitesdgardev_user;
