
CREATE OR REPLACE FUNCTION religiosos.sp_elimina_registro_transmision(
	id_transmision integer)
    RETURNS TABLE(mensaje text, proceso_exitoso boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE

actoReligioso integer[] := ARRAY(SELECT i_id FROM religiosos."TBL_Actos_Religiosos" WHERE i_id_tbl_transmision=id_transmision );

actoFecha integer[] ;

j integer;

i integer;

BEGIN

FOREACH j IN ARRAY actoReligioso

LOOP

actoFecha= ARRAY(SELECT i_id FROM religiosos."TBL_Actos_Fechas" WHERE i_id_acto_religioso=j);
  
  FOREACH i IN ARRAY actoFecha
   LOOP
     DELETE FROM  religiosos."ASOC_Actos_Fechas_Dia_Mes_Anio" WHERE i_tbl_acto_fecha=i;
   
   END LOOP;   
   
     DELETE FROM religiosos."TBL_Actos_Fechas" WHERE i_id_acto_religioso=j;
	 DELETE FROM religiosos."CAT_Medios_Transmision" WHERE i_id_tbl_acto=j;
	END LOOP;
    
	DELETE FROM religiosos."TBL_Actos_Religiosos" WHERE i_id_tbl_transmision=id_transmision;
     DELETE FROM religiosos."ASOC_Transmision_Usuario" WHERE i_id_tbl_transmision=id_transmision;
     DELETE FROM religiosos."ASOC_Transmision_Estatus" WHERE i_id_tbl_transmision=id_transmision;
	 DELETE FROM religiosos."ASOC_Transmision_Archivos" WHERE i_id_tbl_transmision=id_transmision;
	 	 DELETE FROM religiosos."TBL_Transmision" WHERE i_id=id_transmision;

	
	
	
	RETURN QUERY
  
  SELECT 
        'Transmision eliminado de forma correcta' AS mensaje,
		true AS proceso_exitoso 	;

END
$BODY$;