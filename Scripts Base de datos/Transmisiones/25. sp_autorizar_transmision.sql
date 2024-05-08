DROP FUNCTION IF EXISTS religiosos.sp_autorizar_transmision(integer, character varying, character varying, character varying, integer);

CREATE OR REPLACE FUNCTION religiosos.sp_autorizar_transmision(
	id_transmision integer,
	fecha character varying,
	horario character varying,
	direccion character varying,
	id_usuario integer)
    RETURNS TABLE(mensaje character varying, destinatario character varying, proceso_exitoso boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
	fecha_inicio integer := 0;
	existeTransmision CONSTANT integer := (SELECT i_id FROM religiosos."TBL_Transmision" WHERE i_id = id_transmision LIMIT 1);
	correo CONSTANT character varying := (SELECT c_correo_electronico FROM religiosos."TBL_Transmision" WHERE i_id = id_transmision LIMIT 1);

BEGIN

	IF existeTransmision is not null  THEN
	
		UPDATE religiosos."TBL_Transmision"
			SET d_fecha_autorizacion = CURRENT_DATE
			, d_fecha_cotejo = COALESCE(CAST(fecha AS date))
			, c_horario_cotejo =  COALESCE(horario, c_horario_cotejo)
			, c_direccion = COALESCE(direccion, c_direccion)
			WHERE i_id = id_transmision;
		
		--ELIMINACIÓN ESTATUS ACTUAL
		DELETE FROM religiosos."ASOC_Transmision_Estatus"
		WHERE i_id_tbl_transmision = id_transmision AND i_id_tbl_estatus = 30; 
		
		INSERT INTO religiosos."ASOC_Transmision_Estatus"(i_id_tbl_transmision, i_id_tbl_estatus)
		VALUES (id_transmision, 32); --ESTATUS AUTORIZADA
		
		INSERT INTO religiosos."TBL_Fechas" (d_inicio, i_estatus, i_id_cat_fechas) 
		Values (current_timestamp, 1, 6) RETURNING i_id INTO fecha_inicio;
		
		INSERT INTO religiosos."ASOC_Transmision_Fechas"(i_id_tbl_transmision, i_id_tbl_fechas)
		VALUES (id_transmision, fecha_inicio);
									  
		RETURN QUERY 
			SELECT CAST('La información se ha cargado de forma exitosa.' as CHARACTER VARYING) AS mensaje
			, correo as destinatario
			,(true) as proceso_exitoso;

	ELSE
			
		RETURN QUERY
			SELECT CAST('La transmisión no existe' AS CHARACTER VARYING) AS mensaje
			, CAST('' AS CHARACTER VARYING) as destinatario
			, (false) as proceso_exitoso;
				
	END IF;

END;
$BODY$;
