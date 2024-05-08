
DROP FUNCTION IF EXISTS religiosos.sp_actualizar_estatus_transmision(integer, integer);
CREATE OR REPLACE FUNCTION religiosos.sp_actualizar_estatus_transmision(
	i_id_transmision integer,
	i_id_estatus integer)
    RETURNS TABLE(mensaje character varying, proceso_exitoso boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE

t_folio_old character varying;
t_folio_new character varying;

estatusTrans integer :=(SELECT i_id_tbl_estatus FROM religiosos."ASOC_Transmision_Estatus"
									  WHERE i_id_tbl_transmision=i_id_transmision ORDER BY i_id DESC limit 1 );

_folio character varying := (SELECT c_nfolio FROM religiosos."TBL_Transmision" WHERE i_id = i_id_transmision LIMIT 1 );

BEGIN

 

    IF(i_id_estatus = 29) THEN --REGISTRADA
        INSERT INTO religiosos."ASOC_Transmision_Estatus"(i_id_tbl_transmision, i_id_tbl_estatus)
		VALUES (i_id_transmision, i_id_estatus);
        
        UPDATE religiosos."TBL_Transmision"
        SET d_fecha_solicitud = CURRENT_DATE
        WHERE i_id = i_id_transmision;
	ELSEIF(estatusTrans = 31) THEN --OBSERVACIONES
	   
	   DELETE  FROM religiosos."ASOC_Transmision_Estatus" WHERE i_id_tbl_transmision=i_id_transmision AND i_id_tbl_estatus=38;
	   
        INSERT INTO religiosos."ASOC_Transmision_Estatus"(i_id_tbl_transmision, i_id_tbl_estatus)
		VALUES (i_id_transmision, 38);
        
        UPDATE religiosos."TBL_Transmision"
        SET d_fecha_solicitud = CURRENT_DATE
        WHERE i_id = i_id_transmision;	
	
	ELSE  -- REGRESA A DICTAMINADOR ESTATUS 30
	
		DELETE FROM religiosos."ASOC_Transmision_Estatus"
			WHERE i_id_tbl_transmision = i_id_transmision AND i_id_tbl_estatus = 31;
			
		INSERT INTO religiosos."ASOC_Transmision_Estatus"(i_id_tbl_transmision, i_id_tbl_estatus)
		VALUES (i_id_transmision, i_id_estatus);
        
    END IF;
	
	IF(_folio ISNULL AND i_id_estatus = 35) THEN
		 SELECT c_nfolio INTO t_folio_old FROM religiosos."TBL_Transmision" WHERE c_nfolio not in ('') ORDER BY i_id DESC Limit 1;
			
			IF (t_folio_old = '') 
			THEN
				t_folio_old:= CAST(CONCAT('00000','/',EXTRACT(YEAR FROM NOW())) AS CHARACTER VARYING);
			END IF;
			
			SELECT
				CASE 
				WHEN CAST(EXTRACT(YEAR FROM NOW()) AS CHARACTER VARYING) = CAST(SUBSTRING(t_folio_old, 7, 4) AS CHARACTER VARYING)
			THEN 
			   CAST(CONCAT(CAST(to_char(CAST(SUBSTRING( t_folio_old, 1, 5) AS INTEGER)+1, 'fm00000')
					 AS CHARACTER VARYING),'/',EXTRACT(YEAR FROM NOW())) AS CHARACTER VARYING)
			ELSE
			    CAST(CONCAT('00001','/',EXTRACT(YEAR FROM NOW())) AS CHARACTER VARYING)
			END INTO t_folio_new;
			
	
		UPDATE religiosos."TBL_Transmision"
		SET c_nfolio = (SELECT folio FROM religiosos.obtener_folio_transmisiones()),--t_folio_new,
			d_fecha_solicitud = NOW()
		WHERE i_id = i_id_transmision;
		
	END IF;

	RETURN QUERY 
		SELECT CAST('La información se ha cargado de forma exitosa.' as CHARACTER VARYING) AS mensaje,
		(true) as proceso_exitoso;

END;
$BODY$;