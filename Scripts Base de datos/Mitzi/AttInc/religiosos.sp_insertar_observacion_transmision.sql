-- FUNCTION: religiosos.sp_insertar_observacion_transmision(integer, character varying, integer, integer)

-- DROP FUNCTION IF EXISTS religiosos.sp_insertar_observacion_transmision(integer, character varying, integer, integer);

CREATE OR REPLACE FUNCTION religiosos.sp_insertar_observacion_transmision(
	id_transmision integer,
	observacion character varying,
	id_estatus integer,
	id_usuario integer)
    RETURNS TABLE(mensaje character varying, usuario integer, destinatario character varying, observacion_emitida character varying, proceso_exitoso boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$

DECLARE

	existeTransmision CONSTANT integer := (SELECT i_id FROM religiosos."TBL_Transmision" WHERE i_id = id_transmision LIMIT 1);
	correo CONSTANT character varying := (SELECT c_correo_electronico FROM religiosos."TBL_Transmision" WHERE i_id = id_transmision LIMIT 1);

BEGIN

	IF existeTransmision is not null  THEN
	
		INSERT INTO religiosos."TBL_Observaciones_Transmision"(i_id_tbl_transmision, c_observacion, b_read)
		VALUES (id_transmision, observacion, false);
		
		IF(id_estatus = 0) THEN
		
			--ELIMINACIÓN ESTATUS ACTUAL
			DELETE FROM religiosos."ASOC_Transmision_Estatus"
			WHERE i_id_tbl_transmision = id_transmision AND i_id_tbl_estatus IN (30, 31); 

			INSERT INTO religiosos."ASOC_Transmision_Estatus"(i_id_tbl_transmision, i_id_tbl_estatus)
			VALUES (id_transmision, 31); --ESTATUS OBSERVACION
		
		ELSE
			--ELIMINACIÓN ESTATUS ACTUAL
			DELETE FROM religiosos."ASOC_Transmision_Estatus"
			WHERE i_id_tbl_transmision = id_transmision AND i_id_tbl_estatus IN (30, 31, 32, 33, 34); 

			INSERT INTO religiosos."ASOC_Transmision_Estatus"(i_id_tbl_transmision, i_id_tbl_estatus)
			VALUES (id_transmision, id_estatus); --ESTATUS OBSERVACION
			
		END IF;
										  
		RETURN QUERY 
			SELECT CAST('La información se ha cargado de forma exitosa.' AS character varying) AS mensaje
			, id_usuario AS usuario
			, correo AS destinatario
			, observacion AS observacion_emitida
			, (true) as proceso_exitoso;

	ELSE
			
		RETURN QUERY
			SELECT CAST('La transmisión no existe' AS character varying) AS mensaje
			, id_usuario AS usuario
			, CAST('' AS character varying) AS destinatario
			, CAST('' AS character varying) AS observacion_emitida
			, (false) as proceso_exitoso;
				
	END IF;

END;
$BODY$;

ALTER FUNCTION religiosos.sp_insertar_observacion_transmision(integer, character varying, integer, integer)
    OWNER TO tramitesdgardev_user;
