-- FUNCTION: religiosos.sp_finalizar_toma_nota(integer, boolean, boolean, boolean, boolean, boolean, boolean, boolean)

-- DROP FUNCTION IF EXISTS religiosos.sp_finalizar_toma_nota(integer, boolean, boolean, boolean, boolean, boolean, boolean, boolean);

CREATE OR REPLACE FUNCTION religiosos.sp_finalizar_toma_nota(
	i_id_trtn integer,
	b_estatutos boolean,
	b_denominacion boolean,
	b_miembros boolean,
	b_representante boolean,
	b_apoderado boolean,
	b_dom_legal boolean,
	b_dom_notificacion boolean)
    RETURNS TABLE(id_tramite integer, mensaje character varying, proceso_existoso boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
tbl_tomanota_id integer;
t_folio_old character varying;
t_folio_new character varying;
existeFolio boolean;
status_tramite_id integer;
fecha_inicio integer := 0;

tramite constant integer:=(SELECT asttn.i_id_tbl_tramite FROM religiosos."ASOC_TramTNota" asttn 
								 WHERE   (asttn.i_id = i_id_trtn));
								 
tomanota constant integer:=(SELECT asttn.i_id_tbl_tnota FROM religiosos."ASOC_TramTNota" asttn 
								 WHERE   (asttn.i_id = i_id_trtn));
								 
BEGIN
	SELECT c_nfolio INTO t_folio_old FROM religiosos."TBL_TNota" WHERE c_nfolio is not null OR c_nfolio not in ('') ORDER BY c_nfolio DESC LIMIT 1;
	SELECT COALESCE((SELECT CASE WHEN c_nfolio is null THEN false ELSE true END),false) INTO existeFolio
	FROM religiosos."TBL_TNota" WHERE i_id = tomanota;
	
	INSERT INTO religiosos."TBL_Fechas"(d_inicio, i_estatus, i_id_cat_fechas) 
	VALUES (current_timestamp, 1, 3) RETURNING i_id INTO fecha_inicio;

	INSERT INTO religiosos."ASOC_Tramite_Fechas"(i_id_tbl_tramite, i_id_tbl_fechas)
	VALUES (tramite, fecha_inicio);
	
	SELECT
	CASE WHEN CAST(EXTRACT(YEAR FROM NOW()) AS 
				   CHARACTER VARYING) = CAST(SUBSTRING( t_folio_old, 7, 4) 
											 AS CHARACTER VARYING)
	THEN 
	   CAST(
		CONCAT(CAST(to_char(CAST(SUBSTRING( t_folio_old, 1, 5) AS INTEGER)+1, 'fm00000')
			 AS CHARACTER VARYING),'/',EXTRACT(YEAR FROM NOW()))
		AS CHARACTER VARYING)
	ELSE
		CAST( CONCAT('00001','/',EXTRACT(YEAR FROM NOW())) AS CHARACTER VARYING)
		END INTO t_folio_new;
		
	IF(existeFolio = false) THEN
		UPDATE religiosos."TBL_TNota"
		SET c_nfolio = (SELECT folio from religiosos.obtener_folio_nota())--t_folio_new
		WHERE i_id = tomanota;
	END IF;
	
	SELECT i_id_tbl_estatus INTO status_tramite_id
 	FROM religiosos."ASOC_TramiteEstatus" where i_id_tbl_tramite = tramite AND 
 		i_id_tbl_estatus in (23,25) ORDER BY i_id desc limit 1;
					
	IF status_tramite_id = 23 or status_tramite_id = 25
	THEN
		INSERT INTO religiosos."ASOC_TramiteEstatus"(
					i_id_tbl_tramite, i_id_tbl_estatus)
					VALUES (tramite, 37); 
	ELSE
		DELETE FROM religiosos."ASOC_TramiteEstatus" 
		WHERE i_id_tbl_tramite= tramite AND
		i_id_tbl_estatus =13;
		INSERT INTO religiosos."ASOC_TramiteEstatus"(
					i_id_tbl_tramite, i_id_tbl_estatus)
					VALUES ( tramite, 13); 
	END IF;
				
	DELETE FROM religiosos."ASOC_TNotaMovimientos" 
	WHERE i_id_tbl_tnota = tomanota;
	
	IF(b_estatutos) THEN
		INSERT INTO religiosos."ASOC_TNotaMovimientos"(i_id_tbl_tnota,i_id_cat_tnotamovimientos)
		VALUES(tomanota,1);
	END IF;
	
	IF(b_denominacion) THEN
		INSERT INTO religiosos."ASOC_TNotaMovimientos"(i_id_tbl_tnota,i_id_cat_tnotamovimientos)
		VALUES(tomanota,2);
	END IF;
	
	IF(b_miembros) THEN
		INSERT INTO religiosos."ASOC_TNotaMovimientos"(i_id_tbl_tnota,i_id_cat_tnotamovimientos)
		VALUES(tomanota,3);
	END IF;
	
	IF(b_representante) THEN
		INSERT INTO religiosos."ASOC_TNotaMovimientos"(i_id_tbl_tnota,i_id_cat_tnotamovimientos)
		VALUES(tomanota,4);
	END IF;
	
	IF(b_apoderado) THEN
		INSERT INTO religiosos."ASOC_TNotaMovimientos"(i_id_tbl_tnota,i_id_cat_tnotamovimientos)
		VALUES(tomanota,5);
	END IF;
	
	IF(b_dom_legal) THEN
		INSERT INTO religiosos."ASOC_TNotaMovimientos"(i_id_tbl_tnota,i_id_cat_tnotamovimientos)
		VALUES(tomanota,6);
	END IF;
	
	IF(b_dom_notificacion) THEN
		INSERT INTO religiosos."ASOC_TNotaMovimientos"(i_id_tbl_tnota,i_id_cat_tnotamovimientos)
		VALUES(tomanota,7);
	END IF;
	
	

	RETURN QUERY SELECT
	 0 as id_tramite,
	 CAST('Su solicitud de toma de nota se ha llevado a cabo de
	  forma exitosa.

		  En breve le llegarán notificaciones a su correo electrónico para su
		  seguimiento.' as varchar) AS mensaje,
	 (true) AS proceso_existoso; 
END;
$BODY$;

ALTER FUNCTION religiosos.sp_finalizar_toma_nota(integer, boolean, boolean, boolean, boolean, boolean, boolean, boolean)
    OWNER TO tramitesdgardev_user;
