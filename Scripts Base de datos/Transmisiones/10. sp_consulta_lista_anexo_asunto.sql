DROP FUNCTION IF EXISTS religiosos.sp_consulta_lista_anexo_asunto(integer, integer);
CREATE OR REPLACE FUNCTION religiosos.sp_consulta_lista_anexo_asunto(
	p_id integer,
	p_id_tramite integer)
    RETURNS TABLE(id_anexo integer, nombre_anexo character varying, url_anexo character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE 
	BEGIN 
	IF  p_id_tramite IN (27) THEN 	
	  RETURN QUERY	
			SELECT 
				 anexo.i_id AS id_anexo,
				 anexo.c_nombre AS nombre_anexo,
				 anexo.c_ruta AS url_anexo
			FROM religiosos."TBL_Transmision" AS tbltrans
			LEFT OUTER JOIN religiosos."TBL_Anexos" AS anexo ON tbltrans.i_id = anexo.i_id_tbl_transmision
			WHERE tbltrans.i_id = p_id AND anexo.i_id_cat_archivo_tramite = p_id_tramite;
	
	ELSE
		RETURN QUERY	
			SELECT 
				 anexo.i_id AS id_anexo,
				 anexo.c_nombre AS nombre_anexo,
				 anexo.c_ruta AS url_anexo
			FROM religiosos."TBL_TNota" AS tomanota
			LEFT OUTER JOIN religiosos."TBL_Anexos" AS anexo ON tomanota.i_id = anexo.i_id_tbl_tnota
			WHERE tomanota.i_id = p_id AND anexo.i_id_cat_archivo_tramite = p_id_tramite;
		END IF;
END;
$BODY$;