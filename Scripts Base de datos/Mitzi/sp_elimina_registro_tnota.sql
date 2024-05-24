-- FUNCTION: religiosos.sp_elimina_registro_tnota(integer)

-- DROP FUNCTION IF EXISTS religiosos.sp_elimina_registro_tnota(integer);

CREATE OR REPLACE FUNCTION religiosos.sp_elimina_registro_tnota(
	id_tnota integer)
    RETURNS TABLE(mensaje text, proceso_exitoso boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
	id_tramite CONSTANT integer := (SELECT i_id_tbl_tramite FROM religiosos."ASOC_TramTNota" WHERE i_id_tbl_tnota = id_tnota LIMIT 1);

BEGIN
     
	 
	 
     DELETE FROM religiosos."TBL_Anexos" WHERE i_id_tbl_tnota=id_tnota;
	 DELETE FROM religiosos."ASOC_TramTNota" WHERE i_id_tbl_tnota=id_tnota;
	 DELETE FROM religiosos."ASOC_TNotaApoderado" WHERE i_id_tbl_tnota=id_tnota;
	 DELETE FROM religiosos."ASOC_TNotaArchivo" WHERE i_id_tbl_tnota=id_tnota;
	 DELETE FROM religiosos."ASOC_TNotaDom" WHERE i_id_tbl_tnota=id_tnota;
	 DELETE FROM religiosos."ASOC_TNotaMovimientos" WHERE i_id_tbl_tnota=id_tnota;
	 DELETE FROM religiosos."ASOC_TNotaRepre" WHERE i_id_tbl_tnota=id_tnota;
	 DELETE FROM religiosos."ASOC_TnotaDictaminador" WHERE i_id_tbl_tnota=id_tnota;
     DELETE FROM religiosos."TBL_TNota" WHERE i_id=id_tnota;
	 DELETE FROM religiosos."ASOC_TramDom" WHERE i_id_tbl_tramite=id_tramite;
     DELETE FROM religiosos."ASOC_Tramite_Usuario" WHERE i_id_tbl_tramite=id_tramite;
	 DELETE FROM religiosos."ASOC_TramiteEstatus" WHERE i_id_tbl_tramite=id_tramite;
	 DELETE FROM religiosos."ASOC_Tramite_Archivos" WHERE i_id_tbl_tramite=id_tramite;
	 DELETE FROM religiosos."ASOC_Tramite_Fechas" WHERE i_id_tbl_tramite=id_tramite;
	 DELETE FROM religiosos."ASOC_TramRep" WHERE i_id_tbl_tramite=id_tramite;
	 DELETE FROM religiosos."TBL_Cotejo" WHERE i_id_tbl_tramite=id_tramite;
     DELETE FROM religiosos."TBL_Tramite" WHERE i_id=id_tramite;
	 
	RETURN QUERY
  
  SELECT 
        'Toma de nota eliminado de forma correcta' AS mensaje,
		true AS proceso_exitoso 	;

END
$BODY$;

ALTER FUNCTION religiosos.sp_elimina_registro_tnota(integer)
    OWNER TO tramitesdgardev_user;
