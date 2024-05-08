DROP FUNCTION IF EXISTS religiosos.sp_asignar_transmision_dictaminador(integer, integer, integer);

CREATE OR REPLACE FUNCTION religiosos.sp_asignar_transmision_dictaminador(
	id_transmision integer,
	id_usuario_dictaminador integer,
	id_usuario_asignador integer)
    RETURNS TABLE(id integer, proceso_exitoso boolean, mensaje character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
	existeTransmision CONSTANT integer := (SELECT i_id FROM religiosos."TBL_Transmision" WHERE i_id = id_transmision LIMIT 1);
	existeDictaminador CONSTANT integer := (SELECT i_id FROM religiosos."ASOC_TransmisionDictaminador" WHERE i_id_tbl_transmision = id_transmision LIMIT 1);
	existeEstatusEspera CONSTANT integer := (SELECT i_id FROM religiosos."ASOC_Transmision_Estatus" WHERE i_id_tbl_transmision = id_transmision AND i_id_tbl_estatus=30 LIMIT 1);
	BEGIN
	
	
	IF existeEstatusEspera >0 THEN
	   
	    DELETE FROM religiosos."ASOC_Transmision_Estatus" WHERE i_id_tbl_transmision=id_transmision AND i_id_tbl_estatus=30;
		
		END IF;
		
		
	
		IF existeTransmision is not null  THEN 
			IF(existeDictaminador is null) THEN
				INSERT INTO religiosos."ASOC_TransmisionDictaminador"(i_id_tbl_transmision, id_tbl_usuario_dictaminador, id_tbl_usuario_asignador)
					VALUES (id_transmision, id_usuario_dictaminador, id_usuario_asignador);
						
			ELSE
			 
			 	UPDATE religiosos."ASOC_TransmisionDictaminador" 
					SET id_tbl_usuario_dictaminador = id_usuario_dictaminador,
						id_tbl_usuario_asignador = id_usuario_asignador
					WHERE i_id = existeDictaminador;
					
			END IF;
				
			DELETE FROM religiosos."ASOC_Transmision_Estatus" 
				WHERE i_id_tbl_transmision= id_transmision 
				AND i_id_tbl_estatus =29; --TRANSMISION ESTATUS REGISTRADA
				
			DELETE FROM religiosos."ASOC_Transmision_Estatus" 
				WHERE i_id_tbl_transmision= id_transmision 
				AND i_id_tbl_estatus =35; --TRANSMISION ESTATUS ESPERA
				
			INSERT INTO religiosos."ASOC_Transmision_Estatus"(i_id_tbl_transmision, i_id_tbl_estatus)
				VALUES (id_transmision, 30); --TRANSMISION ASIGNADA
					
			RETURN QUERY SELECT existeTransmision as id,
				true as proceso_exitoso,
				CAST('La asignación se ha realizado de forma exitosa.' AS CHARACTER VARYING) AS mensaje;
		
		ELSE
			
			RETURN QUERY SELECT 0 as id,
						 false as proceso_exitoso,
						 CAST('Por favor, asigne un dictaminador.' AS CHARACTER VARYING) AS mensaje;
				
		END IF;

	END;
$BODY$;

