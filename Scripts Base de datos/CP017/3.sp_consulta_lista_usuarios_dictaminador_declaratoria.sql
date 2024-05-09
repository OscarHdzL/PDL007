-- FUNCTION: religiosos.sp_consulta_lista_usuarios_dictaminador_declaratoria()

-- DROP FUNCTION IF EXISTS religiosos.sp_consulta_lista_usuarios_dictaminador_declaratoria();

CREATE OR REPLACE FUNCTION religiosos.sp_consulta_lista_usuarios_dictaminador_declaratoria(
	)
    RETURNS TABLE(id_usuario integer, usuario text, nombre text) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE

BEGIN

	RETURN QUERY
		SELECT tblu.i_id AS id_usuario
		, CONVERT_FROM(DECODE(tblp.c_correo, 'BASE64'), 'UTF-8') AS usuario
		, CONCAT(CONVERT_FROM(DECODE(tblp.c_nombre, 'BASE64'), 'UTF-8') ,' ', 
		CONVERT_FROM(DECODE(tblp.c_apaterno, 'BASE64'), 'UTF-8') ,' ', 
		CONVERT_FROM(DECODE(tblp.c_amaterno, 'BASE64'), 'UTF-8')) AS nombre
	FROM religiosos."TBL_Usuario" AS tblu 
	INNER JOIN religiosos."TBL_Persona" AS tblp ON tblu.i_id_tbl_persona = tblp.i_id 
	INNER JOIN religiosos."CAT_Perfiles" AS cap ON tblu.i_id_tbl_perfil = cap.i_id
	WHERE tblu.i_id_tbl_perfil in (12) AND tblu.b_activo=true;

END
$BODY$;

ALTER FUNCTION religiosos.sp_consulta_lista_usuarios_dictaminador_declaratoria()
    OWNER TO postgres;