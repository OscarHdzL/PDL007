-- DROP FUNCTION religiosos.sp_consulta_declaratoria_paso5(int4);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_declaratoria_paso5(p_id_declaratoria integer)
 RETURNS TABLE(id_declaratoria integer, declaratoria_verdad bit, genera_oficio bit, peticion integer, anexo1 integer, anexo2 integer)
 LANGUAGE plpgsql
AS $function$
DECLARE

BEGIN

	RETURN QUERY 
		SELECT declaratoria.i_id as id_declaratoria
			, declaratoria.b_declaratoria_verdad as declaratoria_verdad
			, declaratoria.b_genera_oficio as genera_oficio
			, (SELECT ta.i_id
				FROM religiosos."ASOC_Declaratoria_Archivos" asoc
			   	INNER JOIN religiosos."TBL_Archivo" ta ON asoc.i_id_tbl_archivo = ta.i_id
			   	WHERE asoc.i_id_tbl_declaratoria = p_id_declaratoria
			    AND ta.i_id_tbl_archtram = 31
			   	AND ta.i_estatus = 1 -- ACTIVO
			   	LIMIT 1) AS peticion
			, (SELECT ta.i_id
				FROM religiosos."ASOC_Declaratoria_Archivos" asoc
			   	INNER JOIN religiosos."TBL_Archivo" ta ON asoc.i_id_tbl_archivo = ta.i_id
			   	WHERE asoc.i_id_tbl_declaratoria = p_id_declaratoria
			    AND ta.i_id_tbl_archtram = 32
			   	AND ta.i_estatus = 1 -- ACTIVO
			   	LIMIT 1) AS anexo1
			, (SELECT ta.i_id
				FROM religiosos."ASOC_Declaratoria_Archivos" asoc
			   	INNER JOIN religiosos."TBL_Archivo" ta ON asoc.i_id_tbl_archivo = ta.i_id
			   	WHERE asoc.i_id_tbl_declaratoria = p_id_declaratoria
			    AND ta.i_id_tbl_archtram = 33
			   	AND ta.i_estatus = 1 -- ACTIVO
			   	LIMIT 1) AS anexo2
		FROM religiosos."TBL_Declaratoria_Procedencia" declaratoria
		WHERE declaratoria.i_id = p_id_declaratoria;

END;
$function$
;