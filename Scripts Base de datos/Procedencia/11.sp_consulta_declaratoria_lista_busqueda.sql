-- DROP FUNCTION religiosos.sp_consulta_declaratoria_lista_busqueda(int4, int4, varchar, varchar, int4);

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_declaratoria_lista_busqueda(p_id_usuario integer, p_id_rol integer, p_denominacion character varying, p_folio character varying, p_estatus integer)
 RETURNS TABLE(id_declaratoria integer, folio character varying, denominacion_religiosa character varying, id_estatus integer, estatus character varying, fecha_envio character varying, fecha_autorizacion character varying, comentarios character varying, id_dictaminador integer, correo_dictaminador text, nombre_dictaminador text)
 LANGUAGE plpgsql
AS $function$
DECLARE

BEGIN

	RETURN QUERY 
	SELECT declaratoria.i_id as id_declaratoria
		, declaratoria.c_folio as folio
		, declaratoria.c_denominacion_religiosa as denominacion_religiosa
		, declaratoria.i_id_tbl_estatus as id_estatus
		, estatus.nombre as estatus
		, declaratoria.d_fecha_envio:: character varying as fecha_envio
		, declaratoria.d_fecha_autorizacion:: character varying as fecha_autorizacion
		, asoc.c_comentarios:: character varying as comentarios
		, asoc.id_tbl_dictaminador as id_dictaminador
		, CONVERT_FROM(DECODE(tblp.c_correo, 'BASE64'), 'UTF-8') AS correo_dictaminador
		, CONCAT(CONVERT_FROM(DECODE(tblp.c_nombre, 'BASE64'), 'UTF-8') ,' ', 
		CONVERT_FROM(DECODE(tblp.c_apaterno, 'BASE64'), 'UTF-8') ,' ', 
		CONVERT_FROM(DECODE(tblp.c_amaterno, 'BASE64'), 'UTF-8')) AS nombre_dictaminador
	FROM religiosos."TBL_Declaratoria_Procedencia" declaratoria
	INNER JOIN religiosos."CAT_Estatus" estatus
		ON estatus.i_id = declaratoria.i_id_tbl_estatus
	LEFT JOIN religiosos."ASOC_DeclaratoriaDictaminador" asoc
		ON asoc.i_id_tbl_declaratoria = declaratoria.i_id
	LEFT JOIN religiosos."TBL_Usuario" tblu 
		ON tblu.i_id = asoc.id_tbl_dictaminador
	LEFT JOIN religiosos."TBL_Persona" tblp 
		ON tblu.i_id_tbl_persona = tblp.i_id
	WHERE declaratoria.b_activo = '1'

	AND ((p_denominacion IS null) OR (LOWER(declaratoria.c_denominacion_religiosa) LIKE ('%'||LOWER(p_denominacion)||'%'))) 
	AND ((p_folio IS null) OR (LOWER(declaratoria.c_folio) LIKE ('%'||LOWER(p_folio)||'%'))) 
	AND ((p_estatus IS null) OR (p_estatus = declaratoria.i_id_tbl_estatus ))
		--AND (declaratoria.i_id_tbl_usuario = p_id_usuario or p_id_usuario = 0)
		--AND (declaratoria.i_id_tbl_estatus = p_id_estatus or p_id_estatus = 0)
		AND CASE
			WHEN (p_id_rol = 0 AND p_id_usuario <> 0) THEN declaratoria.i_id_tbl_estatus IN (39, 40, 41, 42, 43, 44, 45, 46, 47) AND declaratoria.i_id_tbl_usuario = p_id_usuario
			WHEN (p_id_rol = 11 AND p_id_usuario = 0) THEN declaratoria.i_id_tbl_estatus IN (40, 41, 46, 47)
			WHEN (p_id_rol = 12 AND p_id_usuario = 0) THEN declaratoria.i_id_tbl_estatus IN (41, 42, 43, 44, 45, 46, 47)
		END;

END;
$function$
;