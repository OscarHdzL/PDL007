 DROP FUNCTION IF EXISTS religiosos.sp_finalizar_tramite(integer, integer);

CREATE OR REPLACE FUNCTION religiosos.sp_finalizar_tramite(
	s_id_us integer,
	s_id integer)
    RETURNS TABLE(id_tramite integer, mensaje character varying, proceso_existoso boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
tbl_tramite_id integer;
status_tramite_id integer;

t_folio_old character varying;
t_folio_new character varying;

tbl_fecha_apertura_id integer;

BEGIN

--GENERAR FOLIO
	SELECT c_nfolio INTO t_folio_old FROM religiosos."TBL_Tramite" WHERE c_nfolio not in ('') ORDER BY i_id DESC Limit 1;
	
	IF (t_folio_old = '') THEN
		t_folio_old:= CAST(CONCAT('00000','/',EXTRACT(YEAR FROM NOW())) AS CHARACTER VARYING);
	END IF;
			
	SELECT
		CASE 
			WHEN CAST(EXTRACT(YEAR FROM NOW()) AS CHARACTER VARYING) = CAST(SUBSTRING(t_folio_old, 7, 4) AS CHARACTER VARYING)
		THEN 
			CAST(CONCAT(CAST(to_char(CAST(SUBSTRING( t_folio_old, 1, 5) AS INTEGER)+1, 'fm00000') AS CHARACTER VARYING),'/',EXTRACT(YEAR FROM NOW())) AS CHARACTER VARYING)
		ELSE
		    CAST(CONCAT('00001','/',EXTRACT(YEAR FROM NOW())) AS CHARACTER VARYING)
		END INTO t_folio_new;
--FIN			

	SELECT tt.i_id INTO tbl_tramite_id
	FROM religiosos."TBL_Tramite" tt
	JOIN religiosos."ASOC_Tramite_Usuario" astu ON astu.i_id_tbl_tramite = tt.i_id
    JOIN religiosos."ASOC_TramiteEstatus" astes ON astes.i_id_tbl_tramite = tt.i_id
	AND (astes.i_id_tbl_estatus = 7 or astes.i_id_tbl_estatus = 8 or astes.i_id_tbl_estatus = 9)
	WHERE  (tt.i_id = s_id or s_id is null) 
	AND (astu.i_id_tbl_usuario = s_id_us or s_id_us is null) ORDER BY astes.i_id_tbl_estatus limit 1; 
		    
	IF(tbl_tramite_id is null or tbl_tramite_id <1) THEN
		RETURN QUERY SELECT
			0 as id_tramite,
			CAST('Por favor, ingrese toda la Información requerida para concluir su registro.' as varchar) AS mensaje,
			(false) AS proceso_existoso;

	ELSE 
		SELECT i_id_tbl_estatus INTO status_tramite_id
		FROM religiosos."ASOC_TramiteEstatus" 
		WHERE i_id_tbl_tramite = s_id 
		AND i_id_tbl_estatus in (8,15,17) 
		ORDER BY i_id DESC 
		LIMIT 1;
					
		IF status_tramite_id = 15 or status_tramite_id = 17 THEN
		
			INSERT INTO religiosos."ASOC_TramiteEstatus"(i_id_tbl_tramite, i_id_tbl_estatus)
			VALUES (tbl_tramite_id, 36);
		
		ELSE
		
			DELETE FROM religiosos."ASOC_TramiteEstatus" 
			WHERE i_id_tbl_tramite= tbl_tramite_id 
			AND i_id_tbl_estatus =9;
			
			INSERT INTO religiosos."ASOC_TramiteEstatus"(i_id_tbl_tramite, i_id_tbl_estatus)
			VALUES ( tbl_tramite_id, 9); 
		END IF;		
					
		IF status_tramite_id = 8 THEN
			
			INSERT INTO religiosos."TBL_Fechas"(d_inicio, i_estatus, i_id_cat_fechas)
			VALUES (current_timestamp, 9, 3)
			RETURNING i_id INTO tbl_fecha_apertura_id;

			INSERT INTO religiosos."ASOC_Tramite_Fechas"(i_id_tbl_tramite, i_id_tbl_fechas)
			VALUES (tbl_tramite_id, tbl_fecha_apertura_id);

			UPDATE religiosos."TBL_Tramite"
			SET c_nfolio = (SELECT folio FROM religiosos.obtener_folio_registro())--t_folio_new
			WHERE i_id = tbl_tramite_id;
			
		END IF;
							
		RETURN QUERY SELECT
			0 as id_tramite,
			CAST('Su solicitud de registro de asociación religiosa se ha llevado a cabo de forma exitosa.
	 				En breve le llegarán notificaciones a su correo electrónico para su seguimiento.' as varchar) AS mensaje,
			(true) AS proceso_existoso; 
	
	END IF;
	
END;
$BODY$;