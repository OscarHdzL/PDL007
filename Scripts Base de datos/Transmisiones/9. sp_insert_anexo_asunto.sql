DROP FUNCTION IF EXISTS religiosos.sp_insert_anexo_asunto(integer, character varying, character varying, integer);
CREATE OR REPLACE FUNCTION religiosos.sp_insert_anexo_asunto(
	p_id integer,
	p_nombre_anexo character varying,
	p_url_anexo character varying,
	p_id_tramite integer)
    RETURNS TABLE(id_anexo integer, mensaje character varying, proceso_exitoso bit) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE

		existeTomaNota integer := (SELECT i_id FROM religiosos."TBL_TNota" WHERE i_id = p_id LIMIT 1);
		existeTransmision integer := (SELECT i_id FROM religiosos."TBL_Transmision" WHERE i_id = p_id LIMIT 1);		
		anexo_id integer := null;

	BEGIN
	IF (existeTomaNota IS NOT NULL AND p_id_tramite NOT IN (27))
	THEN
		INSERT INTO religiosos."TBL_Anexos"
		(c_nombre, 
		 c_ruta, 
		 i_id_tbl_tnota,
   		i_id_cat_archivo_tramite) 
		VALUES 
		(p_nombre_anexo,
		 p_url_anexo,
		 p_id,
		p_id_tramite)
		RETURNING i_id INTO anexo_id; 
				 				 
		RETURN QUERY SELECT 
        	anexo_id as id_anexo,
			CAST('Registro completado correctamente' as varchar) AS mensaje, 
			(1::bit) AS proceso_exitoso;	
	ELSEIF (existeTransmision IS NOT NULL) THEN
	  	INSERT INTO religiosos."TBL_Anexos"
		(c_nombre, 
		 c_ruta, 
		 i_id_tbl_transmision,
   		i_id_cat_archivo_tramite) 
		VALUES 
		(p_nombre_anexo,
		 p_url_anexo,
		 p_id,
		 p_id_tramite)
		RETURNING i_id INTO anexo_id; 
				 				 
		RETURN QUERY SELECT 
        	anexo_id as id_anexo,
			CAST('Registro completado correctamente' as varchar) AS mensaje, 
			(1::bit) AS proceso_exitoso;	
	   
	ELSE	  
		RETURN QUERY SELECT 
        	0 as id_anexo,
			CASE 
				WHEN existeTomaNota IS NULL THEN CAST ('La toma de nota no existe' as varchar)
			END AS mensaje,
			(0::bit) AS proceso_exitoso;
	END IF;	
	
	END;
	
$BODY$;