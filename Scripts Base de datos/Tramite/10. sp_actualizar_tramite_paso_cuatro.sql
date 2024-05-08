DROP FUNCTION IF EXISTS religiosos.sp_actualizar_tramite_paso_cuatro(integer, character varying, character varying, character varying, character varying, character varying, character varying, character varying, integer, integer, integer, integer, integer);


CREATE OR REPLACE FUNCTION religiosos.sp_actualizar_tramite_paso_cuatro(
	s_id integer,
	s_superficie character varying,
	s_medidas character varying,
	s_colindancia_text_1 character varying,
	s_colindancia_text_2 character varying,
	s_colindancia_text_3 character varying,
	s_colindancia_text_4 character varying,
	s_colindancia_usos character varying,
	s_colindancia_num_1 double precision,
	s_colindancia_num_2 double precision,
	s_colindancia_num_3 double precision,
	s_colindancia_num_4 double precision,
	s_aviso_apertura integer)
    RETURNS TABLE(id_tramite integer, mensaje character varying, proceso_existoso boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
tbl_medidas_id integer;
tbl_tramite_id integer;
tbl_colindancia_id integer;
tbl_avisoap_id integer;
			BEGIN
			          SELECT tt.i_id, tm.i_id, tav.i_id INTO tbl_tramite_id, tbl_medidas_id,tbl_avisoap_id
					  FROM religiosos."TBL_Tramite" tt 
					  LEFT JOIN religiosos."TBL_Medidas" tm ON tm.i_id = tt.i_id_tbl_medidas
					  LEFT JOIN religiosos."TBL_Avisoap" tav ON tav.i_id= tt.i_id_tbl_avisoap
					  WHERE tt.i_id = s_id limit 1;
					  
					  IF(tbl_medidas_id is not null or tbl_medidas_id > 0) THEN
					         IF(tbl_avisoap_id is not null) THEN
							    UPDATE religiosos."TBL_Avisoap" 
							   SET i_id_cat_avisoap =s_aviso_apertura WHERE i_id=tbl_avisoap_id;
							 ELSE
					            INSERT INTO religiosos."TBL_Avisoap"(
	                             i_id_cat_avisoap, i_id_tbl_archivo, i_estatus) 
							     VALUES (s_aviso_apertura, NULL, 0) RETURNING i_id INTO tbl_avisoap_id;
							    
								 UPDATE  religiosos."TBL_Tramite"
					             SET i_id_tbl_avisoap=tbl_avisoap_id  WHERE i_id=tbl_tramite_id;
								 
							  END IF;
					      
							
					        UPDATE religiosos."TBL_Medidas"
							SET c_superficie = s_superficie, c_medidas = s_medidas, c_usos = s_colindancia_usos
							WHERE i_id =tbl_medidas_id;
							
							DELETE FROM religiosos."ASOC_Medidas_Colindancias" WHERE i_id_tbl_medidas = tbl_medidas_id;
							
							INSERT INTO religiosos."TBL_Colindancias"( c_nombre, c_descripcion, d_medida)
							VALUES ( 'Colindancia 1', s_colindancia_text_1, s_colindancia_num_1) RETURNING i_id INTO tbl_colindancia_id;
							
							INSERT INTO religiosos."ASOC_Medidas_Colindancias"(
								i_id_tbl_medidas, i_id_tbl_colindancias)
								VALUES ( tbl_medidas_id,tbl_colindancia_id);
							
							
							INSERT INTO religiosos."TBL_Colindancias"( c_nombre, c_descripcion, d_medida)
							VALUES ( 'Colindancia 2', s_colindancia_text_2, s_colindancia_num_2) RETURNING i_id INTO tbl_colindancia_id;
							
							INSERT INTO religiosos."ASOC_Medidas_Colindancias"(
								i_id_tbl_medidas, i_id_tbl_colindancias)
								VALUES ( tbl_medidas_id,tbl_colindancia_id);
							
							INSERT INTO religiosos."TBL_Colindancias"( c_nombre, c_descripcion, d_medida)
							VALUES ( 'Colindancia 3', s_colindancia_text_3, s_colindancia_num_3) RETURNING i_id INTO tbl_colindancia_id;
							
							INSERT INTO religiosos."ASOC_Medidas_Colindancias"(
								i_id_tbl_medidas, i_id_tbl_colindancias)
								VALUES ( tbl_medidas_id,tbl_colindancia_id);
							
							INSERT INTO religiosos."TBL_Colindancias"( c_nombre, c_descripcion, d_medida)
							VALUES ( 'Colindancia 4', s_colindancia_text_4, s_colindancia_num_4) RETURNING i_id INTO tbl_colindancia_id;
							
							INSERT INTO religiosos."ASOC_Medidas_Colindancias"(
								i_id_tbl_medidas, i_id_tbl_colindancias)
								VALUES ( tbl_medidas_id,tbl_colindancia_id);
							
							RETURN QUERY SELECT
							tbl_tramite_id as id_tramite,
							CAST('La información se ha cargado de forma exitosa.'   as varchar) AS mensaje,
							(true) AS proceso_existoso;
					  ELSE
					       IF(tbl_tramite_id is not null or tbl_tramite_id > 0) THEN
						   INSERT INTO religiosos."TBL_Medidas"(
							    c_superficie, c_medidas, c_usos)
							   VALUES (s_superficie, s_medidas, s_colindancia_usos) RETURNING i_id INTO tbl_medidas_id;
							   
							 IF(tbl_avisoap_id is not null) THEN
							    UPDATE religiosos."TBL_Avisoap" 
							    SET i_id_cat_tbl_archivo =s_aviso_apertura WHERE i_id=i_id_tbl_avisoap;
							 ELSE
					            INSERT INTO religiosos."TBL_Avisoap"(
	                             i_id_cat_avisoap, i_id_tbl_archivo, i_estatus) 
							     VALUES (s_aviso_apertura, NULL, 0) RETURNING i_id INTO tbl_avisoap_id;
							    
								 UPDATE  religiosos."TBL_Tramite"
					             SET i_id_tbl_avisoap=tbl_avisoap_id  WHERE i_id=tbl_tramite_id;
								 
							  END IF;
							  
							UPDATE  religiosos."TBL_Tramite"
					        SET 
							i_id_tbl_medidas=tbl_medidas_id  WHERE i_id=tbl_tramite_id;
							
							INSERT INTO religiosos."TBL_Colindancias"( c_nombre, c_descripcion, d_medida)
							VALUES ( 'Colindancia 1', s_colindancia_text_1, s_colindancia_num_1) RETURNING i_id INTO tbl_colindancia_id;
							
							INSERT INTO religiosos."ASOC_Medidas_Colindancias"(
								i_id_tbl_medidas, i_id_tbl_colindancias)
								VALUES ( tbl_medidas_id,tbl_colindancia_id);
							
							
							INSERT INTO religiosos."TBL_Colindancias"( c_nombre, c_descripcion, d_medida)
							VALUES ( 'Colindancia 2', s_colindancia_text_2, s_colindancia_num_2) RETURNING i_id INTO tbl_colindancia_id;
							
							INSERT INTO religiosos."ASOC_Medidas_Colindancias"(
								i_id_tbl_medidas, i_id_tbl_colindancias)
								VALUES ( tbl_medidas_id,tbl_colindancia_id);
							
							INSERT INTO religiosos."TBL_Colindancias"( c_nombre, c_descripcion, d_medida)
							VALUES ( 'Colindancia 3', s_colindancia_text_3, s_colindancia_num_3) RETURNING i_id INTO tbl_colindancia_id;
							
							INSERT INTO religiosos."ASOC_Medidas_Colindancias"(
								i_id_tbl_medidas, i_id_tbl_colindancias)
								VALUES ( tbl_medidas_id,tbl_colindancia_id);
							
							INSERT INTO religiosos."TBL_Colindancias"( c_nombre, c_descripcion, d_medida)
							VALUES ( 'Colindancia 4', s_colindancia_text_4, s_colindancia_num_4) RETURNING i_id INTO tbl_colindancia_id;
							
							INSERT INTO religiosos."ASOC_Medidas_Colindancias"(
								i_id_tbl_medidas, i_id_tbl_colindancias)
								VALUES ( tbl_medidas_id,tbl_colindancia_id);
							INSERT INTO religiosos."ASOC_TramiteEstatus"(
								i_id_tbl_tramite, i_id_tbl_estatus)
								VALUES ( tbl_tramite_id, 6);
							RETURN QUERY SELECT
							tbl_tramite_id as id_tramite,
							CAST('La información se ha cargado de forma exitosa.'  as varchar) AS mensaje,
							(true) AS proceso_existoso;
							
							ELSE
							
							RETURN QUERY SELECT
							0 as id_tramite,
							CAST('La información no se ha cargado de forma exitosa.' as varchar) AS mensaje,
							(false) AS proceso_existoso;
							END IF;
							
					  END IF;
		END;
$BODY$;
