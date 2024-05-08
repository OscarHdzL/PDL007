-- DROP FUNCTION religiosos.sp_consulta_declaratoria_paso4(int4);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_declaratoria_paso4(p_id_declaratoria integer)
 RETURNS TABLE(id_declaratoria integer, regular bit, id_ubicacion integer, unidad integer, superficie character varying, colindancia character varying, descripcion_salida character varying, norte numeric, noreste numeric, noroeste numeric, sur numeric, sureste numeric, suroeste numeric, oriente numeric, poniente numeric, ubicacion character varying, culto_publico bit, inicio_actividades character varying, uso integer, otro character varying, imagen_ubicacion integer)
 LANGUAGE plpgsql
AS $function$
DECLARE

id_tramite_declaratoria integer;

BEGIN

	RETURN QUERY 
		SELECT ubicacion.i_id_tbl_declaratoria as id_declaratoria
			, ubicacion.b_regular as regular
			, ubicacion.i_id as id_ubicacion
			, ubicacion.i_id_t_tipo_unidad as unidad
			, ubicacion.c_superficie as superficie
			, ubicacion.c_colindancia as colindancia
			, ubicacion.c_descripcion_salida as descripcion_salida
			, ubicacion.i_norte::numeric as norte
			, ubicacion.i_noreste::numeric as noreste
			, ubicacion.i_noroeste::numeric as noroeste
			, ubicacion.i_sur::numeric as sur
			, ubicacion.i_sureste::numeric as sureste
			, ubicacion.i_suroeste::numeric as suroeste
			, ubicacion.i_oriente::numeric as oriente
			, ubicacion.i_poniente::numeric as poniente
			, ubicacion.c_ubicacion as ubicacion
			, ubicacion.b_culto_publico as culto_publico
			, ubicacion.d_inicio_actividades::character varying  as inicio_actividades
			, ubicacion.i_id_tipo_uso_inmueble as uso
			, ubicacion.c_otro as otro
			, (SELECT ta.i_id
				FROM religiosos."ASOC_Declaratoria_Archivos" asoc
			   	INNER JOIN religiosos."TBL_Archivo" ta ON asoc.i_id_tbl_archivo = ta.i_id
			   	WHERE asoc.i_id_tbl_declaratoria = p_id_declaratoria
			    AND ta.i_id_tbl_archtram = 30
			   	AND ta.i_estatus = 1 -- ACTIVO
			   	LIMIT 1) AS imagen_ubicacion
		FROM religiosos."TBL_Declaratoria_Ubicacion" ubicacion
		WHERE ubicacion.i_id_tbl_declaratoria = p_id_declaratoria;

END;
$function$
;