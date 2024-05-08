CREATE OR REPLACE FUNCTION religiosos.sp_elimina_registro_tramite(
	id_tramite integer)
    RETURNS TABLE(mensaje text, proceso_exitoso boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
 
	 DELETE FROM religiosos."ASOC_TramDom" WHERE i_id_tbl_tramite=id_tramite ;
     DELETE FROM religiosos."ASOC_Tramite_Usuario" WHERE i_id_tbl_tramite=id_tramite;
	 DELETE FROM religiosos."ASOC_TramiteEstatus" WHERE i_id_tbl_tramite=id_tramite;
	 DELETE FROM religiosos."ASOC_Tramite_Archivos" WHERE i_id_tbl_tramite=id_tramite;
	 DELETE FROM religiosos."ASOC_Tramite_Fechas" WHERE i_id_tbl_tramite=id_tramite;
	 DELETE FROM religiosos."ASOC_TramRep" WHERE i_id_tbl_tramite=id_tramite;
     DELETE FROM religiosos."TBL_Tramite" WHERE i_id=id_tramite;
	
	RETURN QUERY
  
  SELECT 
        'Tramite eliminado de forma correcta' AS mensaje,
		true AS proceso_exitoso 	;

END
$BODY$;
