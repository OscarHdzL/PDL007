DROP FUNCTION IF EXISTS religiosos.sp_consulta_detalle_tramite_paso_sexto(integer, integer, boolean);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_detalle_tramite_paso_sexto(
	s_id_us integer,
	i_id_c integer DEFAULT NULL::integer,
	is_dictaminador boolean DEFAULT false)
    RETURNS TABLE(s_id integer, s_cat_notarioarr integer, s_cat_modalidad integer, s_doc_ext_1 boolean, s_doc_ext_2 boolean, s_doc_notario boolean, s_id_cnotorioarr integer) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
 status constant integer:=(SELECT MAX(i_id_tbl_estatus) FROM religiosos."TBL_Tramite" tt 
								 JOIN religiosos."ASOC_TramiteEstatus" astes ON astes.i_id_tbl_tramite = tt.i_id
								  JOIN religiosos."ASOC_Tramite_Usuario" astu ON astu.i_id_tbl_tramite = tt.i_id
								 WHERE   (tt.i_id = i_id_c or i_id_c is null) 
								 AND (astu.i_id_tbl_usuario = s_id_us or s_id_us is null));
 -----Este estatus es el último registrado para permitir corregir al usuario.
 statusEdicion constant integer:=(SELECT astes.i_id_tbl_estatus FROM religiosos."TBL_Tramite" tt 
								 JOIN religiosos."ASOC_TramiteEstatus" astes ON astes.i_id_tbl_tramite = tt.i_id
								 JOIN religiosos."ASOC_Tramite_Usuario" astu ON astu.i_id_tbl_tramite = tt.i_id
								 WHERE   (tt.i_id = i_id_c) 
								 AND (astu.i_id_tbl_usuario = s_id_us) ORDER BY astes.i_id DESC limit 1);

idPerfilAsignador constant integer :=(SELECT i_id_tbl_perfil
								FROM religiosos."TBL_Usuario"
								WHERE i_id = s_id_us
								limit 1);
BEGIN
	   
 IF (status < 9 or statusEdicion in (3,4,5,6,7,8,9,10,14,15,16,17,18,19,20,36)) THEN
         RETURN QUERY SELECT
		 tt.i_id AS s_id,
         cnot.i_id AS s_cat_cnotario,
	     tt.i_id_tbl_cotejodoc AS s_cat_modalidad,
		  COALESCE((SELECT CASE WHEN ta.i_id is null THEN false
					  ELSE true END
		 FROM religiosos."TBL_Convenioex" asoc 
		 INNER JOIN religiosos."TBL_Archivo" ta ON asoc.i_id_tbl_archivo_1 = ta.i_id
		 WHERE asoc.i_id = tt.i_id_tbl_convenioex
		 AND ta.i_estatus = 1 -- ACTIVO
		 LIMIT 1),false),
		 COALESCE((SELECT  CASE WHEN ta.i_id is null THEN false
					  ELSE true END
		 FROM religiosos."TBL_Convenioex" asoc 
		 INNER JOIN religiosos."TBL_Archivo" ta ON asoc.i_id_tbl_archivo_2 = ta.i_id
		 WHERE asoc.i_id = tt.i_id_tbl_convenioex
		 AND ta.i_estatus = 1 -- ACTIVO
		 LIMIT 1),false),
		COALESCE((SELECT CASE WHEN ta.c_nombre is null THEN false
					  ELSE true END FROM religiosos."TBL_Cnotorioarr" asoc INNER JOIN
				  religiosos."TBL_Archivo" ta ON asoc.i_id_tbl_archivo = ta.i_id
				 WHERE asoc.i_id = tt.i_id_tbl_cnotorioarr),false),
	    
		 tt.i_id_tbl_cnotorioarr
		 FROM religiosos."TBL_Tramite" tt 
		 JOIN religiosos."ASOC_Tramite_Usuario" astu ON astu.i_id_tbl_tramite = tt.i_id
		 LEFT JOIN religiosos."TBL_Cnotorioarr" tc ON tc.i_id = tt.i_id_tbl_cnotorioarr
		 LEFT JOIN religiosos."CAT_Cnotorioarr" cnot ON tc.i_id_cat_cnotorioarr = cnot.i_id
		 WHERE 
		 (tt.i_id = i_id_c or i_id_c is null) 
		 AND (astu.i_id_tbl_usuario = s_id_us or s_id_us is null) limit 1;
ELSEIF(is_dictaminador) THEN

	RETURN QUERY SELECT
		 tt.i_id AS s_id,
         cnot.i_id AS s_cat_cnotario,
	     tt.i_id_tbl_cotejodoc AS s_cat_modalidad,
		  COALESCE((SELECT CASE WHEN ta.i_id is null THEN false
					  ELSE true END
		 FROM religiosos."TBL_Convenioex" asoc 
		 INNER JOIN religiosos."TBL_Archivo" ta ON asoc.i_id_tbl_archivo_1 = ta.i_id
		 WHERE asoc.i_id = tt.i_id_tbl_convenioex
		 AND ta.i_estatus = 1 -- ACTIVO
		 LIMIT 1),false),
		 COALESCE((SELECT  CASE WHEN ta.i_id is null THEN false
					  ELSE true END
		 FROM religiosos."TBL_Convenioex" asoc 
		 INNER JOIN religiosos."TBL_Archivo" ta ON asoc.i_id_tbl_archivo_2 = ta.i_id
		 WHERE asoc.i_id = tt.i_id_tbl_convenioex
		 AND ta.i_estatus = 1 -- ACTIVO
		 LIMIT 1),false),
		COALESCE((SELECT CASE WHEN ta.c_nombre is null THEN false
					  ELSE true END FROM religiosos."TBL_Cnotorioarr" asoc INNER JOIN
				  religiosos."TBL_Archivo" ta ON asoc.i_id_tbl_archivo = ta.i_id
				 WHERE asoc.i_id = tt.i_id_tbl_cnotorioarr),false),
	    
		 tt.i_id_tbl_cnotorioarr
		 FROM religiosos."TBL_Tramite" tt 
		 JOIN religiosos."ASOC_TraDictaminador" astu ON astu.i_id_tbl_tramite = tt.i_id
		 LEFT JOIN religiosos."TBL_Cnotorioarr" tc ON tc.i_id = tt.i_id_tbl_cnotorioarr
		 LEFT JOIN religiosos."CAT_Cnotorioarr" cnot ON tc.i_id_cat_cnotorioarr = cnot.i_id
		 WHERE (tt.i_id = i_id_c or i_id_c is null)
		 limit 1;

ELSEIF(idPerfilAsignador = 7)THEN
		RETURN QUERY SELECT
		 tt.i_id AS s_id,
         cnot.i_id AS s_cat_cnotario,
	     tt.i_id_tbl_cotejodoc AS s_cat_modalidad,
		  COALESCE((SELECT CASE WHEN ta.i_id is null THEN false
					  ELSE true END
		 FROM religiosos."TBL_Convenioex" asoc 
		 INNER JOIN religiosos."TBL_Archivo" ta ON asoc.i_id_tbl_archivo_1 = ta.i_id
		 WHERE asoc.i_id = tt.i_id_tbl_convenioex
		 AND ta.i_estatus = 1 -- ACTIVO
		 LIMIT 1),false),
		 COALESCE((SELECT  CASE WHEN ta.i_id is null THEN false
					  ELSE true END
		 FROM religiosos."TBL_Convenioex" asoc 
		 INNER JOIN religiosos."TBL_Archivo" ta ON asoc.i_id_tbl_archivo_2 = ta.i_id
		 WHERE asoc.i_id = tt.i_id_tbl_convenioex
		 AND ta.i_estatus = 1 -- ACTIVO
		 LIMIT 1),false),
		COALESCE((SELECT CASE WHEN ta.c_nombre is null THEN false
					  ELSE true END FROM religiosos."TBL_Cnotorioarr" asoc INNER JOIN
				  religiosos."TBL_Archivo" ta ON asoc.i_id_tbl_archivo = ta.i_id
				 WHERE asoc.i_id = tt.i_id_tbl_cnotorioarr),false),
	    
		 tt.i_id_tbl_cnotorioarr
		 FROM religiosos."TBL_Tramite" tt 
		 LEFT JOIN religiosos."ASOC_TraDictaminador" astu ON astu.i_id_tbl_tramite = tt.i_id
		 LEFT JOIN religiosos."TBL_Cnotorioarr" tc ON tc.i_id = tt.i_id_tbl_cnotorioarr
		 LEFT JOIN religiosos."CAT_Cnotorioarr" cnot ON tc.i_id_cat_cnotorioarr = cnot.i_id
		 WHERE (tt.i_id = i_id_c or i_id_c is null)
		 limit 1;
	
	ELSE
		RETURN QUERY SELECT
		 tt.i_id AS s_id,
         cnot.i_id AS s_cat_cnotario,
	     tt.i_id_tbl_cotejodoc AS s_cat_modalidad,
		  COALESCE((SELECT CASE WHEN ta.i_id is null THEN false
					  ELSE true END
		 FROM religiosos."TBL_Convenioex" asoc 
		 INNER JOIN religiosos."TBL_Archivo" ta ON asoc.i_id_tbl_archivo_1 = ta.i_id
		 WHERE asoc.i_id = tt.i_id_tbl_convenioex
		 AND ta.i_estatus = 1 -- ACTIVO
		 LIMIT 1),false),
		 COALESCE((SELECT  CASE WHEN ta.i_id is null THEN false
					  ELSE true END
		 FROM religiosos."TBL_Convenioex" asoc 
		 INNER JOIN religiosos."TBL_Archivo" ta ON asoc.i_id_tbl_archivo_2 = ta.i_id
		 WHERE asoc.i_id = tt.i_id_tbl_convenioex
		 AND ta.i_estatus = 1 -- ACTIVO
		 LIMIT 1),false),
		COALESCE((SELECT CASE WHEN ta.c_nombre is null THEN false
					  ELSE true END FROM religiosos."TBL_Cnotorioarr" asoc INNER JOIN
				  religiosos."TBL_Archivo" ta ON asoc.i_id_tbl_archivo = ta.i_id
				 WHERE asoc.i_id = tt.i_id_tbl_cnotorioarr),false),
	    
		 tt.i_id_tbl_cnotorioarr
		 FROM religiosos."TBL_Tramite" tt 
		 JOIN religiosos."ASOC_TraDictaminador" astu ON astu.i_id_tbl_tramite = tt.i_id
		 LEFT JOIN religiosos."TBL_Cnotorioarr" tc ON tc.i_id = tt.i_id_tbl_cnotorioarr
		 LEFT JOIN religiosos."CAT_Cnotorioarr" cnot ON tc.i_id_cat_cnotorioarr = cnot.i_id
		 WHERE 
		 (tt.i_id = i_id_c or i_id_c is null) 
		 AND (astu.i_id_tbl_usuariodictam = s_id_us) limit 1;
	
	END IF;
 
--ELSE
    --RETURN;
--END IF;
END;
$BODY$;