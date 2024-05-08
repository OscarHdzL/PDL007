DROP FUNCTION IF EXISTS religiosos.sp_actualizar_tramite_paso_sexto(integer, integer, integer);

CREATE OR REPLACE FUNCTION religiosos.sp_actualizar_tramite_paso_sexto(
	s_id integer,
	s_cat_cnotario integer,
	s_cat_modalidad integer)
    RETURNS TABLE(id_tramite integer, mensaje character varying, proceso_existoso boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
tbl_tramite_id integer;
tbl_cnotorio_id integer;

t_folio_old character varying;
t_folio_new character varying;

tbl_fecha_apertura_id integer;

id_tbl_perfil integer;

status_tramite_id integer := (SELECT i_id_tbl_estatus
								FROM religiosos."ASOC_TramiteEstatus" 
								WHERE i_id_tbl_tramite = s_id 
								AND i_id_tbl_estatus in (7,8,15,17) 
								ORDER BY i_id DESC 
								LIMIT 1);
BEGIN
			
			SELECT i_id INTO id_tbl_perfil
            FROM religiosos."TBL_Usuario"
            WHERE i_id_tbl_perfil = 7
            LIMIT 1;

			SELECT tt.i_id,tt.i_id_tbl_cnotorioarr INTO tbl_tramite_id, tbl_cnotorio_id 
			FROM religiosos."TBL_Tramite" tt WHERE i_id=s_id;
		    if(tbl_tramite_id is null or tbl_tramite_id <1) THEN
			
					RETURN QUERY SELECT
		           	 0 as id_tramite,
					 CAST('La información no se ha cargado de forma exitosa.' as varchar) AS mensaje,
					 (false) AS proceso_existoso;

		     ELSE
				
					  IF(tbl_cnotorio_id is not null or tbl_cnotorio_id > 0) THEN
					        UPDATE  religiosos."TBL_Cnotorioarr"
							SET i_id_cat_cnotorioarr = s_cat_cnotario
							WHERE i_id = tbl_cnotorio_id;
							
							UPDATE religiosos."TBL_Tramite"
							SET i_id_tbl_cotejodoc=s_cat_modalidad
							WHERE i_id =tbl_tramite_id;
							
							IF status_tramite_id = 7 OR status_tramite_id = 8 OR status_tramite_id = 0 THEN
								DELETE FROM religiosos."ASOC_TramiteEstatus"
								WHERE i_id_tbl_tramite=s_id  AND i_id_tbl_estatus=8;
								INSERT INTO religiosos."ASOC_TramiteEstatus"(
								i_id_tbl_tramite, i_id_tbl_estatus)
								VALUES ( tbl_tramite_id, 8);
							END IF;
							
							RETURN QUERY SELECT
							id_tbl_perfil as id_tramite,
							CAST('La información se ha cargado de forma exitosa.'  as varchar) AS mensaje,
							(true) AS proceso_existoso;
					  ELSE
			                INSERT INTO religiosos."TBL_Cnotorioarr"(
								i_id_cat_cnotorioarr, i_id_tbl_archivo, i_estatus)
								VALUES (s_cat_cnotario, NULL, 0) RETURNING i_id INTO tbl_cnotorio_id;
							
							UPDATE religiosos."TBL_Tramite"
							SET i_id_tbl_cnotorioarr=tbl_cnotorio_id,
							i_id_tbl_cotejodoc=s_cat_modalidad
							WHERE i_id =tbl_tramite_id;
							
							IF status_tramite_id = 7 OR status_tramite_id = 8 OR status_tramite_id = 0 THEN
								DELETE FROM religiosos."ASOC_TramiteEstatus"
								WHERE i_id_tbl_tramite=s_id  AND i_id_tbl_estatus=8;
								INSERT INTO religiosos."ASOC_TramiteEstatus"(
									i_id_tbl_tramite, i_id_tbl_estatus)
									VALUES ( tbl_tramite_id, 8);
							END IF;
								
							RETURN QUERY SELECT
							id_tbl_perfil as id_tramite,
							CAST('La información se ha cargado de forma exitosa.' as varchar) AS mensaje,
							(true) AS proceso_existoso;
							    
					  END IF;
			 END IF;
		END;
$BODY$;