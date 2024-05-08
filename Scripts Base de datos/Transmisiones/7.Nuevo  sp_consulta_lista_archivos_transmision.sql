
CREATE OR REPLACE FUNCTION religiosos.sp_consulta_lista_archivos_transmision(
	id_transmision integer)
    RETURNS TABLE(transmisionid integer, cantidadarchivos integer) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
existeIdentificacion integer := (SELECT Count(*)::INTEGER  FROM religiosos."TBL_Anexos" WHERE i_id_tbl_transmision= id_transmision LIMIT 1);
existeOficio integer := (SELECT 
						  Count(*)::INTEGER  AS CantidadArchivos FROM religiosos."ASOC_Transmision_Archivos" AS asocTran
					  LEFT JOIN religiosos."TBL_Archivo" ta
						ON asocTran.i_id_tbl_archivo = ta.i_id WHERE asocTran.i_id_tbl_transmision = id_transmision
					    AND  ta.i_id_tbl_archtram IN (28)						
				        AND ta.i_estatus = 1
			        GROUP BY asocTran.i_id_tbl_transmision LIMIT 1);

BEGIN
							
				IF (existeOficio = 1 AND ExisteIdentificacion >= 1) THEN
				RETURN QUERY 
				  SELECT 
				  id_transmision AS transmisionid,
				  (existeOficio +ExisteIdentificacion) AS CantidadArchivos;
				  END IF;

	END;
$BODY$;
